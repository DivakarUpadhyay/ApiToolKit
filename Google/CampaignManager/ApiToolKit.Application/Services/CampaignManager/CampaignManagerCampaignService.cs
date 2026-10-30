using ApiToolKit.Application.DTOs.CampaignManager.Campaign;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerCampaignService : CampaignManagerServiceBase, ICampaignManagerCampaignService
{
    public CampaignManagerCampaignService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> InsertCampaignAsync(InsertCampaignRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/campaigns";
        return await SendAsync(r.AccountKey, "InsertCampaign", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> ListCampaignsAsync(ListCampaignsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/campaigns";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        Add(query, "atLeastOneOptimizationActivity", r.AtLeastOneOptimizationActivity?.ToString().ToLower());
        AddMany(query, "advertiserGroupIds", r.AdvertiserGroupIds);
        Add(query, "searchString", r.SearchString);
        AddMany(query, "excludedIds", r.ExcludedIds);
        Add(query, "archived", r.Archived?.ToString().ToLower());
        Add(query, "subaccountId", r.SubaccountId);
        AddMany(query, "ids", r.Ids);
        Add(query, "sortOrder", r.SortOrder);
        Add(query, "sortField", r.SortField);
        Add(query, "maxResults", r.MaxResults?.ToString());
        AddMany(query, "advertiserIds", r.AdvertiserIds);
        Add(query, "overriddenEventTagId", r.OverriddenEventTagId);
        return await SendAsync(r.AccountKey, "ListCampaigns", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetCampaignAsync(GetCampaignRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/campaigns/{Esc(r.CampaignId)}";
        return await SendAsync(r.AccountKey, "GetCampaign", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateCampaignAsync(UpdateCampaignRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/campaigns";
        return await SendAsync(r.AccountKey, "UpdateCampaign", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> PatchCampaignAsync(PatchCampaignRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/campaigns";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "PatchCampaign", HttpMethod.Patch, url, query, r.Body);
    }
}
