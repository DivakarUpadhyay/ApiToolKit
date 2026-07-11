using ApiToolKit.Application.DTOs.Shopify.Products;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyProductService : IShopifyProductService
{
    private readonly IShopifyAuthRepository _authRepo;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyProductService(IShopifyAuthRepository authRepo, IShopifyApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListProductsResponse>> ListProductsAsync(string shopDomain, int? limit = null, string? status = null)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ListProductsResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListProductsResponse>.Fail($"No access token found for shop: {shopDomain}");

        var queryParams = new Dictionary<string, string>();
        if (limit.HasValue) queryParams["limit"] = limit.Value.ToString();
        if (!string.IsNullOrEmpty(status)) queryParams["status"] = status;

        var response = await _apiClient.GetAsync<ListProductsResponse>(
            shopDomain,
            "products.json",
            queryParams.Count > 0 ? queryParams : null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListProducts",
            RequestBody = JsonConvert.SerializeObject(queryParams),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListProductsResponse>.Fail("No response from Shopify API.");

        return Result<ListProductsResponse>.Ok(response);
    }

    public async Task<Result<ProductResponse>> GetProductAsync(string shopDomain, string productId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ProductResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ProductResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<ProductResponse>(
            shopDomain,
            $"products/{productId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetProduct",
            RequestBody = JsonConvert.SerializeObject(new { productId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Product != null,
            StatusCode = response?.Product != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ProductResponse>.Fail("No response from Shopify API.");

        return Result<ProductResponse>.Ok(response);
    }

    public async Task<Result<int>> CountProductsAsync(string shopDomain, string? status = null)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<int>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<int>.Fail($"No access token found for shop: {shopDomain}");

        var queryParams = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(status)) queryParams["status"] = status;

        var response = await _apiClient.GetAsync<dynamic>(
            shopDomain,
            "products/count.json",
            queryParams.Count > 0 ? queryParams : null,
            token.AccessToken);

        int count = 0;
        if (response != null)
        {
            int.TryParse(response?.count?.ToString(), out count);
        }

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CountProducts",
            RequestBody = JsonConvert.SerializeObject(queryParams),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<int>.Fail("No response from Shopify API.");

        return Result<int>.Ok(count);
    }

    public async Task<Result<ProductResponse>> CreateProductAsync(string shopDomain, CreateProductRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ProductResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ProductResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { product = request };

        var response = await _apiClient.PostAsync<ProductResponse>(
            shopDomain,
            "products.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CreateProduct",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Product != null,
            StatusCode = response?.Product != null ? 201 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ProductResponse>.Fail("No response from Shopify API.");

        return Result<ProductResponse>.Ok(response);
    }

    public async Task<Result<ProductResponse>> UpdateProductAsync(string shopDomain, string productId, UpdateProductRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ProductResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ProductResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { product = request };

        var response = await _apiClient.PutAsync<ProductResponse>(
            shopDomain,
            $"products/{productId}.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "UpdateProduct",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Product != null,
            StatusCode = response?.Product != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ProductResponse>.Fail("No response from Shopify API.");

        return Result<ProductResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteProductAsync(string shopDomain, string productId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<bool>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<bool>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.DeleteAsync<dynamic>(
            shopDomain,
            $"products/{productId}.json",
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "DeleteProduct",
            RequestBody = JsonConvert.SerializeObject(new { productId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<bool>.Ok(true);
    }
}
