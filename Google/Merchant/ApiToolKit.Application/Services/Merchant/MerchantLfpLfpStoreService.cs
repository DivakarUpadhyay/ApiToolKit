using ApiToolKit.Application.DTOs.Merchant.LfpLfpStore;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantLfpLfpStoreService : MerchantServiceBase, IMerchantLfpLfpStoreService
{
    public MerchantLfpLfpStoreService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetLfpStoreAsync(GetLfpStoreRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/lfp/v1/accounts/{Esc(r.AccountId)}/lfpStores/{Esc(r.LfpStoreId)}";
        return await SendAsync(r.AccountKey, "GetLfpStore", HttpMethod.Get, url);
    }

    public async Task<Result<object>> DeleteLfpStoreAsync(DeleteLfpStoreRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/lfp/v1/accounts/{Esc(r.AccountId)}/lfpStores/{Esc(r.LfpStoreId)}";
        return await SendAsync(r.AccountKey, "DeleteLfpStore", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> InsertLfpStoreAsync(InsertLfpStoreRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/lfp/v1/accounts/{Esc(r.AccountId)}/lfpStores:insert";
        return await SendAsync(r.AccountKey, "InsertLfpStore", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> ListLfpStoresAsync(ListLfpStoresRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/lfp/v1/accounts/{Esc(r.AccountId)}/lfpStores";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        Add(query, "targetAccount", r.TargetAccount);
        return await SendAsync(r.AccountKey, "ListLfpStores", HttpMethod.Get, url, query);
    }
}
