using ApiToolKit.Application.DTOs.CampaignManager.Ad;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerAdService : CampaignManagerServiceBase, ICampaignManagerAdService
{
    public CampaignManagerAdService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> UpdateAdAsync(UpdateAdRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/ads";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "UpdateAd", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> ListAdsAsync(ListAdsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/ads";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "sslRequired", r.SslRequired?.ToString().ToLower());
        AddMany(query, "placementIds", r.PlacementIds);
        Add(query, "pageToken", r.PageToken);
        AddMany(query, "type", r.Type);
        Add(query, "archived", r.Archived?.ToString().ToLower());
        Add(query, "maxResults", r.MaxResults?.ToString());
        Add(query, "sslCompliant", r.SslCompliant?.ToString().ToLower());
        AddMany(query, "creativeOptimizationConfigurationIds", r.CreativeOptimizationConfigurationIds);
        AddMany(query, "landingPageIds", r.LandingPageIds);
        AddMany(query, "sizeIds", r.SizeIds);
        AddMany(query, "audienceSegmentIds", r.AudienceSegmentIds);
        AddMany(query, "ids", r.Ids);
        Add(query, "active", r.Active?.ToString().ToLower());
        Add(query, "sortField", r.SortField);
        Add(query, "overriddenEventTagId", r.OverriddenEventTagId);
        Add(query, "sortOrder", r.SortOrder);
        AddMany(query, "remarketingListIds", r.RemarketingListIds);
        AddMany(query, "creativeIds", r.CreativeIds);
        Add(query, "dynamicClickTracker", r.DynamicClickTracker?.ToString().ToLower());
        AddMany(query, "campaignIds", r.CampaignIds);
        Add(query, "compatibility", r.Compatibility);
        Add(query, "searchString", r.SearchString);
        return await SendAsync(r.AccountKey, "ListAds", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetAdAsync(GetAdRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/ads/{Esc(r.AdId)}";
        return await SendAsync(r.AccountKey, "GetAd", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateAdAltAsync(UpdateAdAltRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/ads";
        return await SendAsync(r.AccountKey, "UpdateAdAlt", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> InsertAdAsync(InsertAdRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/ads";
        return await SendAsync(r.AccountKey, "InsertAd", HttpMethod.Post, url, null, r.Body);
    }
}
