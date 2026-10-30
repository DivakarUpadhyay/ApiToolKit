using ApiToolKit.Application.DTOs.CampaignManager.TvCampaignDetail;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerTvCampaignDetailService : CampaignManagerServiceBase, ICampaignManagerTvCampaignDetailService
{
    public CampaignManagerTvCampaignDetailService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetTvCampaignDetailAsync(GetTvCampaignDetailRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/tvCampaignDetails/{Esc(r.TvCampaignDetailId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "countryDartId", r.CountryDartId);
        Add(query, "accountId", r.AccountId);
        Add(query, "tvDataProvider", r.TvDataProvider);
        return await SendAsync(r.AccountKey, "GetTvCampaignDetail", HttpMethod.Get, url, query);
    }
}
