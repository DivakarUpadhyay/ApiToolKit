using ApiToolKit.Application.DTOs.CampaignManager.AdvertiserGroup;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerAdvertiserGroupService : CampaignManagerServiceBase, ICampaignManagerAdvertiserGroupService
{
    public CampaignManagerAdvertiserGroupService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> DeleteAdvertiserGroupAsync(DeleteAdvertiserGroupRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/advertiserGroups/{Esc(r.AdvertiserGroupId)}";
        return await SendAsync(r.AccountKey, "DeleteAdvertiserGroup", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> ListAdvertiserGroupsAsync(ListAdvertiserGroupsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/advertiserGroups";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "sortField", r.SortField);
        Add(query, "searchString", r.SearchString);
        Add(query, "sortOrder", r.SortOrder);
        Add(query, "maxResults", r.MaxResults?.ToString());
        AddMany(query, "ids", r.Ids);
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListAdvertiserGroups", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> UpdateAdvertiserGroupAsync(UpdateAdvertiserGroupRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/advertiserGroups";
        return await SendAsync(r.AccountKey, "UpdateAdvertiserGroup", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> InsertAdvertiserGroupAsync(InsertAdvertiserGroupRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/advertiserGroups";
        return await SendAsync(r.AccountKey, "InsertAdvertiserGroup", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> GetAdvertiserGroupAsync(GetAdvertiserGroupRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/advertiserGroups/{Esc(r.AdvertiserGroupId)}";
        return await SendAsync(r.AccountKey, "GetAdvertiserGroup", HttpMethod.Get, url);
    }

    public async Task<Result<object>> PatchAdvertiserGroupAsync(PatchAdvertiserGroupRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/advertiserGroups";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "PatchAdvertiserGroup", HttpMethod.Patch, url, query, r.Body);
    }
}
