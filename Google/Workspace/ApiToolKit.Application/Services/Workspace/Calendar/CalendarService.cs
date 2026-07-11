using ApiToolKit.Application.DTOs.Workspace.Calendar;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Workspace.Calendar;

public class CalendarService : ICalendarService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly IWorkspaceApiClient _apiClient;
    private const string BaseUrl = "https://www.googleapis.com/calendar/v3/users/me/calendarList";
    private const string CalendarsUrl = "https://www.googleapis.com/calendar/v3/calendars";

    public CalendarService(IWorkspaceAuthService authService, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _apiClient = apiClient;
    }

    public async Task<Result<ListCalendarsResponse>> ListCalendarsAsync(string userId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ListCalendarsResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<ListCalendarsResponse>(BaseUrl, accessToken);
        if (response == null) return Result<ListCalendarsResponse>.Fail("No response from Calendar API.");

        return Result<ListCalendarsResponse>.Ok(response);
    }

    public async Task<Result<CalendarResponse>> GetCalendarAsync(string userId, string calendarId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<CalendarResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<CalendarResponse>($"{CalendarsUrl}/{calendarId}", accessToken);
        if (response == null) return Result<CalendarResponse>.Fail("Calendar not found.");

        return Result<CalendarResponse>.Ok(response);
    }

    public async Task<Result<CalendarResponse>> CreateCalendarAsync(string userId, CreateCalendarRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<CalendarResponse>.Fail("Access token not found.");

        var body = new { summary = request.Summary, description = request.Description, timeZone = request.TimeZone, location = request.Location };
        var response = await _apiClient.PostAsync<CalendarResponse>(CalendarsUrl, body, accessToken);
        if (response == null) return Result<CalendarResponse>.Fail("Failed to create calendar.");

        return Result<CalendarResponse>.Ok(response);
    }

    public async Task<Result<CalendarResponse>> UpdateCalendarAsync(string userId, UpdateCalendarRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<CalendarResponse>.Fail("Access token not found.");

        var body = new
        {
            summary = request.Summary,
            description = request.Description,
            timeZone = request.TimeZone,
            location = request.Location,
            colorId = request.ColorId
        };

        var response = await _apiClient.PatchAsync<CalendarResponse>($"{CalendarsUrl}/{request.CalendarId}", body, accessToken);
        if (response == null) return Result<CalendarResponse>.Fail("Failed to update calendar.");

        return Result<CalendarResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteCalendarAsync(string userId, DeleteCalendarRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        await _apiClient.DeleteAsync($"{CalendarsUrl}/{request.CalendarId}", accessToken);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> ClearCalendarAsync(string userId, ClearCalendarRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        await _apiClient.PostAsync<object>($"{CalendarsUrl}/{request.CalendarId}/clear", null, accessToken);
        return Result<bool>.Ok(true);
    }
}
