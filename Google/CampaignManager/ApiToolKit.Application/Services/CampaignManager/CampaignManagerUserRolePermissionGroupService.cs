using ApiToolKit.Application.DTOs.CampaignManager.UserRolePermissionGroup;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerUserRolePermissionGroupService : CampaignManagerServiceBase, ICampaignManagerUserRolePermissionGroupService
{
    public CampaignManagerUserRolePermissionGroupService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListUserRolePermissionGroupsAsync(ListUserRolePermissionGroupsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/userRolePermissionGroups";
        return await SendAsync(r.AccountKey, "ListUserRolePermissionGroups", HttpMethod.Get, url);
    }

    public async Task<Result<object>> GetUserRolePermissionGroupAsync(GetUserRolePermissionGroupRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/userRolePermissionGroups/{Esc(r.UserRolePermissionGroupId)}";
        return await SendAsync(r.AccountKey, "GetUserRolePermissionGroup", HttpMethod.Get, url);
    }
}
