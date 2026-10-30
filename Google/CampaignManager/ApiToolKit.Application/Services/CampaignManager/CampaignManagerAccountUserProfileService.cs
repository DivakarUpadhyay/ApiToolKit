using ApiToolKit.Application.DTOs.CampaignManager.AccountUserProfile;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerAccountUserProfileService : CampaignManagerServiceBase, ICampaignManagerAccountUserProfileService
{
    public CampaignManagerAccountUserProfileService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> InsertAccountUserProfileAsync(InsertAccountUserProfileRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/accountUserProfiles";
        return await SendAsync(r.AccountKey, "InsertAccountUserProfile", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> ListAccountUserProfilesAsync(ListAccountUserProfilesRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/accountUserProfiles";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "maxResults", r.MaxResults?.ToString());
        AddMany(query, "ids", r.Ids);
        Add(query, "userRoleId", r.UserRoleId);
        Add(query, "pageToken", r.PageToken);
        Add(query, "subaccountId", r.SubaccountId);
        Add(query, "sortOrder", r.SortOrder);
        Add(query, "sortField", r.SortField);
        Add(query, "searchString", r.SearchString);
        Add(query, "active", r.Active?.ToString().ToLower());
        return await SendAsync(r.AccountKey, "ListAccountUserProfiles", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> UpdateAccountUserProfileAsync(UpdateAccountUserProfileRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/accountUserProfiles";
        return await SendAsync(r.AccountKey, "UpdateAccountUserProfile", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> PatchAccountUserProfileAsync(PatchAccountUserProfileRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/accountUserProfiles";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "PatchAccountUserProfile", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> GetAccountUserProfileAsync(GetAccountUserProfileRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.ProfileId)}/accountUserProfiles/{Esc(r.AccountUserProfileId)}";
        return await SendAsync(r.AccountKey, "GetAccountUserProfile", HttpMethod.Get, url);
    }
}
