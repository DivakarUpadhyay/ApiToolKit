using ApiToolKit.Application.DTOs.Facebook.Event;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookEventService : IFacebookEventService
{
    private readonly IFacebookAuthRepository _authRepo;
    private readonly IFacebookEventRepository _eventRepo;
    private readonly IFacebookApiClient _apiClient;

    public FacebookEventService(IFacebookAuthRepository authRepo, IFacebookEventRepository eventRepo, IFacebookApiClient apiClient)
    {
        _authRepo = authRepo;
        _eventRepo = eventRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<CreateEventResponse>> CreateEventAsync(CreateEventRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<CreateEventResponse>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?>
        {
            ["name"] = request.Name,
            ["start_time"] = request.StartTime.ToString("o"),
            ["privacy"] = request.Privacy
        };
        if (request.Description != null) body["description"] = request.Description;
        if (request.EndTime.HasValue) body["end_time"] = request.EndTime.Value.ToString("o");
        if (request.Location != null) body["location"] = request.Location;
        if (request.LocationId != null) body["place"] = request.LocationId;
        if (request.CoverUrl != null) body["cover_url"] = request.CoverUrl;
        body["is_online"] = request.IsOnline;
        if (request.OnlineEventFormat != null) body["online_event_format"] = request.OnlineEventFormat;

        var response = await _apiClient.PostAsync<dynamic>($"/{request.PageId}/events", body, accessToken);
        if (response == null) return Result<CreateEventResponse>.Fail("No response from Facebook API.");

        string eventId = response?.id?.ToString() ?? string.Empty;

        await _eventRepo.SaveEventAsync(new FacebookEvent
        {
            EventId = eventId,
            PageId = request.PageId,
            Name = request.Name,
            Description = request.Description,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Place = request.Location,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        await _eventRepo.SaveApiLogAsync(new FacebookApiLog
        {
            PageId = request.PageId,
            ApiName = "CreateEvent",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<CreateEventResponse>.Ok(new CreateEventResponse { Id = eventId });
    }

    public async Task<Result<EventResponse>> GetEventAsync(string eventId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<EventResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,description,start_time,end_time,place,cover,attending_count,interested_count,declined_count,guest_list_enabled",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{eventId}", queryParams);
        if (response == null) return Result<EventResponse>.Fail("No response from Facebook API.");

        var eventResponse = JsonConvert.DeserializeObject<EventResponse>(JsonConvert.SerializeObject(response)) ?? new EventResponse();
        return Result<EventResponse>.Ok(eventResponse);
    }

    public async Task<Result<bool>> UpdateEventAsync(UpdateEventRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?>();
        if (request.Name != null) body["name"] = request.Name;
        if (request.Description != null) body["description"] = request.Description;
        if (request.StartTime.HasValue) body["start_time"] = request.StartTime.Value.ToString("o");
        if (request.EndTime.HasValue) body["end_time"] = request.EndTime.Value.ToString("o");

        var response = await _apiClient.PostAsync<dynamic>($"/{request.EventId}", body, accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> DeleteEventAsync(string eventId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var response = await _apiClient.DeleteAsync<dynamic>($"/{eventId}", accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        await _eventRepo.DeleteEventAsync(eventId);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<List<EventResponse>>> GetEventsByPageAsync(string pageId, string accessToken, int limit)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<List<EventResponse>>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,description,start_time,end_time,place,cover,attending_count,interested_count,declined_count",
            ["limit"] = limit.ToString(),
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{pageId}/events", queryParams);
        if (response == null) return Result<List<EventResponse>>.Fail("No response from Facebook API.");

        var events = JsonConvert.DeserializeObject<List<EventResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<EventResponse>();

        return Result<List<EventResponse>>.Ok(events);
    }

    public async Task<Result<List<EventAttendeeResponse>>> GetEventAttendeesAsync(GetEventAttendeesRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<List<EventAttendeeResponse>>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,rsvp_status",
            ["limit"] = request.Limit.ToString(),
            ["access_token"] = accessToken
        };
        if (request.After != null) queryParams["after"] = request.After;

        var response = await _apiClient.GetAsync<dynamic>($"/{request.EventId}/attending", queryParams);
        if (response == null) return Result<List<EventAttendeeResponse>>.Fail("No response from Facebook API.");

        var attendees = JsonConvert.DeserializeObject<List<EventAttendeeResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<EventAttendeeResponse>();

        return Result<List<EventAttendeeResponse>>.Ok(attendees);
    }

    public async Task<Result<bool>> RsvpEventAsync(RsvpEventRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var endpoint = request.Rsvp.ToLower() switch
        {
            "attending" => $"/{request.EventId}/attending",
            "maybe" => $"/{request.EventId}/maybe",
            "declined" => $"/{request.EventId}/declined",
            _ => $"/{request.EventId}/attending"
        };

        var response = await _apiClient.PostAsync<dynamic>(endpoint, new { }, accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        return Result<bool>.Ok(true);
    }
}
