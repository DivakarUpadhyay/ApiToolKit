using ApiToolKit.Application.DTOs.CampaignManager.PlatformType;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerPlatformTypeService : CampaignManagerServiceBase, ICampaignManagerPlatformTypeService
{
    public CampaignManagerPlatformTypeService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetPlatformTypeAsync(GetPlatformTypeRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/platformTypes/{Esc(r.PlatformTypeId)}";
        return await SendAsync(r.AccountKey, "GetPlatformType", HttpMethod.Get, url);
    }

    public async Task<Result<object>> ListPlatformTypesAsync(ListPlatformTypesRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/platformTypes";
        return await SendAsync(r.AccountKey, "ListPlatformTypes", HttpMethod.Get, url);
    }
}
