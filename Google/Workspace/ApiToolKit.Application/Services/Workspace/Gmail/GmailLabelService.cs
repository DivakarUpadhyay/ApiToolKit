using ApiToolKit.Application.DTOs.Workspace.Gmail;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Workspace.Gmail;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;

namespace ApiToolKit.Application.Services.Workspace.Gmail;

public class GmailLabelService : IGmailLabelService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly IGmailRepository _gmailRepo;
    private readonly IWorkspaceApiClient _apiClient;
    private const string BaseUrl = "https://gmail.googleapis.com/gmail/v1/users/me/labels";

    public GmailLabelService(IWorkspaceAuthService authService, IGmailRepository gmailRepo, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _gmailRepo = gmailRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListLabelsResponse>> ListLabelsAsync(string userId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ListLabelsResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<ListLabelsResponse>(BaseUrl, accessToken);
        if (response == null) return Result<ListLabelsResponse>.Fail("No response from Gmail API.");

        return Result<ListLabelsResponse>.Ok(response);
    }

    public async Task<Result<LabelResponse>> GetLabelAsync(string userId, string labelId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<LabelResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<LabelResponse>($"{BaseUrl}/{labelId}", accessToken);
        if (response == null) return Result<LabelResponse>.Fail("Label not found.");

        return Result<LabelResponse>.Ok(response);
    }

    public async Task<Result<LabelResponse>> CreateLabelAsync(string userId, CreateLabelRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<LabelResponse>.Fail("Access token not found.");

        var body = new
        {
            name = request.Name,
            messageListVisibility = request.MessageListVisibility,
            labelListVisibility = request.LabelListVisibility,
            color = request.TextColor != null ? new { textColor = request.TextColor, backgroundColor = request.BackgroundColor } : null
        };

        var response = await _apiClient.PostAsync<LabelResponse>(BaseUrl, body, accessToken);
        if (response == null) return Result<LabelResponse>.Fail("Failed to create label.");

        await _gmailRepo.SaveLabelAsync(new GmailLabel
        {
            LabelId = response.Id ?? string.Empty,
            UserId = userId,
            Name = response.Name ?? request.Name,
            Type = response.Type,
            MessageListVisibility = response.MessageListVisibility,
            LabelListVisibility = response.LabelListVisibility,
            CreatedAt = DateTime.UtcNow
        });

        return Result<LabelResponse>.Ok(response);
    }

    public async Task<Result<LabelResponse>> UpdateLabelAsync(string userId, UpdateLabelRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<LabelResponse>.Fail("Access token not found.");

        var body = new
        {
            name = request.Name,
            messageListVisibility = request.MessageListVisibility,
            labelListVisibility = request.LabelListVisibility,
            color = request.TextColor != null ? new { textColor = request.TextColor, backgroundColor = request.BackgroundColor } : null
        };

        var response = await _apiClient.PatchAsync<LabelResponse>($"{BaseUrl}/{request.LabelId}", body, accessToken);
        if (response == null) return Result<LabelResponse>.Fail("Failed to update label.");

        return Result<LabelResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteLabelAsync(string userId, DeleteLabelRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        await _apiClient.DeleteAsync($"{BaseUrl}/{request.LabelId}", accessToken);
        return Result<bool>.Ok(true);
    }
}
