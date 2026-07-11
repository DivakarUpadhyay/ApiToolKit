using ApiToolKit.Application.DTOs.Workspace.Drive;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface IDrivePermissionService
{
    Task<Result<ListPermissionsResponse>> ListPermissionsAsync(string userId, string fileId);
    Task<Result<PermissionResponse>> GetPermissionAsync(string userId, string fileId, string permissionId);
    Task<Result<PermissionResponse>> CreatePermissionAsync(string userId, CreatePermissionRequest request);
    Task<Result<PermissionResponse>> UpdatePermissionAsync(string userId, UpdatePermissionRequest request);
    Task<Result<bool>> DeletePermissionAsync(string userId, DeletePermissionRequest request);
}
