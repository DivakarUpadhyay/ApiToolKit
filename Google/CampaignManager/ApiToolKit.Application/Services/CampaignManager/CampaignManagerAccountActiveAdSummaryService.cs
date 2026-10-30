using ApiToolKit.Application.DTOs.CampaignManager.AccountActiveAdSummary;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerAccountActiveAdSummaryService : CampaignManagerServiceBase, ICampaignManagerAccountActiveAdSummaryService
{
    public CampaignManagerAccountActiveAdSummaryService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetAccountActiveAdSummaryAsync(GetAccountActiveAdSummaryRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/accountActiveAdSummaries/{Esc(r.AccountActiveAdSummaryId)}";
        return await SendAsync(r.AccountKey, "GetAccountActiveAdSummary", HttpMethod.Get, url);
    }
}
