using ApiToolKit.Application.DTOs.Workspace.Gmail;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Workspace;
using ApiToolKit.Domain.Entities.Workspace.Gmail;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;
using Newtonsoft.Json;
using System.Text;

namespace ApiToolKit.Application.Services.Workspace.Gmail;

public class GmailMessageService : IGmailMessageService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly IGmailRepository _gmailRepo;
    private readonly IWorkspaceAuthRepository _authRepo;
    private readonly IWorkspaceApiClient _apiClient;
    private const string BaseUrl = "https://gmail.googleapis.com/gmail/v1/users/";

    public GmailMessageService(IWorkspaceAuthService authService, IGmailRepository gmailRepo,
        IWorkspaceAuthRepository authRepo, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _gmailRepo = gmailRepo;
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListMessagesResponse>> ListMessagesAsync(string userId, ListMessagesRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ListMessagesResponse>.Fail("Access token not found.");

        var sb = new StringBuilder($"{BaseUrl}{request.UserId}/messages?maxResults={request.MaxResults}");
        if (!string.IsNullOrEmpty(request.PageToken)) sb.Append($"&pageToken={Uri.EscapeDataString(request.PageToken)}");
        if (!string.IsNullOrEmpty(request.Q)) sb.Append($"&q={Uri.EscapeDataString(request.Q)}");
        if (request.LabelIds?.Any() == true)
            foreach (var label in request.LabelIds) sb.Append($"&labelIds={Uri.EscapeDataString(label)}");
        if (request.IncludeSpamTrash) sb.Append("&includeSpamTrash=true");

        var response = await _apiClient.GetAsync<ListMessagesResponse>(sb.ToString(), accessToken);
        if (response == null) return Result<ListMessagesResponse>.Fail("No response from Gmail API.");

        await _authRepo.SaveApiLogAsync(new WorkspaceApiLog
        {
            UserId = userId, Product = "Gmail", ApiName = "ListMessages",
            IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow
        });

        return Result<ListMessagesResponse>.Ok(response);
    }

    public async Task<Result<GmailMessageResponse>> GetMessageAsync(string userId, string messageId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<GmailMessageResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<GmailMessageResponse>($"{BaseUrl}me/messages/{messageId}", accessToken);
        if (response == null) return Result<GmailMessageResponse>.Fail("Message not found.");

        await _gmailRepo.SaveMessageAsync(new GmailMessage
        {
            MessageId = response.Id ?? messageId,
            ThreadId = response.ThreadId ?? string.Empty,
            UserId = userId,
            Snippet = response.Snippet,
            LabelIds = response.LabelIds != null ? string.Join(",", response.LabelIds) : null,
            InternalDate = response.InternalDate,
            SizeEstimate = response.SizeEstimate,
            CreatedAt = DateTime.UtcNow
        });

        return Result<GmailMessageResponse>.Ok(response);
    }

    public async Task<Result<GmailMessageResponse>> SendMessageAsync(string userId, SendMessageRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<GmailMessageResponse>.Fail("Access token not found.");

        var rawMessage = BuildRawMessage(request);
        var body = new { raw = rawMessage };
        var response = await _apiClient.PostAsync<GmailMessageResponse>($"{BaseUrl}me/messages/send", body, accessToken);
        if (response == null) return Result<GmailMessageResponse>.Fail("Failed to send message.");

        return Result<GmailMessageResponse>.Ok(response);
    }

    public async Task<Result<GmailMessageResponse>> ModifyMessageAsync(string userId, ModifyMessageRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<GmailMessageResponse>.Fail("Access token not found.");

        var body = new { addLabelIds = request.AddLabelIds, removeLabelIds = request.RemoveLabelIds };
        var response = await _apiClient.PostAsync<GmailMessageResponse>(
            $"{BaseUrl}{request.UserId}/messages/{request.MessageId}/modify", body, accessToken);
        if (response == null) return Result<GmailMessageResponse>.Fail("Failed to modify message.");

        return Result<GmailMessageResponse>.Ok(response);
    }

    public async Task<Result<bool>> BatchModifyAsync(string userId, BatchModifyRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        var body = new { ids = request.Ids, addLabelIds = request.AddLabelIds, removeLabelIds = request.RemoveLabelIds };
        await _apiClient.PostAsync<object>($"{BaseUrl}me/messages/batchModify", body, accessToken);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> DeleteMessageAsync(string userId, DeleteMessageRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        await _apiClient.DeleteAsync($"{BaseUrl}{request.UserId}/messages/{request.MessageId}", accessToken);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<GmailMessageResponse>> TrashMessageAsync(string userId, TrashMessageRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<GmailMessageResponse>.Fail("Access token not found.");

        var response = await _apiClient.PostAsync<GmailMessageResponse>(
            $"{BaseUrl}{request.UserId}/messages/{request.MessageId}/trash", null, accessToken);
        if (response == null) return Result<GmailMessageResponse>.Fail("Failed to trash message.");

        return Result<GmailMessageResponse>.Ok(response);
    }

    public async Task<Result<GmailMessageResponse>> UntrashMessageAsync(string userId, string messageId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<GmailMessageResponse>.Fail("Access token not found.");

        var response = await _apiClient.PostAsync<GmailMessageResponse>(
            $"{BaseUrl}me/messages/{messageId}/untrash", null, accessToken);
        if (response == null) return Result<GmailMessageResponse>.Fail("Failed to untrash message.");

        return Result<GmailMessageResponse>.Ok(response);
    }

    public async Task<Result<object>> GetAttachmentAsync(string userId, GetAttachmentRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<object>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<object>(
            $"{BaseUrl}{request.UserId}/messages/{request.MessageId}/attachments/{request.AttachmentId}", accessToken);
        if (response == null) return Result<object>.Fail("Attachment not found.");

        return Result<object>.Ok(response);
    }

    private static string BuildRawMessage(SendMessageRequest request)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"To: {string.Join(", ", request.To)}");
        if (request.Cc?.Any() == true) sb.AppendLine($"Cc: {string.Join(", ", request.Cc)}");
        if (request.Bcc?.Any() == true) sb.AppendLine($"Bcc: {string.Join(", ", request.Bcc)}");
        sb.AppendLine($"Subject: {request.Subject}");
        if (!string.IsNullOrEmpty(request.ReplyTo)) sb.AppendLine($"Reply-To: {request.ReplyTo}");
        if (!string.IsNullOrEmpty(request.InReplyTo)) sb.AppendLine($"In-Reply-To: {request.InReplyTo}");
        if (!string.IsNullOrEmpty(request.References)) sb.AppendLine($"References: {request.References}");

        if (!string.IsNullOrEmpty(request.HtmlBody))
        {
            sb.AppendLine("MIME-Version: 1.0");
            sb.AppendLine("Content-Type: text/html; charset=UTF-8");
            sb.AppendLine();
            sb.AppendLine(request.HtmlBody);
        }
        else
        {
            sb.AppendLine("Content-Type: text/plain; charset=UTF-8");
            sb.AppendLine();
            sb.AppendLine(request.Body);
        }

        return Convert.ToBase64String(Encoding.UTF8.GetBytes(sb.ToString()))
            .Replace('+', '-').Replace('/', '_').TrimEnd('=');
    }
}
