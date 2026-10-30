using ApiToolKit.Application.DTOs.CampaignManager.FloodlightActivity;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerFloodlightActivityService : CampaignManagerServiceBase, ICampaignManagerFloodlightActivityService
{
    public CampaignManagerFloodlightActivityService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> UpdateFloodlightActivityAsync(UpdateFloodlightActivityRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/floodlightActivities";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "UpdateFloodlightActivity", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> UpdateFloodlightActivityAltAsync(UpdateFloodlightActivityAltRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/floodlightActivities";
        return await SendAsync(r.AccountKey, "UpdateFloodlightActivityAlt", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> DeleteFloodlightActivityAsync(DeleteFloodlightActivityRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/floodlightActivities/{Esc(r.FloodlightActivityId)}";
        return await SendAsync(r.AccountKey, "DeleteFloodlightActivity", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> ListFloodlightActivitiesAsync(ListFloodlightActivitiesRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/floodlightActivities";
        var query = new List<KeyValuePair<string, string?>>();
        AddMany(query, "ids", r.Ids);
        Add(query, "sortOrder", r.SortOrder);
        Add(query, "tagString", r.TagString);
        Add(query, "sortField", r.SortField);
        Add(query, "maxResults", r.MaxResults?.ToString());
        Add(query, "searchString", r.SearchString);
        AddMany(query, "floodlightActivityGroupIds", r.FloodlightActivityGroupIds);
        Add(query, "floodlightActivityGroupTagString", r.FloodlightActivityGroupTagString);
        Add(query, "floodlightActivityGroupName", r.FloodlightActivityGroupName);
        Add(query, "pageToken", r.PageToken);
        Add(query, "floodlightConfigurationId", r.FloodlightConfigurationId);
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "floodlightActivityGroupType", r.FloodlightActivityGroupType);
        return await SendAsync(r.AccountKey, "ListFloodlightActivities", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetFloodlightActivityAsync(GetFloodlightActivityRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/floodlightActivities/{Esc(r.FloodlightActivityId)}";
        return await SendAsync(r.AccountKey, "GetFloodlightActivity", HttpMethod.Get, url);
    }

    public async Task<Result<object>> GeneratetagFloodlightActivityAsync(GeneratetagFloodlightActivityRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/floodlightActivities/generatetag";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "floodlightActivityId", r.FloodlightActivityId);
        return await SendAsync(r.AccountKey, "GeneratetagFloodlightActivity", HttpMethod.Post, url, query);
    }

    public async Task<Result<object>> InsertFloodlightActivityAsync(InsertFloodlightActivityRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/floodlightActivities";
        return await SendAsync(r.AccountKey, "InsertFloodlightActivity", HttpMethod.Post, url, null, r.Body);
    }
}
