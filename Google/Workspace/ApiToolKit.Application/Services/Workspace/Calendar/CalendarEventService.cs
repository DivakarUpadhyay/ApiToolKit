using ApiToolKit.Application.DTOs.Workspace.Calendar;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Workspace.Calendar;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;
using Newtonsoft.Json;
using System.Text;

namespace ApiToolKit.Application.Services.Workspace.Calendar;

public class CalendarEventService : ICalendarEventService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly ICalendarRepository _calendarRepo;
    private readonly IWorkspaceApiClient _apiClient;
    private const string BaseUrl = "https://www.googleapis.com/calendar/v3/calendars";

    public CalendarEventService(IWorkspaceAuthService authService, ICalendarRepository calendarRepo, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _calendarRepo = calendarRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<EventListResponse>> ListEventsAsync(string userId, ListEventsRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<EventListResponse>.Fail("Access token not found.");

        var sb = new StringBuilder($"{BaseUrl}/{Uri.EscapeDataString(request.CalendarId)}/events?maxResults={request.MaxResults}&orderBy={request.OrderBy}&singleEvents={request.SingleEvents.ToString().ToLower()}");
        if (!string.IsNullOrEmpty(request.PageToken)) sb.Append($"&pageToken={Uri.EscapeDataString(request.PageToken)}");
        if (request.TimeMin.HasValue) sb.Append($"&timeMin={Uri.EscapeDataString(request.TimeMin.Value.ToString("o"))}");
        if (request.TimeMax.HasValue) sb.Append($"&timeMax={Uri.EscapeDataString(request.TimeMax.Value.ToString("o"))}");
        if (!string.IsNullOrEmpty(request.Q)) sb.Append($"&q={Uri.EscapeDataString(request.Q)}");
        if (request.ShowDeleted) sb.Append("&showDeleted=true");

        var response = await _apiClient.GetAsync<EventListResponse>(sb.ToString(), accessToken);
        if (response == null) return Result<EventListResponse>.Fail("No response from Calendar API.");

        return Result<EventListResponse>.Ok(response);
    }

    public async Task<Result<EventResponse>> GetEventAsync(string userId, string calendarId, string eventId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<EventResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<EventResponse>($"{BaseUrl}/{Uri.EscapeDataString(calendarId)}/events/{eventId}", accessToken);
        if (response == null) return Result<EventResponse>.Fail("Event not found.");

        return Result<EventResponse>.Ok(response);
    }

    public async Task<Result<EventResponse>> CreateEventAsync(string userId, CreateEventRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<EventResponse>.Fail("Access token not found.");

        var attendees = request.Attendees?.Select(a => new { email = a }).ToList();
        var body = new
        {
            summary = request.Summary,
            description = request.Description,
            location = request.Location,
            start = request.Start,
            end = request.End,
            attendees,
            recurrence = request.Recurrence,
            colorId = request.ColorId,
            visibility = request.Visibility,
            reminders = request.Reminders != null ? new { useDefault = false, overrides = request.Reminders } : null,
            conferenceData = request.ConferenceDataVersion > 0 ? new { createRequest = new { requestId = Guid.NewGuid().ToString() } } : null
        };

        var url = $"{BaseUrl}/{Uri.EscapeDataString(request.CalendarId)}/events?sendNotifications={request.SendNotifications.ToString().ToLower()}";
        if (request.ConferenceDataVersion > 0) url += $"&conferenceDataVersion={request.ConferenceDataVersion}";

        var response = await _apiClient.PostAsync<EventResponse>(url, body, accessToken);
        if (response == null) return Result<EventResponse>.Fail("Failed to create event.");

        if (response.Id != null)
        {
            await _calendarRepo.SaveEventAsync(new CalendarEvent
            {
                EventId = response.Id,
                CalendarId = request.CalendarId,
                UserId = userId,
                Summary = response.Summary,
                Description = response.Description,
                Location = response.Location,
                StartDateTime = response.Start?.DateTime,
                EndDateTime = response.End?.DateTime,
                StartTimeZone = response.Start?.TimeZone,
                EndTimeZone = response.End?.TimeZone,
                Status = response.Status,
                HtmlLink = response.HtmlLink,
                RecurringEventId = response.RecurringEventId,
                Recurrence = response.Recurrence != null ? string.Join(";", response.Recurrence) : null,
                Attendees = response.Attendees != null ? JsonConvert.SerializeObject(response.Attendees) : null,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        return Result<EventResponse>.Ok(response);
    }

    public async Task<Result<EventResponse>> UpdateEventAsync(string userId, UpdateEventRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<EventResponse>.Fail("Access token not found.");

        var attendees = request.Attendees?.Select(a => new { email = a }).ToList();
        var body = new
        {
            summary = request.Summary,
            description = request.Description,
            location = request.Location,
            start = request.Start,
            end = request.End,
            attendees
        };

        var url = $"{BaseUrl}/{Uri.EscapeDataString(request.CalendarId)}/events/{request.EventId}?sendNotifications={request.SendNotifications.ToString().ToLower()}";

        var response = await _apiClient.PatchAsync<EventResponse>(url, body, accessToken);
        if (response == null) return Result<EventResponse>.Fail("Failed to update event.");

        return Result<EventResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteEventAsync(string userId, DeleteEventRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        var url = $"{BaseUrl}/{Uri.EscapeDataString(request.CalendarId)}/events/{request.EventId}?sendNotifications={request.SendNotifications.ToString().ToLower()}";
        await _apiClient.DeleteAsync(url, accessToken);
        await _calendarRepo.DeleteEventAsync(request.EventId, request.CalendarId);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<EventResponse>> QuickAddEventAsync(string userId, QuickAddEventRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<EventResponse>.Fail("Access token not found.");

        var url = $"{BaseUrl}/{Uri.EscapeDataString(request.CalendarId)}/events/quickAdd?text={Uri.EscapeDataString(request.Text)}";
        var response = await _apiClient.PostAsync<EventResponse>(url, null, accessToken);
        if (response == null) return Result<EventResponse>.Fail("Failed to quick add event.");

        return Result<EventResponse>.Ok(response);
    }

    public async Task<Result<EventResponse>> MoveEventAsync(string userId, MoveEventRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<EventResponse>.Fail("Access token not found.");

        var url = $"{BaseUrl}/{Uri.EscapeDataString(request.CalendarId)}/events/{request.EventId}/move?destination={Uri.EscapeDataString(request.Destination)}";
        var response = await _apiClient.PostAsync<EventResponse>(url, null, accessToken);
        if (response == null) return Result<EventResponse>.Fail("Failed to move event.");

        return Result<EventResponse>.Ok(response);
    }
}
