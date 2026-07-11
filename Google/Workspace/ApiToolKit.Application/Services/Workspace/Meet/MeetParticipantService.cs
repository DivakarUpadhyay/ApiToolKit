using ApiToolKit.Application.DTOs.Workspace.Meet;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Workspace.Meet;

public class MeetParticipantService : IMeetParticipantService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly IWorkspaceApiClient _apiClient;
    private const string BaseUrl = "https://meet.googleapis.com/v2";

    public MeetParticipantService(IWorkspaceAuthService authService, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _apiClient = apiClient;
    }

    public async Task<Result<ListParticipantsResponse>> ListParticipantsAsync(string userId, ListParticipantsRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ListParticipantsResponse>.Fail("Access token not found.");

        var url = $"{BaseUrl}/{request.ConferenceRecord}/participants?pageSize={request.PageSize}";
        if (!string.IsNullOrEmpty(request.PageToken)) url += $"&pageToken={Uri.EscapeDataString(request.PageToken)}";

        var response = await _apiClient.GetAsync<ListParticipantsResponse>(url, accessToken);
        if (response == null) return Result<ListParticipantsResponse>.Fail("No response from Meet API.");

        return Result<ListParticipantsResponse>.Ok(response);
    }

    public async Task<Result<ParticipantResponse>> GetParticipantAsync(string userId, string participantName)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ParticipantResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<ParticipantResponse>($"{BaseUrl}/{participantName}", accessToken);
        if (response == null) return Result<ParticipantResponse>.Fail("Participant not found.");

        return Result<ParticipantResponse>.Ok(response);
    }
}
