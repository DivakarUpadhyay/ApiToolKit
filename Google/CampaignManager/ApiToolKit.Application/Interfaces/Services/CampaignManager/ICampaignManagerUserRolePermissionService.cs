using ApiToolKit.Application.DTOs.CampaignManager.UserRolePermission;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerUserRolePermissionService
{
    Task<Result<object>> GetUserRolePermissionAsync(GetUserRolePermissionRequest request);
    Task<Result<object>> ListUserRolePermissionsAsync(ListUserRolePermissionsRequest request);
}
