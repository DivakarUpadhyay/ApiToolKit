using ApiToolKit.Application.DTOs.Workspace.Chat;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Workspace.Chat;

public class ChatSpaceService : IChatSpaceService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly IWorkspaceApiClient _apiClient;
    private const string BaseUrl = "https://chat.googleapis.com/v1/spaces";

    public ChatSpaceService(IWorkspaceAuthService authService, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _apiClient = apiClient;
    }

    public async Task<Result<ListChatSpacesResponse>> ListSpacesAsync(string userId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ListChatSpacesResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<ListChatSpacesResponse>(BaseUrl, accessToken);
        if (response == null) return Result<ListChatSpacesResponse>.Fail("No response from Chat API.");

        return Result<ListChatSpacesResponse>.Ok(response);
    }

    public async Task<Result<ChatSpaceResponse>> GetSpaceAsync(string userId, string spaceName)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ChatSpaceResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<ChatSpaceResponse>($"{BaseUrl}/{spaceName}", accessToken);
        if (response == null) return Result<ChatSpaceResponse>.Fail("Chat space not found.");

        return Result<ChatSpaceResponse>.Ok(response);
    }

    public async Task<Result<ChatSpaceResponse>> CreateSpaceAsync(string userId, CreateChatSpaceRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ChatSpaceResponse>.Fail("Access token not found.");

        var body = new
        {
            displayName = request.DisplayName,
            spaceType = request.SpaceType,
            memberships = request.Members?.Select(m => new { member = new { name = $"users/{m}", type = "HUMAN" } }).ToList()
        };

        var response = await _apiClient.PostAsync<ChatSpaceResponse>(BaseUrl, body, accessToken);
        if (response == null) return Result<ChatSpaceResponse>.Fail("Failed to create chat space.");

        return Result<ChatSpaceResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteSpaceAsync(string userId, string spaceName)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        await _apiClient.DeleteAsync($"{BaseUrl}/{spaceName}", accessToken);
        return Result<bool>.Ok(true);
    }
}
