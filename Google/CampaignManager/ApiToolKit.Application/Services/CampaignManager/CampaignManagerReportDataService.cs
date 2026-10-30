using ApiToolKit.Application.DTOs.CampaignManager.ReportData;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerReportDataService : CampaignManagerServiceBase, ICampaignManagerReportDataService
{
    public CampaignManagerReportDataService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> QueryReportDataAsync(QueryReportDataRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.ProfileId)}/reportdata/query";
        return await SendAsync(r.AccountKey, "QueryReportData", HttpMethod.Post, url, null, r.Body);
    }
}
