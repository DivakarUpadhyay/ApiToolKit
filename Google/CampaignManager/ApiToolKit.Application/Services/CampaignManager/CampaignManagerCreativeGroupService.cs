using ApiToolKit.Application.DTOs.CampaignManager.CreativeGroup;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerCreativeGroupService : CampaignManagerServiceBase, ICampaignManagerCreativeGroupService
{
    public CampaignManagerCreativeGroupService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetCreativeGroupAsync(GetCreativeGroupRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/creativeGroups/{Esc(r.CreativeGroupId)}";
        return await SendAsync(r.AccountKey, "GetCreativeGroup", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateCreativeGroupAsync(UpdateCreativeGroupRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/creativeGroups";
        return await SendAsync(r.AccountKey, "UpdateCreativeGroup", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> PatchCreativeGroupAsync(PatchCreativeGroupRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/creativeGroups";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "PatchCreativeGroup", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> ListCreativeGroupsAsync(ListCreativeGroupsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/creativeGroups";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "searchString", r.SearchString);
        Add(query, "sortField", r.SortField);
        Add(query, "sortOrder", r.SortOrder);
        Add(query, "groupNumber", r.GroupNumber?.ToString());
        Add(query, "pageToken", r.PageToken);
        AddMany(query, "ids", r.Ids);
        Add(query, "maxResults", r.MaxResults?.ToString());
        AddMany(query, "advertiserIds", r.AdvertiserIds);
        return await SendAsync(r.AccountKey, "ListCreativeGroups", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> InsertCreativeGroupAsync(InsertCreativeGroupRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/creativeGroups";
        return await SendAsync(r.AccountKey, "InsertCreativeGroup", HttpMethod.Post, url, null, r.Body);
    }
}
