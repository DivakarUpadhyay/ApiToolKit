using ApiToolKit.Application.DTOs.Merchant.ProductsProductInput;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantProductsProductInputService : MerchantServiceBase, IMerchantProductsProductInputService
{
    public MerchantProductsProductInputService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> InsertProductInputAsync(InsertProductInputRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/products/v1/accounts/{Esc(r.AccountId)}/productInputs:insert";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "dataSource", r.DataSource);
        return await SendAsync(r.AccountKey, "InsertProductInput", HttpMethod.Post, url, query, r.Body);
    }

    public async Task<Result<object>> DeleteProductInputAsync(DeleteProductInputRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/products/v1/accounts/{Esc(r.AccountId)}/productInputs/{Esc(r.ProductInputId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "dataSource", r.DataSource);
        return await SendAsync(r.AccountKey, "DeleteProductInput", HttpMethod.Delete, url, query);
    }

    public async Task<Result<object>> UpdateProductInputAsync(UpdateProductInputRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/products/v1/accounts/{Esc(r.AccountId)}/productInputs/{Esc(r.ProductInputId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "dataSource", r.DataSource);
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateProductInput", HttpMethod.Patch, url, query, r.Body);
    }
}
