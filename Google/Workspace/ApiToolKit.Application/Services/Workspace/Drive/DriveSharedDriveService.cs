using ApiToolKit.Application.DTOs.Workspace.Drive;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Workspace.Drive;

public class DriveSharedDriveService : IDriveSharedDriveService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly IWorkspaceApiClient _apiClient;
    private const string BaseUrl = "https://www.googleapis.com/drive/v3/drives";

    public DriveSharedDriveService(IWorkspaceAuthService authService, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _apiClient = apiClient;
    }

    public async Task<Result<ListSharedDrivesResponse>> ListSharedDrivesAsync(string userId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ListSharedDrivesResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<ListSharedDrivesResponse>(BaseUrl, accessToken);
        if (response == null) return Result<ListSharedDrivesResponse>.Fail("No response from Drive API.");

        return Result<ListSharedDrivesResponse>.Ok(response);
    }

    public async Task<Result<SharedDriveResponse>> GetSharedDriveAsync(string userId, string driveId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<SharedDriveResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<SharedDriveResponse>($"{BaseUrl}/{driveId}", accessToken);
        if (response == null) return Result<SharedDriveResponse>.Fail("Shared drive not found.");

        return Result<SharedDriveResponse>.Ok(response);
    }

    public async Task<Result<SharedDriveResponse>> CreateSharedDriveAsync(string userId, CreateSharedDriveRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<SharedDriveResponse>.Fail("Access token not found.");

        var requestId = request.RequestId ?? Guid.NewGuid().ToString();
        var body = new { name = request.Name };
        var response = await _apiClient.PostAsync<SharedDriveResponse>($"{BaseUrl}?requestId={requestId}", body, accessToken);
        if (response == null) return Result<SharedDriveResponse>.Fail("Failed to create shared drive.");

        return Result<SharedDriveResponse>.Ok(response);
    }

    public async Task<Result<SharedDriveResponse>> UpdateSharedDriveAsync(string userId, UpdateSharedDriveRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<SharedDriveResponse>.Fail("Access token not found.");

        var body = new { name = request.Name, colorRgb = request.ColorRgb };
        var response = await _apiClient.PatchAsync<SharedDriveResponse>($"{BaseUrl}/{request.DriveId}", body, accessToken);
        if (response == null) return Result<SharedDriveResponse>.Fail("Failed to update shared drive.");

        return Result<SharedDriveResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteSharedDriveAsync(string userId, string driveId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        await _apiClient.DeleteAsync($"{BaseUrl}/{driveId}", accessToken);
        return Result<bool>.Ok(true);
    }
}
