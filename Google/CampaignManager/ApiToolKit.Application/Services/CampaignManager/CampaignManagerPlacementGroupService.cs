using ApiToolKit.Application.DTOs.CampaignManager.PlacementGroup;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerPlacementGroupService : CampaignManagerServiceBase, ICampaignManagerPlacementGroupService
{
    public CampaignManagerPlacementGroupService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> UpdatePlacementGroupAsync(UpdatePlacementGroupRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/placementGroups";
        return await SendAsync(r.AccountKey, "UpdatePlacementGroup", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> GetPlacementGroupAsync(GetPlacementGroupRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/placementGroups/{Esc(r.PlacementGroupId)}";
        return await SendAsync(r.AccountKey, "GetPlacementGroup", HttpMethod.Get, url);
    }

    public async Task<Result<object>> PatchPlacementGroupAsync(PatchPlacementGroupRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/placementGroups";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "PatchPlacementGroup", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> InsertPlacementGroupAsync(InsertPlacementGroupRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/placementGroups";
        return await SendAsync(r.AccountKey, "InsertPlacementGroup", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> ListPlacementGroupsAsync(ListPlacementGroupsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/placementGroups";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "sortField", r.SortField);
        AddMany(query, "ids", r.Ids);
        AddMany(query, "contentCategoryIds", r.ContentCategoryIds);
        Add(query, "maxResults", r.MaxResults?.ToString());
        AddMany(query, "activeStatus", r.ActiveStatus);
        AddMany(query, "siteIds", r.SiteIds);
        AddMany(query, "pricingTypes", r.PricingTypes);
        AddMany(query, "placementStrategyIds", r.PlacementStrategyIds);
        Add(query, "sortOrder", r.SortOrder);
        AddMany(query, "directorySiteIds", r.DirectorySiteIds);
        Add(query, "minEndDate", r.MinEndDate);
        Add(query, "minStartDate", r.MinStartDate);
        Add(query, "placementGroupType", r.PlacementGroupType);
        AddMany(query, "campaignIds", r.CampaignIds);
        AddMany(query, "advertiserIds", r.AdvertiserIds);
        Add(query, "pageToken", r.PageToken);
        Add(query, "searchString", r.SearchString);
        Add(query, "maxStartDate", r.MaxStartDate);
        Add(query, "maxEndDate", r.MaxEndDate);
        return await SendAsync(r.AccountKey, "ListPlacementGroups", HttpMethod.Get, url, query);
    }
}
