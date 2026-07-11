using ApiToolKit.Application.DTOs.Workspace.Drive;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface IDriveSharedDriveService
{
    Task<Result<ListSharedDrivesResponse>> ListSharedDrivesAsync(string userId);
    Task<Result<SharedDriveResponse>> GetSharedDriveAsync(string userId, string driveId);
    Task<Result<SharedDriveResponse>> CreateSharedDriveAsync(string userId, CreateSharedDriveRequest request);
    Task<Result<SharedDriveResponse>> UpdateSharedDriveAsync(string userId, UpdateSharedDriveRequest request);
    Task<Result<bool>> DeleteSharedDriveAsync(string userId, string driveId);
}
