using ApiToolKit.Application.DTOs.CampaignManager.Metro;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerMetroService : CampaignManagerServiceBase, ICampaignManagerMetroService
{
    public CampaignManagerMetroService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListMetrosAsync(ListMetrosRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/metros";
        return await SendAsync(r.AccountKey, "ListMetros", HttpMethod.Get, url);
    }
}
