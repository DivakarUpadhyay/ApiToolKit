using ApiToolKit.Application.DTOs.CampaignManager.RemarketingList;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerRemarketingListService : CampaignManagerServiceBase, ICampaignManagerRemarketingListService
{
    public CampaignManagerRemarketingListService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListRemarketingListsAsync(ListRemarketingListsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/remarketingLists";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "sortField", r.SortField);
        Add(query, "pageToken", r.PageToken);
        Add(query, "active", r.Active?.ToString().ToLower());
        Add(query, "sortOrder", r.SortOrder);
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "floodlightActivityId", r.FloodlightActivityId);
        Add(query, "maxResults", r.MaxResults?.ToString());
        Add(query, "name", r.Name);
        return await SendAsync(r.AccountKey, "ListRemarketingLists", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetRemarketingListAsync(GetRemarketingListRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/remarketingLists/{Esc(r.RemarketingListId)}";
        return await SendAsync(r.AccountKey, "GetRemarketingList", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateRemarketingListAsync(UpdateRemarketingListRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/remarketingLists";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "UpdateRemarketingList", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> UpdateRemarketingListAltAsync(UpdateRemarketingListAltRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/remarketingLists";
        return await SendAsync(r.AccountKey, "UpdateRemarketingListAlt", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> InsertRemarketingListAsync(InsertRemarketingListRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/remarketingLists";
        return await SendAsync(r.AccountKey, "InsertRemarketingList", HttpMethod.Post, url, null, r.Body);
    }
}
