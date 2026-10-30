using ApiToolKit.Application.DTOs.CampaignManager.Account;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerAccountService : CampaignManagerServiceBase, ICampaignManagerAccountService
{
    public CampaignManagerAccountService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetAccountAsync(GetAccountRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/accounts/{Esc(r.AccountId)}";
        return await SendAsync(r.AccountKey, "GetAccount", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateAccountAsync(UpdateAccountRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/accounts";
        return await SendAsync(r.AccountKey, "UpdateAccount", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> PatchAccountAsync(PatchAccountRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/accounts";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "PatchAccount", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> ListAccountsAsync(ListAccountsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/accounts";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        Add(query, "maxResults", r.MaxResults?.ToString());
        Add(query, "sortOrder", r.SortOrder);
        Add(query, "active", r.Active?.ToString().ToLower());
        AddMany(query, "ids", r.Ids);
        Add(query, "searchString", r.SearchString);
        Add(query, "sortField", r.SortField);
        return await SendAsync(r.AccountKey, "ListAccounts", HttpMethod.Get, url, query);
    }
}
