using ApiToolKit.Application.DTOs.CampaignManager.ReportCompatibleField;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerReportCompatibleFieldService : CampaignManagerServiceBase, ICampaignManagerReportCompatibleFieldService
{
    public CampaignManagerReportCompatibleFieldService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> QueryCompatibleFieldAsync(QueryCompatibleFieldRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.ProfileId)}/reports/compatiblefields/query";
        return await SendAsync(r.AccountKey, "QueryCompatibleField", HttpMethod.Post, url, null, r.Body);
    }
}
