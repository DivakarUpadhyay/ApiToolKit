using ApiToolKit.Application.DTOs.CampaignManager.MobileApp;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerMobileAppService : CampaignManagerServiceBase, ICampaignManagerMobileAppService
{
    public CampaignManagerMobileAppService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetMobileAppAsync(GetMobileAppRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/mobileApps/{Esc(r.MobileAppId)}";
        return await SendAsync(r.AccountKey, "GetMobileApp", HttpMethod.Get, url);
    }

    public async Task<Result<object>> ListMobileAppsAsync(ListMobileAppsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/mobileApps";
        var query = new List<KeyValuePair<string, string?>>();
        AddMany(query, "directories", r.Directories);
        Add(query, "maxResults", r.MaxResults?.ToString());
        Add(query, "searchString", r.SearchString);
        Add(query, "pageToken", r.PageToken);
        AddMany(query, "ids", r.Ids);
        return await SendAsync(r.AccountKey, "ListMobileApps", HttpMethod.Get, url, query);
    }
}
