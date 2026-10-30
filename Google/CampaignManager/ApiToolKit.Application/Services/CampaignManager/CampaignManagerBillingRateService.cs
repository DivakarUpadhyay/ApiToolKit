using ApiToolKit.Application.DTOs.CampaignManager.BillingRate;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerBillingRateService : CampaignManagerServiceBase, ICampaignManagerBillingRateService
{
    public CampaignManagerBillingRateService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListBillingRatesAsync(ListBillingRatesRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/billingProfiles/{Esc(r.BillingProfileId)}/billingRates";
        return await SendAsync(r.AccountKey, "ListBillingRates", HttpMethod.Get, url);
    }
}
