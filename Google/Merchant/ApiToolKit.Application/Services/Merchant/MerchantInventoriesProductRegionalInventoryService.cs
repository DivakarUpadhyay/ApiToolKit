using ApiToolKit.Application.DTOs.Merchant.InventoriesProductRegionalInventory;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantInventoriesProductRegionalInventoryService : MerchantServiceBase, IMerchantInventoriesProductRegionalInventoryService
{
    public MerchantInventoriesProductRegionalInventoryService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> DeleteRegionalInventoryAsync(DeleteRegionalInventoryRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/inventories/v1/accounts/{Esc(r.AccountId)}/products/{Esc(r.ProductId)}/regionalInventories/{Esc(r.RegionalInventoryId)}";
        return await SendAsync(r.AccountKey, "DeleteRegionalInventory", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> ListRegionalInventoriesAsync(ListRegionalInventoriesRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/inventories/v1/accounts/{Esc(r.AccountId)}/products/{Esc(r.ProductId)}/regionalInventories";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        Add(query, "pageSize", r.PageSize?.ToString());
        return await SendAsync(r.AccountKey, "ListRegionalInventories", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> InsertRegionalInventoryAsync(InsertRegionalInventoryRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/inventories/v1/accounts/{Esc(r.AccountId)}/products/{Esc(r.ProductId)}/regionalInventories:insert";
        return await SendAsync(r.AccountKey, "InsertRegionalInventory", HttpMethod.Post, url, null, r.Body);
    }
}
