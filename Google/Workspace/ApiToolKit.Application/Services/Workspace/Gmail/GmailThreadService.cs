using ApiToolKit.Application.DTOs.Workspace.Gmail;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;
using System.Text;

namespace ApiToolKit.Application.Services.Workspace.Gmail;

public class GmailThreadService : IGmailThreadService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly IWorkspaceApiClient _apiClient;
    private const string BaseUrl = "https://gmail.googleapis.com/gmail/v1/users/me/threads";

    public GmailThreadService(IWorkspaceAuthService authService, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _apiClient = apiClient;
    }

    public async Task<Result<ListThreadsResponse>> ListThreadsAsync(string userId, ListThreadsRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ListThreadsResponse>.Fail("Access token not found.");

        var sb = new StringBuilder($"{BaseUrl}?maxResults={request.MaxResults}");
        if (!string.IsNullOrEmpty(request.PageToken)) sb.Append($"&pageToken={Uri.EscapeDataString(request.PageToken)}");
        if (!string.IsNullOrEmpty(request.Q)) sb.Append($"&q={Uri.EscapeDataString(request.Q)}");
        if (request.LabelIds?.Any() == true)
            foreach (var label in request.LabelIds) sb.Append($"&labelIds={Uri.EscapeDataString(label)}");

        var response = await _apiClient.GetAsync<ListThreadsResponse>(sb.ToString(), accessToken);
        if (response == null) return Result<ListThreadsResponse>.Fail("No response from Gmail API.");

        return Result<ListThreadsResponse>.Ok(response);
    }

    public async Task<Result<ThreadResponse>> GetThreadAsync(string userId, string threadId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ThreadResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<ThreadResponse>($"{BaseUrl}/{threadId}", accessToken);
        if (response == null) return Result<ThreadResponse>.Fail("Thread not found.");

        return Result<ThreadResponse>.Ok(response);
    }

    public async Task<Result<ThreadResponse>> ModifyThreadAsync(string userId, ModifyThreadRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ThreadResponse>.Fail("Access token not found.");

        var body = new { addLabelIds = request.AddLabelIds, removeLabelIds = request.RemoveLabelIds };
        var response = await _apiClient.PostAsync<ThreadResponse>($"{BaseUrl}/{request.ThreadId}/modify", body, accessToken);
        if (response == null) return Result<ThreadResponse>.Fail("Failed to modify thread.");

        return Result<ThreadResponse>.Ok(response);
    }

    public async Task<Result<GmailMessageResponse>> TrashThreadAsync(string userId, string threadId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<GmailMessageResponse>.Fail("Access token not found.");

        var response = await _apiClient.PostAsync<GmailMessageResponse>($"{BaseUrl}/{threadId}/trash", null, accessToken);
        if (response == null) return Result<GmailMessageResponse>.Fail("Failed to trash thread.");

        return Result<GmailMessageResponse>.Ok(response);
    }

    public async Task<Result<GmailMessageResponse>> UntrashThreadAsync(string userId, string threadId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<GmailMessageResponse>.Fail("Access token not found.");

        var response = await _apiClient.PostAsync<GmailMessageResponse>($"{BaseUrl}/{threadId}/untrash", null, accessToken);
        if (response == null) return Result<GmailMessageResponse>.Fail("Failed to untrash thread.");

        return Result<GmailMessageResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteThreadAsync(string userId, DeleteThreadRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        await _apiClient.DeleteAsync($"{BaseUrl}/{request.ThreadId}", accessToken);
        return Result<bool>.Ok(true);
    }
}
