using ApiToolKit.Application.DTOs.Merchant.Account;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantAccountService : MerchantServiceBase, IMerchantAccountService
{
    public MerchantAccountService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> CreateTestAccountAsync(CreateTestAccountRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}:createTestAccount";
        return await SendAsync(r.AccountKey, "CreateTestAccount", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> CreateAndConfigureAccountAsync(CreateAndConfigureAccountRequest r)
    {
        var url = "https://merchantapi.googleapis.com/accounts/v1/accounts:createAndConfigure";
        return await SendAsync(r.AccountKey, "CreateAndConfigureAccount", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> DeleteAccountAsync(DeleteAccountRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "force", r.Force?.ToString().ToLower());
        return await SendAsync(r.AccountKey, "DeleteAccount", HttpMethod.Delete, url, query);
    }

    public async Task<Result<object>> UpdateAccountAsync(UpdateAccountRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateAccount", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> GetAccountAsync(GetAccountRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}";
        return await SendAsync(r.AccountKey, "GetAccount", HttpMethod.Get, url);
    }

    public async Task<Result<object>> ListAccountsAsync(ListAccountsRequest r)
    {
        var url = "https://merchantapi.googleapis.com/accounts/v1/accounts";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "filter", r.Filter);
        return await SendAsync(r.AccountKey, "ListAccounts", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> ListSubaccountsAsync(ListSubaccountsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}:listSubaccounts";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListSubaccounts", HttpMethod.Get, url, query);
    }
}
