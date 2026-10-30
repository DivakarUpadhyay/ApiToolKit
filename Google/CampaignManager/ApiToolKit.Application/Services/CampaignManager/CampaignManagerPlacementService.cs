using ApiToolKit.Application.DTOs.CampaignManager.Placement;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerPlacementService : CampaignManagerServiceBase, ICampaignManagerPlacementService
{
    public CampaignManagerPlacementService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> UpdatePlacementAsync(UpdatePlacementRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/placements";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "UpdatePlacement", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> GetPlacementAsync(GetPlacementRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/placements/{Esc(r.PlacementId)}";
        return await SendAsync(r.AccountKey, "GetPlacement", HttpMethod.Get, url);
    }

    public async Task<Result<object>> InsertPlacementAsync(InsertPlacementRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/placements";
        return await SendAsync(r.AccountKey, "InsertPlacement", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> GeneratetagsPlacementAsync(GeneratetagsPlacementRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/placements/generatetags";
        var query = new List<KeyValuePair<string, string?>>();
        AddMany(query, "tagFormats", r.TagFormats);
        Add(query, "tagProperties.dcDbmMacroIncluded", r.TagPropertiesDcDbmMacroIncluded?.ToString().ToLower());
        Add(query, "tagProperties.gppMacrosIncluded", r.TagPropertiesGppMacrosIncluded?.ToString().ToLower());
        AddMany(query, "placementIds", r.PlacementIds);
        Add(query, "campaignId", r.CampaignId);
        Add(query, "tagProperties.tcfGdprMacrosIncluded", r.TagPropertiesTcfGdprMacrosIncluded?.ToString().ToLower());
        return await SendAsync(r.AccountKey, "GeneratetagsPlacement", HttpMethod.Post, url, query);
    }

    public async Task<Result<object>> UpdatePlacementAltAsync(UpdatePlacementAltRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/placements";
        return await SendAsync(r.AccountKey, "UpdatePlacementAlt", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> ListPlacementsAsync(ListPlacementsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/placements";
        var query = new List<KeyValuePair<string, string?>>();
        AddMany(query, "campaignIds", r.CampaignIds);
        Add(query, "pageToken", r.PageToken);
        Add(query, "maxResults", r.MaxResults?.ToString());
        AddMany(query, "ids", r.Ids);
        Add(query, "searchString", r.SearchString);
        Add(query, "maxStartDate", r.MaxStartDate);
        AddMany(query, "advertiserIds", r.AdvertiserIds);
        AddMany(query, "contentCategoryIds", r.ContentCategoryIds);
        Add(query, "minEndDate", r.MinEndDate);
        AddMany(query, "compatibilities", r.Compatibilities);
        Add(query, "sortOrder", r.SortOrder);
        AddMany(query, "sizeIds", r.SizeIds);
        AddMany(query, "pricingTypes", r.PricingTypes);
        AddMany(query, "placementStrategyIds", r.PlacementStrategyIds);
        AddMany(query, "activeStatus", r.ActiveStatus);
        Add(query, "sortField", r.SortField);
        AddMany(query, "siteIds", r.SiteIds);
        Add(query, "minStartDate", r.MinStartDate);
        Add(query, "maxEndDate", r.MaxEndDate);
        AddMany(query, "groupIds", r.GroupIds);
        Add(query, "paymentSource", r.PaymentSource);
        AddMany(query, "directorySiteIds", r.DirectorySiteIds);
        return await SendAsync(r.AccountKey, "ListPlacements", HttpMethod.Get, url, query);
    }
}
