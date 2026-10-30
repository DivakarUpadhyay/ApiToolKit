using ApiToolKit.Application.DTOs.CampaignManager.UserRolePermission;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerUserRolePermissionService : CampaignManagerServiceBase, ICampaignManagerUserRolePermissionService
{
    public CampaignManagerUserRolePermissionService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetUserRolePermissionAsync(GetUserRolePermissionRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/userRolePermissions/{Esc(r.UserRolePermissionId)}";
        return await SendAsync(r.AccountKey, "GetUserRolePermission", HttpMethod.Get, url);
    }

    public async Task<Result<object>> ListUserRolePermissionsAsync(ListUserRolePermissionsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/userRolePermissions";
        var query = new List<KeyValuePair<string, string?>>();
        AddMany(query, "ids", r.Ids);
        return await SendAsync(r.AccountKey, "ListUserRolePermissions", HttpMethod.Get, url, query);
    }
}
