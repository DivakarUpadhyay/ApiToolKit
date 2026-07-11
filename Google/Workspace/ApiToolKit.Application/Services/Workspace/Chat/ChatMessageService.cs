using ApiToolKit.Application.DTOs.Workspace.Chat;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Workspace.Chat;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;

namespace ApiToolKit.Application.Services.Workspace.Chat;

public class ChatMessageService : IChatMessageService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly IChatRepository _chatRepo;
    private readonly IWorkspaceApiClient _apiClient;
    private const string BaseUrl = "https://chat.googleapis.com/v1";

    public ChatMessageService(IWorkspaceAuthService authService, IChatRepository chatRepo, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _chatRepo = chatRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListMessagesResponse>> ListMessagesAsync(string userId, ListMessagesRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ListMessagesResponse>.Fail("Access token not found.");

        var url = $"{BaseUrl}/{request.SpaceName}/messages?pageSize={request.PageSize}&orderBy={Uri.EscapeDataString(request.OrderBy)}&showDeleted={request.ShowDeleted.ToString().ToLower()}";
        if (!string.IsNullOrEmpty(request.PageToken)) url += $"&pageToken={Uri.EscapeDataString(request.PageToken)}";
        if (!string.IsNullOrEmpty(request.Filter)) url += $"&filter={Uri.EscapeDataString(request.Filter)}";

        var response = await _apiClient.GetAsync<ListMessagesResponse>(url, accessToken);
        if (response == null) return Result<ListMessagesResponse>.Fail("No response from Chat API.");

        return Result<ListMessagesResponse>.Ok(response);
    }

    public async Task<Result<MessageResponse>> GetMessageAsync(string userId, string messageName)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<MessageResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<MessageResponse>($"{BaseUrl}/{messageName}", accessToken);
        if (response == null) return Result<MessageResponse>.Fail("Message not found.");

        return Result<MessageResponse>.Ok(response);
    }

    public async Task<Result<MessageResponse>> SendMessageAsync(string userId, SendMessageRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<MessageResponse>.Fail("Access token not found.");

        var url = $"{BaseUrl}/{request.SpaceName}/messages?messageReplyOption={request.ReplyOption}";
        var body = new
        {
            text = request.Text,
            thread = request.ThreadName != null ? new { name = request.ThreadName } : null
        };

        var response = await _apiClient.PostAsync<MessageResponse>(url, body, accessToken);
        if (response == null) return Result<MessageResponse>.Fail("Failed to send message.");

        await _chatRepo.SaveMessageAsync(new ChatMessage
        {
            MessageName = response.Name ?? string.Empty,
            SpaceName = request.SpaceName,
            UserId = userId,
            Text = response.Text,
            CreateTime = response.CreateTime,
            LastUpdateTime = response.LastUpdateTime,
            FormattedText = response.FormattedText,
            CreatedAt = DateTime.UtcNow
        });

        return Result<MessageResponse>.Ok(response);
    }

    public async Task<Result<MessageResponse>> UpdateMessageAsync(string userId, UpdateMessageRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<MessageResponse>.Fail("Access token not found.");

        var url = $"{BaseUrl}/{request.MessageName}?updateMask={request.UpdateMask}";
        var body = new { text = request.Text };

        var response = await _apiClient.PatchAsync<MessageResponse>(url, body, accessToken);
        if (response == null) return Result<MessageResponse>.Fail("Failed to update message.");

        return Result<MessageResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteMessageAsync(string userId, DeleteMessageRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        await _apiClient.DeleteAsync($"{BaseUrl}/{request.MessageName}", accessToken);
        return Result<bool>.Ok(true);
    }
}
