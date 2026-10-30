using ApiToolKit.Application.DTOs.CampaignManager.TargetableRemarketingList;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerTargetableRemarketingListService : CampaignManagerServiceBase, ICampaignManagerTargetableRemarketingListService
{
    public CampaignManagerTargetableRemarketingListService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListTargetableRemarketingListsAsync(ListTargetableRemarketingListsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/targetableRemarketingLists";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "pageToken", r.PageToken);
        Add(query, "sortOrder", r.SortOrder);
        Add(query, "name", r.Name);
        Add(query, "active", r.Active?.ToString().ToLower());
        Add(query, "sortField", r.SortField);
        Add(query, "maxResults", r.MaxResults?.ToString());
        return await SendAsync(r.AccountKey, "ListTargetableRemarketingLists", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetTargetableRemarketingListAsync(GetTargetableRemarketingListRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/targetableRemarketingLists/{Esc(r.TargetableRemarketingListId)}";
        return await SendAsync(r.AccountKey, "GetTargetableRemarketingList", HttpMethod.Get, url);
    }
}
