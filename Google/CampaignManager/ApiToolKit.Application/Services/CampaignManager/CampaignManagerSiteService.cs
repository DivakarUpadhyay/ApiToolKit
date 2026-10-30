using ApiToolKit.Application.DTOs.CampaignManager.Site;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerSiteService : CampaignManagerServiceBase, ICampaignManagerSiteService
{
    public CampaignManagerSiteService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListSitesAsync(ListSitesRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/sites";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "approved", r.Approved?.ToString().ToLower());
        AddMany(query, "directorySiteIds", r.DirectorySiteIds);
        Add(query, "acceptsInterstitialPlacements", r.AcceptsInterstitialPlacements?.ToString().ToLower());
        AddMany(query, "campaignIds", r.CampaignIds);
        Add(query, "unmappedSite", r.UnmappedSite?.ToString().ToLower());
        Add(query, "pageToken", r.PageToken);
        Add(query, "subaccountId", r.SubaccountId);
        Add(query, "maxResults", r.MaxResults?.ToString());
        Add(query, "acceptsPublisherPaidPlacements", r.AcceptsPublisherPaidPlacements?.ToString().ToLower());
        Add(query, "searchString", r.SearchString);
        Add(query, "adWordsSite", r.AdWordsSite?.ToString().ToLower());
        AddMany(query, "ids", r.Ids);
        Add(query, "acceptsInStreamVideoPlacements", r.AcceptsInStreamVideoPlacements?.ToString().ToLower());
        Add(query, "sortField", r.SortField);
        Add(query, "sortOrder", r.SortOrder);
        return await SendAsync(r.AccountKey, "ListSites", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> InsertSiteAsync(InsertSiteRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/sites";
        return await SendAsync(r.AccountKey, "InsertSite", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> UpdateSiteAsync(UpdateSiteRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/sites";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "UpdateSite", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> GetSiteAsync(GetSiteRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/sites/{Esc(r.SiteId)}";
        return await SendAsync(r.AccountKey, "GetSite", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateSiteAltAsync(UpdateSiteAltRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/sites";
        return await SendAsync(r.AccountKey, "UpdateSiteAlt", HttpMethod.Put, url, null, r.Body);
    }
}
