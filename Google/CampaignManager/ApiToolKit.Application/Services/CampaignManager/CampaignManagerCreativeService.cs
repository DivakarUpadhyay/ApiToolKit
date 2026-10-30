using ApiToolKit.Application.DTOs.CampaignManager.Creative;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerCreativeService : CampaignManagerServiceBase, ICampaignManagerCreativeService
{
    public CampaignManagerCreativeService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListCreativesAsync(ListCreativesRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/creatives";
        var query = new List<KeyValuePair<string, string?>>();
        AddMany(query, "companionCreativeIds", r.CompanionCreativeIds);
        Add(query, "sortField", r.SortField);
        Add(query, "sortOrder", r.SortOrder);
        Add(query, "archived", r.Archived?.ToString().ToLower());
        Add(query, "studioCreativeId", r.StudioCreativeId);
        Add(query, "campaignId", r.CampaignId);
        AddMany(query, "sizeIds", r.SizeIds);
        Add(query, "advertiserId", r.AdvertiserId);
        AddMany(query, "ids", r.Ids);
        Add(query, "active", r.Active?.ToString().ToLower());
        Add(query, "searchString", r.SearchString);
        Add(query, "maxResults", r.MaxResults?.ToString());
        Add(query, "pageToken", r.PageToken);
        AddMany(query, "creativeFieldIds", r.CreativeFieldIds);
        AddMany(query, "types", r.Types);
        AddMany(query, "renderingIds", r.RenderingIds);
        return await SendAsync(r.AccountKey, "ListCreatives", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> UpdateCreativeAsync(UpdateCreativeRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/creatives";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "UpdateCreative", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> UpdateCreativeAltAsync(UpdateCreativeAltRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/creatives";
        return await SendAsync(r.AccountKey, "UpdateCreativeAlt", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> GetCreativeAsync(GetCreativeRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/creatives/{Esc(r.CreativeId)}";
        return await SendAsync(r.AccountKey, "GetCreative", HttpMethod.Get, url);
    }

    public async Task<Result<object>> InsertCreativeAsync(InsertCreativeRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/creatives";
        return await SendAsync(r.AccountKey, "InsertCreative", HttpMethod.Post, url, null, r.Body);
    }
}
