using ApiToolKit.Application.DTOs.CampaignManager.DirectorySite;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerDirectorySiteService : CampaignManagerServiceBase, ICampaignManagerDirectorySiteService
{
    public CampaignManagerDirectorySiteService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListDirectorySitesAsync(ListDirectorySitesRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/directorySites";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "sortField", r.SortField);
        Add(query, "searchString", r.SearchString);
        Add(query, "pageToken", r.PageToken);
        Add(query, "sortOrder", r.SortOrder);
        Add(query, "acceptsInStreamVideoPlacements", r.AcceptsInStreamVideoPlacements?.ToString().ToLower());
        Add(query, "active", r.Active?.ToString().ToLower());
        AddMany(query, "ids", r.Ids);
        Add(query, "dfpNetworkCode", r.DfpNetworkCode);
        Add(query, "acceptsPublisherPaidPlacements", r.AcceptsPublisherPaidPlacements?.ToString().ToLower());
        Add(query, "acceptsInterstitialPlacements", r.AcceptsInterstitialPlacements?.ToString().ToLower());
        Add(query, "maxResults", r.MaxResults?.ToString());
        return await SendAsync(r.AccountKey, "ListDirectorySites", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> InsertDirectorySiteAsync(InsertDirectorySiteRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/directorySites";
        return await SendAsync(r.AccountKey, "InsertDirectorySite", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> GetDirectorySiteAsync(GetDirectorySiteRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/directorySites/{Esc(r.DirectorySiteId)}";
        return await SendAsync(r.AccountKey, "GetDirectorySite", HttpMethod.Get, url);
    }
}
