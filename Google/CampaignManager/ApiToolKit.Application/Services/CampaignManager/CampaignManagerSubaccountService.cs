using ApiToolKit.Application.DTOs.CampaignManager.Subaccount;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerSubaccountService : CampaignManagerServiceBase, ICampaignManagerSubaccountService
{
    public CampaignManagerSubaccountService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> UpdateSubaccountAsync(UpdateSubaccountRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/subaccounts";
        return await SendAsync(r.AccountKey, "UpdateSubaccount", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> PatchSubaccountAsync(PatchSubaccountRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/subaccounts";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "PatchSubaccount", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> InsertSubaccountAsync(InsertSubaccountRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/subaccounts";
        return await SendAsync(r.AccountKey, "InsertSubaccount", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> GetSubaccountAsync(GetSubaccountRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/subaccounts/{Esc(r.SubaccountId)}";
        return await SendAsync(r.AccountKey, "GetSubaccount", HttpMethod.Get, url);
    }

    public async Task<Result<object>> ListSubaccountsAsync(ListSubaccountsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/subaccounts";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "searchString", r.SearchString);
        Add(query, "sortOrder", r.SortOrder);
        AddMany(query, "ids", r.Ids);
        Add(query, "pageToken", r.PageToken);
        Add(query, "maxResults", r.MaxResults?.ToString());
        Add(query, "sortField", r.SortField);
        return await SendAsync(r.AccountKey, "ListSubaccounts", HttpMethod.Get, url, query);
    }
}
