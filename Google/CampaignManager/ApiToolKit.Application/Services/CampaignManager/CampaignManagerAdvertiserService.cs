using ApiToolKit.Application.DTOs.CampaignManager.Advertiser;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerAdvertiserService : CampaignManagerServiceBase, ICampaignManagerAdvertiserService
{
    public CampaignManagerAdvertiserService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> InsertAdvertiserAsync(InsertAdvertiserRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/advertisers";
        return await SendAsync(r.AccountKey, "InsertAdvertiser", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> GetAdvertiserAsync(GetAdvertiserRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/advertisers/{Esc(r.AdvertiserId)}";
        return await SendAsync(r.AccountKey, "GetAdvertiser", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateAdvertiserAsync(UpdateAdvertiserRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/advertisers";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "UpdateAdvertiser", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> UpdateAdvertiserAltAsync(UpdateAdvertiserAltRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/advertisers";
        return await SendAsync(r.AccountKey, "UpdateAdvertiserAlt", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> ListAdvertisersAsync(ListAdvertisersRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/advertisers";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "sortOrder", r.SortOrder);
        Add(query, "subaccountId", r.SubaccountId);
        Add(query, "includeAdvertisersWithoutGroupsOnly", r.IncludeAdvertisersWithoutGroupsOnly?.ToString().ToLower());
        Add(query, "searchString", r.SearchString);
        Add(query, "sortField", r.SortField);
        Add(query, "maxResults", r.MaxResults?.ToString());
        AddMany(query, "floodlightConfigurationIds", r.FloodlightConfigurationIds);
        Add(query, "onlyParent", r.OnlyParent?.ToString().ToLower());
        Add(query, "status", r.Status);
        AddMany(query, "advertiserGroupIds", r.AdvertiserGroupIds);
        Add(query, "pageToken", r.PageToken);
        AddMany(query, "ids", r.Ids);
        return await SendAsync(r.AccountKey, "ListAdvertisers", HttpMethod.Get, url, query);
    }
}
