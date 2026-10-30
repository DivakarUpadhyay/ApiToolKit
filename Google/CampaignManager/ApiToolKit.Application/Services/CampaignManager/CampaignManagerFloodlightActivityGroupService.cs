using ApiToolKit.Application.DTOs.CampaignManager.FloodlightActivityGroup;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerFloodlightActivityGroupService : CampaignManagerServiceBase, ICampaignManagerFloodlightActivityGroupService
{
    public CampaignManagerFloodlightActivityGroupService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListFloodlightActivityGroupsAsync(ListFloodlightActivityGroupsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/floodlightActivityGroups";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "maxResults", r.MaxResults?.ToString());
        Add(query, "sortField", r.SortField);
        Add(query, "sortOrder", r.SortOrder);
        Add(query, "type", r.Type);
        Add(query, "searchString", r.SearchString);
        Add(query, "pageToken", r.PageToken);
        AddMany(query, "ids", r.Ids);
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "floodlightConfigurationId", r.FloodlightConfigurationId);
        return await SendAsync(r.AccountKey, "ListFloodlightActivityGroups", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> UpdateFloodlightActivityGroupAsync(UpdateFloodlightActivityGroupRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/floodlightActivityGroups";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "UpdateFloodlightActivityGroup", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> InsertFloodlightActivityGroupAsync(InsertFloodlightActivityGroupRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/floodlightActivityGroups";
        return await SendAsync(r.AccountKey, "InsertFloodlightActivityGroup", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> UpdateFloodlightActivityGroupAltAsync(UpdateFloodlightActivityGroupAltRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/floodlightActivityGroups";
        return await SendAsync(r.AccountKey, "UpdateFloodlightActivityGroupAlt", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> GetFloodlightActivityGroupAsync(GetFloodlightActivityGroupRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/floodlightActivityGroups/{Esc(r.FloodlightActivityGroupId)}";
        return await SendAsync(r.AccountKey, "GetFloodlightActivityGroup", HttpMethod.Get, url);
    }
}
