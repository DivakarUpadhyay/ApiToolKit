using ApiToolKit.Application.DTOs.CampaignManager.BillingProfile;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerBillingProfileService : CampaignManagerServiceBase, ICampaignManagerBillingProfileService
{
    public CampaignManagerBillingProfileService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListBillingProfilesAsync(ListBillingProfilesRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/billingProfiles";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "sortOrder", r.SortOrder);
        AddMany(query, "ids", r.Ids);
        Add(query, "onlySuggestion", r.OnlySuggestion?.ToString().ToLower());
        AddMany(query, "subaccountIds", r.SubaccountIds);
        Add(query, "pageToken", r.PageToken);
        Add(query, "currency_code", r.CurrencyCode);
        AddMany(query, "status", r.Status);
        Add(query, "sortField", r.SortField);
        Add(query, "name", r.Name);
        Add(query, "maxResults", r.MaxResults?.ToString());
        return await SendAsync(r.AccountKey, "ListBillingProfiles", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetBillingProfileAsync(GetBillingProfileRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/billingProfiles/{Esc(r.BillingProfileId)}";
        return await SendAsync(r.AccountKey, "GetBillingProfile", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateBillingProfileAsync(UpdateBillingProfileRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/billingProfiles";
        return await SendAsync(r.AccountKey, "UpdateBillingProfile", HttpMethod.Put, url, null, r.Body);
    }
}
