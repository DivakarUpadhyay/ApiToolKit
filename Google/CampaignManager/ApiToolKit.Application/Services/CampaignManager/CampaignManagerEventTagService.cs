using ApiToolKit.Application.DTOs.CampaignManager.EventTag;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerEventTagService : CampaignManagerServiceBase, ICampaignManagerEventTagService
{
    public CampaignManagerEventTagService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListEventTagsAsync(ListEventTagsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/eventTags";
        var query = new List<KeyValuePair<string, string?>>();
        AddMany(query, "ids", r.Ids);
        Add(query, "sortOrder", r.SortOrder);
        Add(query, "advertiserId", r.AdvertiserId);
        AddMany(query, "eventTagTypes", r.EventTagTypes);
        Add(query, "enabled", r.Enabled?.ToString().ToLower());
        Add(query, "sortField", r.SortField);
        Add(query, "campaignId", r.CampaignId);
        Add(query, "adId", r.AdId);
        Add(query, "searchString", r.SearchString);
        Add(query, "definitionsOnly", r.DefinitionsOnly?.ToString().ToLower());
        return await SendAsync(r.AccountKey, "ListEventTags", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> UpdateEventTagAsync(UpdateEventTagRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/eventTags";
        return await SendAsync(r.AccountKey, "UpdateEventTag", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> PatchEventTagAsync(PatchEventTagRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/eventTags";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "PatchEventTag", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> DeleteEventTagAsync(DeleteEventTagRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/eventTags/{Esc(r.EventTagId)}";
        return await SendAsync(r.AccountKey, "DeleteEventTag", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> GetEventTagAsync(GetEventTagRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/eventTags/{Esc(r.EventTagId)}";
        return await SendAsync(r.AccountKey, "GetEventTag", HttpMethod.Get, url);
    }

    public async Task<Result<object>> InsertEventTagAsync(InsertEventTagRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/eventTags";
        return await SendAsync(r.AccountKey, "InsertEventTag", HttpMethod.Post, url, null, r.Body);
    }
}
