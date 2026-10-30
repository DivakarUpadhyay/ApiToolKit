using ApiToolKit.Application.DTOs.CampaignManager.TvCampaignSummary;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerTvCampaignSummaryService : CampaignManagerServiceBase, ICampaignManagerTvCampaignSummaryService
{
    public CampaignManagerTvCampaignSummaryService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListTvCampaignSummariesAsync(ListTvCampaignSummariesRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/tvCampaignSummaries";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "countryDartId", r.CountryDartId);
        Add(query, "name", r.Name);
        Add(query, "accountId", r.AccountId);
        Add(query, "tvDataProvider", r.TvDataProvider);
        return await SendAsync(r.AccountKey, "ListTvCampaignSummaries", HttpMethod.Get, url, query);
    }
}
