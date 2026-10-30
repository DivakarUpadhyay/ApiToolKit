using ApiToolKit.Application.DTOs.CampaignManager.UserRolePermissionGroup;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerUserRolePermissionGroupService
{
    Task<Result<object>> ListUserRolePermissionGroupsAsync(ListUserRolePermissionGroupsRequest request);
    Task<Result<object>> GetUserRolePermissionGroupAsync(GetUserRolePermissionGroupRequest request);
}
