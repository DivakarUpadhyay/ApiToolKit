using ApiToolKit.Application.DTOs.Workspace.Drive;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Workspace.Drive;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;

namespace ApiToolKit.Application.Services.Workspace.Drive;

public class DrivePermissionService : IDrivePermissionService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly IDriveRepository _driveRepo;
    private readonly IWorkspaceApiClient _apiClient;
    private const string BaseUrl = "https://www.googleapis.com/drive/v3/files";

    public DrivePermissionService(IWorkspaceAuthService authService, IDriveRepository driveRepo, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _driveRepo = driveRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListPermissionsResponse>> ListPermissionsAsync(string userId, string fileId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ListPermissionsResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<ListPermissionsResponse>($"{BaseUrl}/{fileId}/permissions", accessToken);
        if (response == null) return Result<ListPermissionsResponse>.Fail("No response from Drive API.");

        return Result<ListPermissionsResponse>.Ok(response);
    }

    public async Task<Result<PermissionResponse>> GetPermissionAsync(string userId, string fileId, string permissionId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<PermissionResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<PermissionResponse>($"{BaseUrl}/{fileId}/permissions/{permissionId}", accessToken);
        if (response == null) return Result<PermissionResponse>.Fail("Permission not found.");

        return Result<PermissionResponse>.Ok(response);
    }

    public async Task<Result<PermissionResponse>> CreatePermissionAsync(string userId, CreatePermissionRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<PermissionResponse>.Fail("Access token not found.");

        var body = new
        {
            type = request.Type,
            role = request.Role,
            emailAddress = request.EmailAddress,
            domain = request.Domain,
            expirationTime = request.ExpirationTime
        };
        var url = $"{BaseUrl}/{request.FileId}/permissions?sendNotificationEmail={request.SendNotificationEmail.ToString().ToLower()}&transferOwnership={request.TransferOwnership.ToString().ToLower()}";

        var response = await _apiClient.PostAsync<PermissionResponse>(url, body, accessToken);
        if (response == null) return Result<PermissionResponse>.Fail("Failed to create permission.");

        if (response.Id != null)
        {
            await _driveRepo.SavePermissionAsync(new DrivePermission
            {
                PermissionId = response.Id,
                FileId = request.FileId,
                UserId = userId,
                Type = response.Type,
                Role = response.Role,
                EmailAddress = response.EmailAddress,
                DisplayName = response.DisplayName,
                ExpirationTime = response.ExpirationTime,
                CreatedAt = DateTime.UtcNow
            });
        }

        return Result<PermissionResponse>.Ok(response);
    }

    public async Task<Result<PermissionResponse>> UpdatePermissionAsync(string userId, UpdatePermissionRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<PermissionResponse>.Fail("Access token not found.");

        var body = new { role = request.Role, expirationTime = request.ExpirationTime };
        var url = $"{BaseUrl}/{request.FileId}/permissions/{request.PermissionId}?removeExpiration={request.RemoveExpiration.ToString().ToLower()}";

        var response = await _apiClient.PatchAsync<PermissionResponse>(url, body, accessToken);
        if (response == null) return Result<PermissionResponse>.Fail("Failed to update permission.");

        return Result<PermissionResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeletePermissionAsync(string userId, DeletePermissionRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        await _apiClient.DeleteAsync($"{BaseUrl}/{request.FileId}/permissions/{request.PermissionId}", accessToken);
        return Result<bool>.Ok(true);
    }
}
