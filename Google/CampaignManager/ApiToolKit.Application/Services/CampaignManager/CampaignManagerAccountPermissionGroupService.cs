using ApiToolKit.Application.DTOs.CampaignManager.AccountPermissionGroup;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerAccountPermissionGroupService : CampaignManagerServiceBase, ICampaignManagerAccountPermissionGroupService
{
    public CampaignManagerAccountPermissionGroupService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetAccountPermissionGroupAsync(GetAccountPermissionGroupRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/accountPermissionGroups/{Esc(r.AccountPermissionGroupId)}";
        return await SendAsync(r.AccountKey, "GetAccountPermissionGroup", HttpMethod.Get, url);
    }

    public async Task<Result<object>> ListAccountPermissionGroupsAsync(ListAccountPermissionGroupsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/accountPermissionGroups";
        return await SendAsync(r.AccountKey, "ListAccountPermissionGroups", HttpMethod.Get, url);
    }
}
