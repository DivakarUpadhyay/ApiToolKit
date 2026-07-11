using ApiToolKit.Application.DTOs.Identity.Toolkit;
using ApiToolKit.Application.Interfaces.Services.Identity;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Identity;

namespace ApiToolKit.Application.Services.Identity;

public class AdminUserService : IAdminUserService
{
    private readonly IIdentityApiClient _apiClient;
    private readonly IIdentityAuthRepository _authRepo;
    private readonly IIdentityUserRepository _userRepo;

    public AdminUserService(IIdentityApiClient apiClient, IIdentityAuthRepository authRepo, IIdentityUserRepository userRepo)
    {
        _apiClient = apiClient;
        _authRepo = authRepo;
        _userRepo = userRepo;
    }

    private async Task<(string projectId, string apiKey)> GetConfigAsync()
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) throw new InvalidOperationException("Identity config not found.");
        return (config.ProjectId, config.ApiKey);
    }

    private string AdminUrl(string projectId, string apiKey, string path) =>
        $"https://identitytoolkit.googleapis.com/v1/projects/{projectId}/{path}?key={apiKey}";

    public async Task<Result<ListUsersResponse>> ListUsersAsync(ListUsersRequest request, string accessToken)
    {
        try
        {
            var (projectId, apiKey) = await GetConfigAsync();
            var body = new { maxResults = request.PageSize, nextPageToken = request.NextPageToken };
            var response = await _apiClient.PostAsync<ListUsersResponse>(
                AdminUrl(projectId, apiKey, "accounts:batchGet"), body, accessToken);
            if (response == null) return Result<ListUsersResponse>.Fail("No response from Admin API.");
            return Result<ListUsersResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<ListUsersResponse>.Fail($"ListUsers failed: {ex.Message}");
        }
    }

    public async Task<Result<UserInfo>> GetUserAsync(AdminGetUserRequest request, string accessToken)
    {
        try
        {
            var (projectId, apiKey) = await GetConfigAsync();
            object body = request.LocalId != null
                ? new { localId = new[] { request.LocalId } }
                : new { email = new[] { request.Email } };

            var response = await _apiClient.PostAsync<GetAccountInfoResponse>(
                AdminUrl(projectId, apiKey, "accounts:lookup"), body, accessToken);
            if (response?.Users == null || response.Users.Count == 0)
                return Result<UserInfo>.Fail("User not found.");
            return Result<UserInfo>.Ok(response.Users[0]);
        }
        catch (Exception ex)
        {
            return Result<UserInfo>.Fail($"GetUser failed: {ex.Message}");
        }
    }

    public async Task<Result<UserInfo>> CreateUserAsync(AdminCreateUserRequest request, string accessToken)
    {
        try
        {
            var (projectId, apiKey) = await GetConfigAsync();
            var body = new
            {
                email = request.Email,
                password = request.Password,
                displayName = request.DisplayName,
                photoUrl = request.PhotoUrl,
                phoneNumber = request.PhoneNumber,
                emailVerified = request.EmailVerified,
                disabled = request.Disabled
            };
            var response = await _apiClient.PostAsync<UserInfo>(
                AdminUrl(projectId, apiKey, "accounts"), body, accessToken);
            if (response == null) return Result<UserInfo>.Fail("No response from Admin API.");
            return Result<UserInfo>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<UserInfo>.Fail($"CreateUser failed: {ex.Message}");
        }
    }

    public async Task<Result<UserInfo>> UpdateUserAsync(AdminUpdateUserRequest request, string accessToken)
    {
        try
        {
            var (projectId, apiKey) = await GetConfigAsync();
            var body = new
            {
                localId = request.LocalId,
                email = request.Email,
                emailVerified = request.EmailVerified,
                displayName = request.DisplayName,
                photoUrl = request.PhotoUrl,
                password = request.Password,
                phoneNumber = request.PhoneNumber,
                disableUser = request.Disabled
            };
            var response = await _apiClient.PostAsync<UserInfo>(
                AdminUrl(projectId, apiKey, "accounts:update"), body, accessToken);
            if (response == null) return Result<UserInfo>.Fail("No response from Admin API.");
            return Result<UserInfo>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<UserInfo>.Fail($"UpdateUser failed: {ex.Message}");
        }
    }

    public async Task<Result<bool>> DeleteUserAsync(AdminDeleteUserRequest request, string accessToken)
    {
        try
        {
            var (projectId, apiKey) = await GetConfigAsync();
            var body = new { localId = request.LocalId };
            await _apiClient.PostAsync<object>(
                AdminUrl(projectId, apiKey, "accounts:delete"), body, accessToken);
            await _userRepo.DeleteUserAsync(request.LocalId);
            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Fail($"DeleteUser failed: {ex.Message}");
        }
    }
}
