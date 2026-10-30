using ApiToolKit.Application.DTOs.CampaignManager.Region;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerRegionService : CampaignManagerServiceBase, ICampaignManagerRegionService
{
    public CampaignManagerRegionService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListRegionsAsync(ListRegionsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/regions";
        return await SendAsync(r.AccountKey, "ListRegions", HttpMethod.Get, url);
    }
}
