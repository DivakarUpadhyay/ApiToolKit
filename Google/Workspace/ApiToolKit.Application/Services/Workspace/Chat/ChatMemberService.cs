using ApiToolKit.Application.DTOs.Workspace.Chat;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Workspace.Chat;

public class ChatMemberService : IChatMemberService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly IWorkspaceApiClient _apiClient;
    private const string BaseUrl = "https://chat.googleapis.com/v1";

    public ChatMemberService(IWorkspaceAuthService authService, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _apiClient = apiClient;
    }

    public async Task<Result<ListMembersResponse>> ListMembersAsync(string userId, ListMembersRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ListMembersResponse>.Fail("Access token not found.");

        var url = $"{BaseUrl}/{request.SpaceName}/members?pageSize={request.PageSize}";
        if (!string.IsNullOrEmpty(request.PageToken)) url += $"&pageToken={Uri.EscapeDataString(request.PageToken)}";
        if (!string.IsNullOrEmpty(request.Filter)) url += $"&filter={Uri.EscapeDataString(request.Filter)}";

        var response = await _apiClient.GetAsync<ListMembersResponse>(url, accessToken);
        if (response == null) return Result<ListMembersResponse>.Fail("No response from Chat API.");

        return Result<ListMembersResponse>.Ok(response);
    }

    public async Task<Result<MemberResponse>> GetMemberAsync(string userId, string membershipName)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<MemberResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<MemberResponse>($"{BaseUrl}/{membershipName}", accessToken);
        if (response == null) return Result<MemberResponse>.Fail("Member not found.");

        return Result<MemberResponse>.Ok(response);
    }

    public async Task<Result<MemberResponse>> CreateMemberAsync(string userId, CreateMemberRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<MemberResponse>.Fail("Access token not found.");

        var body = new { member = new { name = $"users/{request.UserId}", type = "HUMAN" } };
        var response = await _apiClient.PostAsync<MemberResponse>($"{BaseUrl}/{request.SpaceName}/members", body, accessToken);
        if (response == null) return Result<MemberResponse>.Fail("Failed to create member.");

        return Result<MemberResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteMemberAsync(string userId, DeleteMemberRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        await _apiClient.DeleteAsync($"{BaseUrl}/{request.MembershipName}", accessToken);
        return Result<bool>.Ok(true);
    }
}
