using ApiToolKit.Application.DTOs.Workspace.Meet;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Workspace.Meet;

public class MeetRecordingService : IMeetRecordingService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly IWorkspaceApiClient _apiClient;
    private const string BaseUrl = "https://meet.googleapis.com/v2";

    public MeetRecordingService(IWorkspaceAuthService authService, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _apiClient = apiClient;
    }

    public async Task<Result<ListRecordingsResponse>> ListRecordingsAsync(string userId, ListRecordingsRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ListRecordingsResponse>.Fail("Access token not found.");

        var url = $"{BaseUrl}/{request.ConferenceRecord}/recordings?pageSize={request.PageSize}";
        if (!string.IsNullOrEmpty(request.PageToken)) url += $"&pageToken={Uri.EscapeDataString(request.PageToken)}";

        var response = await _apiClient.GetAsync<ListRecordingsResponse>(url, accessToken);
        if (response == null) return Result<ListRecordingsResponse>.Fail("No response from Meet API.");

        return Result<ListRecordingsResponse>.Ok(response);
    }

    public async Task<Result<RecordingResponse>> GetRecordingAsync(string userId, string recordingName)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<RecordingResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<RecordingResponse>($"{BaseUrl}/{recordingName}", accessToken);
        if (response == null) return Result<RecordingResponse>.Fail("Recording not found.");

        return Result<RecordingResponse>.Ok(response);
    }

    public async Task<Result<ListConferenceRecordsResponse>> ListConferenceRecordsAsync(string userId, ListConferenceRecordsRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ListConferenceRecordsResponse>.Fail("Access token not found.");

        var url = $"{BaseUrl}/conferenceRecords?pageSize={request.PageSize}";
        if (!string.IsNullOrEmpty(request.Filter)) url += $"&filter={Uri.EscapeDataString(request.Filter)}";
        if (!string.IsNullOrEmpty(request.PageToken)) url += $"&pageToken={Uri.EscapeDataString(request.PageToken)}";

        var response = await _apiClient.GetAsync<ListConferenceRecordsResponse>(url, accessToken);
        if (response == null) return Result<ListConferenceRecordsResponse>.Fail("No response from Meet API.");

        return Result<ListConferenceRecordsResponse>.Ok(response);
    }

    public async Task<Result<ConferenceRecordResponse>> GetConferenceRecordAsync(string userId, string conferenceRecordName)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ConferenceRecordResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<ConferenceRecordResponse>($"{BaseUrl}/{conferenceRecordName}", accessToken);
        if (response == null) return Result<ConferenceRecordResponse>.Fail("Conference record not found.");

        return Result<ConferenceRecordResponse>.Ok(response);
    }
}
