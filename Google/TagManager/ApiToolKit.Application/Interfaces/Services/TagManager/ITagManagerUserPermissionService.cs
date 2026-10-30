using ApiToolKit.Application.DTOs.TagManager.UserPermission;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.TagManager;

public interface ITagManagerUserPermissionService
{
    Task<Result<object>> ListUserPermissionsAsync(ListUserPermissionsRequest request);
    Task<Result<object>> GetUserPermissionAsync(GetUserPermissionRequest request);
    Task<Result<object>> CreateUserPermissionAsync(CreateUserPermissionRequest request);
    Task<Result<object>> UpdateUserPermissionAsync(UpdateUserPermissionRequest request);
    Task<Result<object>> DeleteUserPermissionAsync(DeleteUserPermissionRequest request);
}
