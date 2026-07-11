using ApiToolKit.Application.DTOs.Workspace.Drive;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface IDriveFileService
{
    Task<Result<FileListResponse>> ListFilesAsync(string userId, ListFilesRequest request);
    Task<Result<FileResponse>> GetFileAsync(string userId, GetFileRequest request);
    Task<Result<FileResponse>> CreateFolderAsync(string userId, CreateFolderRequest request);
    Task<Result<FileResponse>> CreateFileMetadataAsync(string userId, CreateFileMetadataRequest request);
    Task<Result<FileResponse>> UpdateFileAsync(string userId, UpdateFileRequest request);
    Task<Result<FileResponse>> CopyFileAsync(string userId, CopyFileRequest request);
    Task<Result<FileResponse>> MoveFileAsync(string userId, MoveFileRequest request);
    Task<Result<bool>> DeleteFileAsync(string userId, DeleteFileRequest request);
    Task<Result<FileResponse>> TrashFileAsync(string userId, TrashFileRequest request);
    Task<Result<FileResponse>> UntrashFileAsync(string userId, string fileId);
    Task<Result<PermissionResponse>> ShareFileAsync(string userId, ShareFileRequest request);
}
