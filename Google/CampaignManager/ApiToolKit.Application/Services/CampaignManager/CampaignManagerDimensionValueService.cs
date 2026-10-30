using ApiToolKit.Application.DTOs.CampaignManager.DimensionValue;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerDimensionValueService : CampaignManagerServiceBase, ICampaignManagerDimensionValueService
{
    public CampaignManagerDimensionValueService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> QueryDimensionValueAsync(QueryDimensionValueRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.ProfileId)}/dimensionvalues/query";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        Add(query, "maxResults", r.MaxResults?.ToString());
        return await SendAsync(r.AccountKey, "QueryDimensionValue", HttpMethod.Post, url, query, r.Body);
    }
}
