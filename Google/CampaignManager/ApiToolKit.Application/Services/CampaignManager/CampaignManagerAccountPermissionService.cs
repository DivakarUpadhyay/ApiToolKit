using ApiToolKit.Application.DTOs.CampaignManager.AccountPermission;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerAccountPermissionService : CampaignManagerServiceBase, ICampaignManagerAccountPermissionService
{
    public CampaignManagerAccountPermissionService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetAccountPermissionAsync(GetAccountPermissionRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/accountPermissions/{Esc(r.AccountPermissionId)}";
        return await SendAsync(r.AccountKey, "GetAccountPermission", HttpMethod.Get, url);
    }

    public async Task<Result<object>> ListAccountPermissionsAsync(ListAccountPermissionsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/accountPermissions";
        return await SendAsync(r.AccountKey, "ListAccountPermissions", HttpMethod.Get, url);
    }
}
