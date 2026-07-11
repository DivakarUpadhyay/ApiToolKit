using ApiToolKit.Application.DTOs.Workspace.Calendar;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Workspace.Calendar;

public class CalendarFreeBusyService : ICalendarFreeBusyService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly IWorkspaceApiClient _apiClient;
    private const string FreeBusyUrl = "https://www.googleapis.com/calendar/v3/freeBusy";

    public CalendarFreeBusyService(IWorkspaceAuthService authService, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _apiClient = apiClient;
    }

    public async Task<Result<FreeBusyResponse>> QueryFreeBusyAsync(string userId, FreeBusyRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<FreeBusyResponse>.Fail("Access token not found.");

        var body = new
        {
            timeMin = request.TimeMin.ToString("o"),
            timeMax = request.TimeMax.ToString("o"),
            timeZone = request.TimeZone,
            items = request.Items.Select(id => new { id }).ToList()
        };

        var response = await _apiClient.PostAsync<FreeBusyResponse>(FreeBusyUrl, body, accessToken);
        if (response == null) return Result<FreeBusyResponse>.Fail("No response from Calendar API.");

        return Result<FreeBusyResponse>.Ok(response);
    }
}
