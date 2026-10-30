using ApiToolKit.Application.DTOs.CampaignManager.MobileCarrier;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerMobileCarrierService : CampaignManagerServiceBase, ICampaignManagerMobileCarrierService
{
    public CampaignManagerMobileCarrierService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetMobileCarrierAsync(GetMobileCarrierRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/mobileCarriers/{Esc(r.MobileCarrierId)}";
        return await SendAsync(r.AccountKey, "GetMobileCarrier", HttpMethod.Get, url);
    }

    public async Task<Result<object>> ListMobileCarriersAsync(ListMobileCarriersRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/mobileCarriers";
        return await SendAsync(r.AccountKey, "ListMobileCarriers", HttpMethod.Get, url);
    }
}
