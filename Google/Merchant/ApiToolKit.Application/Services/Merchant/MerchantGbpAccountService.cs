using ApiToolKit.Application.DTOs.Merchant.GbpAccount;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantGbpAccountService : MerchantServiceBase, IMerchantGbpAccountService
{
    public MerchantGbpAccountService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListGbpAccountsAsync(ListGbpAccountsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/gbpAccounts";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListGbpAccounts", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> LinkGbpAccountAsync(LinkGbpAccountRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/gbpAccounts:linkGbpAccount";
        return await SendAsync(r.AccountKey, "LinkGbpAccount", HttpMethod.Post, url, null, r.Body);
    }
}
