using ApiToolKit.Application.DTOs.CampaignManager.AdvertiserLandingPage;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerAdvertiserLandingPageService : CampaignManagerServiceBase, ICampaignManagerAdvertiserLandingPageService
{
    public CampaignManagerAdvertiserLandingPageService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetAdvertiserLandingPageAsync(GetAdvertiserLandingPageRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/advertiserLandingPages/{Esc(r.AdvertiserLandingPageId)}";
        return await SendAsync(r.AccountKey, "GetAdvertiserLandingPage", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateAdvertiserLandingPageAsync(UpdateAdvertiserLandingPageRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/advertiserLandingPages";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "UpdateAdvertiserLandingPage", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> InsertAdvertiserLandingPageAsync(InsertAdvertiserLandingPageRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/advertiserLandingPages";
        return await SendAsync(r.AccountKey, "InsertAdvertiserLandingPage", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> UpdateAdvertiserLandingPageAltAsync(UpdateAdvertiserLandingPageAltRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/advertiserLandingPages";
        return await SendAsync(r.AccountKey, "UpdateAdvertiserLandingPageAlt", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> ListAdvertiserLandingPagesAsync(ListAdvertiserLandingPagesRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/advertiserLandingPages";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "searchString", r.SearchString);
        AddMany(query, "campaignIds", r.CampaignIds);
        Add(query, "maxResults", r.MaxResults?.ToString());
        Add(query, "subaccountId", r.SubaccountId);
        Add(query, "archived", r.Archived?.ToString().ToLower());
        AddMany(query, "ids", r.Ids);
        Add(query, "sortField", r.SortField);
        Add(query, "sortOrder", r.SortOrder);
        AddMany(query, "advertiserIds", r.AdvertiserIds);
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListAdvertiserLandingPages", HttpMethod.Get, url, query);
    }
}
