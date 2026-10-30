using ApiToolKit.Application.DTOs.CampaignManager.Browser;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerBrowserService : CampaignManagerServiceBase, ICampaignManagerBrowserService
{
    public CampaignManagerBrowserService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListBrowsersAsync(ListBrowsersRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/browsers";
        return await SendAsync(r.AccountKey, "ListBrowsers", HttpMethod.Get, url);
    }
}
