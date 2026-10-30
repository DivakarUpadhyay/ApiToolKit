using ApiToolKit.Application.DTOs.Merchant.InventoriesProductLocalInventory;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantInventoriesProductLocalInventoryService : MerchantServiceBase, IMerchantInventoriesProductLocalInventoryService
{
    public MerchantInventoriesProductLocalInventoryService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListLocalInventoriesAsync(ListLocalInventoriesRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/inventories/v1/accounts/{Esc(r.AccountId)}/products/{Esc(r.ProductId)}/localInventories";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        Add(query, "pageSize", r.PageSize?.ToString());
        return await SendAsync(r.AccountKey, "ListLocalInventories", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> InsertLocalInventoryAsync(InsertLocalInventoryRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/inventories/v1/accounts/{Esc(r.AccountId)}/products/{Esc(r.ProductId)}/localInventories:insert";
        return await SendAsync(r.AccountKey, "InsertLocalInventory", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> DeleteLocalInventoryAsync(DeleteLocalInventoryRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/inventories/v1/accounts/{Esc(r.AccountId)}/products/{Esc(r.ProductId)}/localInventories/{Esc(r.LocalInventoryId)}";
        return await SendAsync(r.AccountKey, "DeleteLocalInventory", HttpMethod.Delete, url);
    }
}
