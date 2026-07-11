using ApiToolKit.Application.DTOs.Workspace.Drive;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Workspace.Drive;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;
using Newtonsoft.Json;
using System.Text;

namespace ApiToolKit.Application.Services.Workspace.Drive;

public class DriveFileService : IDriveFileService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly IDriveRepository _driveRepo;
    private readonly IWorkspaceApiClient _apiClient;
    private const string BaseUrl = "https://www.googleapis.com/drive/v3/files";

    public DriveFileService(IWorkspaceAuthService authService, IDriveRepository driveRepo, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _driveRepo = driveRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<FileListResponse>> ListFilesAsync(string userId, ListFilesRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<FileListResponse>.Fail("Access token not found.");

        var sb = new StringBuilder($"{BaseUrl}?pageSize={request.PageSize}");
        if (!string.IsNullOrEmpty(request.Q)) sb.Append($"&q={Uri.EscapeDataString(request.Q)}");
        if (!string.IsNullOrEmpty(request.PageToken)) sb.Append($"&pageToken={Uri.EscapeDataString(request.PageToken)}");
        if (!string.IsNullOrEmpty(request.OrderBy)) sb.Append($"&orderBy={Uri.EscapeDataString(request.OrderBy)}");
        if (!string.IsNullOrEmpty(request.Fields)) sb.Append($"&fields={Uri.EscapeDataString(request.Fields)}");
        if (request.IncludeItemsFromAllDrives) sb.Append("&includeItemsFromAllDrives=true&supportsAllDrives=true");
        if (!string.IsNullOrEmpty(request.DriveId)) sb.Append($"&driveId={Uri.EscapeDataString(request.DriveId)}");

        var response = await _apiClient.GetAsync<FileListResponse>(sb.ToString(), accessToken);
        if (response == null) return Result<FileListResponse>.Fail("No response from Drive API.");

        return Result<FileListResponse>.Ok(response);
    }

    public async Task<Result<FileResponse>> GetFileAsync(string userId, GetFileRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<FileResponse>.Fail("Access token not found.");

        var url = $"{BaseUrl}/{request.FileId}";
        if (!string.IsNullOrEmpty(request.Fields)) url += $"?fields={Uri.EscapeDataString(request.Fields)}";

        var response = await _apiClient.GetAsync<FileResponse>(url, accessToken);
        if (response == null) return Result<FileResponse>.Fail("File not found.");

        return Result<FileResponse>.Ok(response);
    }

    public async Task<Result<FileResponse>> CreateFolderAsync(string userId, CreateFolderRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<FileResponse>.Fail("Access token not found.");

        var body = new { name = request.Name, mimeType = "application/vnd.google-apps.folder", parents = request.Parents };
        var response = await _apiClient.PostAsync<FileResponse>(BaseUrl, body, accessToken);
        if (response == null) return Result<FileResponse>.Fail("Failed to create folder.");

        await SaveFileToRepo(userId, response);
        return Result<FileResponse>.Ok(response);
    }

    public async Task<Result<FileResponse>> CreateFileMetadataAsync(string userId, CreateFileMetadataRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<FileResponse>.Fail("Access token not found.");

        var body = new { name = request.Name, mimeType = request.MimeType, parents = request.Parents, description = request.Description };
        var response = await _apiClient.PostAsync<FileResponse>(BaseUrl, body, accessToken);
        if (response == null) return Result<FileResponse>.Fail("Failed to create file.");

        await SaveFileToRepo(userId, response);
        return Result<FileResponse>.Ok(response);
    }

    public async Task<Result<FileResponse>> UpdateFileAsync(string userId, UpdateFileRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<FileResponse>.Fail("Access token not found.");

        var url = $"{BaseUrl}/{request.FileId}";
        var queryParts = new List<string>();
        if (request.AddParents?.Any() == true) queryParts.Add($"addParents={string.Join(",", request.AddParents)}");
        if (request.RemoveParents?.Any() == true) queryParts.Add($"removeParents={string.Join(",", request.RemoveParents)}");
        if (queryParts.Any()) url += "?" + string.Join("&", queryParts);

        var body = new { name = request.Name, description = request.Description, starred = request.Starred };
        var response = await _apiClient.PatchAsync<FileResponse>(url, body, accessToken);
        if (response == null) return Result<FileResponse>.Fail("Failed to update file.");

        return Result<FileResponse>.Ok(response);
    }

    public async Task<Result<FileResponse>> CopyFileAsync(string userId, CopyFileRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<FileResponse>.Fail("Access token not found.");

        var body = new { name = request.Name, parents = request.Parents };
        var response = await _apiClient.PostAsync<FileResponse>($"{BaseUrl}/{request.FileId}/copy", body, accessToken);
        if (response == null) return Result<FileResponse>.Fail("Failed to copy file.");

        await SaveFileToRepo(userId, response);
        return Result<FileResponse>.Ok(response);
    }

    public async Task<Result<FileResponse>> MoveFileAsync(string userId, MoveFileRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<FileResponse>.Fail("Access token not found.");

        var getResult = await GetFileAsync(userId, new GetFileRequest { FileId = request.FileId, Fields = "parents" });
        if (!getResult.Success) return Result<FileResponse>.Fail("File not found.");

        var currentParents = getResult.Data?.Parents != null ? string.Join(",", getResult.Data.Parents) : string.Empty;
        var url = $"{BaseUrl}/{request.FileId}?addParents={request.NewParentId}&removeParents={currentParents}";

        var response = await _apiClient.PatchAsync<FileResponse>(url, new { }, accessToken);
        if (response == null) return Result<FileResponse>.Fail("Failed to move file.");

        return Result<FileResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteFileAsync(string userId, DeleteFileRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        await _apiClient.DeleteAsync($"{BaseUrl}/{request.FileId}", accessToken);
        await _driveRepo.DeleteFileAsync(request.FileId, userId);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<FileResponse>> TrashFileAsync(string userId, TrashFileRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<FileResponse>.Fail("Access token not found.");

        var body = new { trashed = true };
        var response = await _apiClient.PatchAsync<FileResponse>($"{BaseUrl}/{request.FileId}", body, accessToken);
        if (response == null) return Result<FileResponse>.Fail("Failed to trash file.");

        return Result<FileResponse>.Ok(response);
    }

    public async Task<Result<FileResponse>> UntrashFileAsync(string userId, string fileId)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<FileResponse>.Fail("Access token not found.");

        var body = new { trashed = false };
        var response = await _apiClient.PatchAsync<FileResponse>($"{BaseUrl}/{fileId}", body, accessToken);
        if (response == null) return Result<FileResponse>.Fail("Failed to untrash file.");

        return Result<FileResponse>.Ok(response);
    }

    public async Task<Result<PermissionResponse>> ShareFileAsync(string userId, ShareFileRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<PermissionResponse>.Fail("Access token not found.");

        var body = new { type = request.Type, role = request.Role, emailAddress = request.EmailAddress, domain = request.Domain };
        var url = $"{BaseUrl}/{request.FileId}/permissions?sendNotificationEmail={request.SendNotificationEmail.ToString().ToLower()}";

        var response = await _apiClient.PostAsync<PermissionResponse>(url, body, accessToken);
        if (response == null) return Result<PermissionResponse>.Fail("Failed to share file.");

        return Result<PermissionResponse>.Ok(response);
    }

    private async Task SaveFileToRepo(string userId, FileResponse file)
    {
        if (file.Id == null) return;
        await _driveRepo.SaveFileAsync(new DriveFile
        {
            FileId = file.Id,
            UserId = userId,
            Name = file.Name ?? string.Empty,
            MimeType = file.MimeType,
            Description = file.Description,
            Size = file.Size,
            ModifiedTime = file.ModifiedTime,
            CreatedTime = file.CreatedTime,
            WebViewLink = file.WebViewLink,
            WebContentLink = file.WebContentLink,
            Parents = file.Parents != null ? string.Join(",", file.Parents) : null,
            IsShared = file.Shared,
            IsStarred = file.Starred,
            IsTrashed = file.Trashed,
            Version = file.Version,
            CreatedAt = DateTime.UtcNow
        });
    }
}
