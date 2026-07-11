using ApiToolKit.Application.DTOs.Workspace.Gmail;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;
using System.Text;

namespace ApiToolKit.Application.Services.Workspace.Gmail;

public class GmailDraftService : IGmailDraftService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly IWorkspaceApiClient _apiClient;
    private const string BaseUrl = "https://gmail.googleapis.com/gmail/v1/users/me/drafts";

    public GmailDraftService(IWorkspaceAuthService authService, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _apiClient = apiClient;
    }

    public async Task<Result<ListDraftsResponse>> ListDraftsAsync(string userId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ListDraftsResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<ListDraftsResponse>(BaseUrl, accessToken);
        if (response == null) return Result<ListDraftsResponse>.Fail("No response from Gmail API.");

        return Result<ListDraftsResponse>.Ok(response);
    }

    public async Task<Result<DraftResponse>> GetDraftAsync(string userId, string draftId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<DraftResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<DraftResponse>($"{BaseUrl}/{draftId}", accessToken);
        if (response == null) return Result<DraftResponse>.Fail("Draft not found.");

        return Result<DraftResponse>.Ok(response);
    }

    public async Task<Result<DraftResponse>> CreateDraftAsync(string userId, CreateDraftRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<DraftResponse>.Fail("Access token not found.");

        var rawMessage = BuildRawMessage(request.To, request.Subject, request.Body, request.HtmlBody);
        var body = new { message = new { raw = rawMessage } };

        var response = await _apiClient.PostAsync<DraftResponse>(BaseUrl, body, accessToken);
        if (response == null) return Result<DraftResponse>.Fail("Failed to create draft.");

        return Result<DraftResponse>.Ok(response);
    }

    public async Task<Result<DraftResponse>> UpdateDraftAsync(string userId, UpdateDraftRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<DraftResponse>.Fail("Access token not found.");

        var rawMessage = BuildRawMessage(request.To ?? new List<string>(), request.Subject ?? "", request.Body ?? "", request.HtmlBody);
        var body = new { message = new { raw = rawMessage } };

        var response = await _apiClient.PutAsync<DraftResponse>($"{BaseUrl}/{request.DraftId}", body, accessToken);
        if (response == null) return Result<DraftResponse>.Fail("Failed to update draft.");

        return Result<DraftResponse>.Ok(response);
    }

    public async Task<Result<GmailMessageResponse>> SendDraftAsync(string userId, SendDraftRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<GmailMessageResponse>.Fail("Access token not found.");

        var body = new { id = request.DraftId };
        var response = await _apiClient.PostAsync<GmailMessageResponse>($"{BaseUrl}/send", body, accessToken);
        if (response == null) return Result<GmailMessageResponse>.Fail("Failed to send draft.");

        return Result<GmailMessageResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteDraftAsync(string userId, DeleteDraftRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        await _apiClient.DeleteAsync($"{BaseUrl}/{request.DraftId}", accessToken);
        return Result<bool>.Ok(true);
    }

    private static string BuildRawMessage(List<string> to, string subject, string body, string? htmlBody)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"To: {string.Join(", ", to)}");
        sb.AppendLine($"Subject: {subject}");

        if (!string.IsNullOrEmpty(htmlBody))
        {
            sb.AppendLine("MIME-Version: 1.0");
            sb.AppendLine("Content-Type: text/html; charset=UTF-8");
            sb.AppendLine();
            sb.AppendLine(htmlBody);
        }
        else
        {
            sb.AppendLine("Content-Type: text/plain; charset=UTF-8");
            sb.AppendLine();
            sb.AppendLine(body);
        }

        return Convert.ToBase64String(Encoding.UTF8.GetBytes(sb.ToString()))
            .Replace('+', '-').Replace('/', '_').TrimEnd('=');
    }
}
