using ApiToolKit.Application.DTOs.Merchant.ProductsProduct;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantProductsProductService : MerchantServiceBase, IMerchantProductsProductService
{
    public MerchantProductsProductService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListProductsAsync(ListProductsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/products/v1/accounts/{Esc(r.AccountId)}/products";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        Add(query, "pageSize", r.PageSize?.ToString());
        return await SendAsync(r.AccountKey, "ListProducts", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetProductAsync(GetProductRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/products/v1/accounts/{Esc(r.AccountId)}/products/{Esc(r.ProductId)}";
        return await SendAsync(r.AccountKey, "GetProduct", HttpMethod.Get, url);
    }
}
