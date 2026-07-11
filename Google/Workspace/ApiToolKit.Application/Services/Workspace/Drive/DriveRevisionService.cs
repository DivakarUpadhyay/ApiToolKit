using ApiToolKit.Application.DTOs.Workspace.Drive;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Workspace.Drive;

public class DriveRevisionService : IDriveRevisionService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly IWorkspaceApiClient _apiClient;
    private const string BaseUrl = "https://www.googleapis.com/drive/v3/files";

    public DriveRevisionService(IWorkspaceAuthService authService, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _apiClient = apiClient;
    }

    public async Task<Result<ListRevisionsResponse>> ListRevisionsAsync(string userId, string fileId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<ListRevisionsResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<ListRevisionsResponse>($"{BaseUrl}/{fileId}/revisions", accessToken);
        if (response == null) return Result<ListRevisionsResponse>.Fail("No response from Drive API.");

        return Result<ListRevisionsResponse>.Ok(response);
    }

    public async Task<Result<RevisionResponse>> GetRevisionAsync(string userId, string fileId, string revisionId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<RevisionResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<RevisionResponse>($"{BaseUrl}/{fileId}/revisions/{revisionId}", accessToken);
        if (response == null) return Result<RevisionResponse>.Fail("Revision not found.");

        return Result<RevisionResponse>.Ok(response);
    }

    public async Task<Result<RevisionResponse>> UpdateRevisionAsync(string userId, UpdateRevisionRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<RevisionResponse>.Fail("Access token not found.");

        var body = new { keepForever = request.KeepForever, published = request.Published, publishAuto = request.PublishAuto };
        var response = await _apiClient.PatchAsync<RevisionResponse>($"{BaseUrl}/{request.FileId}/revisions/{request.RevisionId}", body, accessToken);
        if (response == null) return Result<RevisionResponse>.Fail("Failed to update revision.");

        return Result<RevisionResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteRevisionAsync(string userId, DeleteRevisionRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        await _apiClient.DeleteAsync($"{BaseUrl}/{request.FileId}/revisions/{request.RevisionId}", accessToken);
        return Result<bool>.Ok(true);
    }
}
