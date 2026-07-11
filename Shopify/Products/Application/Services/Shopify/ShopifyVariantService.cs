using ApiToolKit.Application.DTOs.Shopify.Products;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyVariantService : IShopifyVariantService
{
    private readonly IShopifyAuthRepository _authRepo;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyVariantService(IShopifyAuthRepository authRepo, IShopifyApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListVariantsResponse>> ListVariantsAsync(string shopDomain, string productId, int? limit = null)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ListVariantsResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListVariantsResponse>.Fail($"No access token found for shop: {shopDomain}");

        var queryParams = new Dictionary<string, string>();
        if (limit.HasValue) queryParams["limit"] = limit.Value.ToString();

        var response = await _apiClient.GetAsync<ListVariantsResponse>(
            shopDomain,
            $"products/{productId}/variants.json",
            queryParams.Count > 0 ? queryParams : null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListVariants",
            RequestBody = JsonConvert.SerializeObject(new { productId, limit }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListVariantsResponse>.Fail("No response from Shopify API.");

        return Result<ListVariantsResponse>.Ok(response);
    }

    public async Task<Result<VariantResponse>> GetVariantAsync(string shopDomain, string variantId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<VariantResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<VariantResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<VariantResponse>(
            shopDomain,
            $"variants/{variantId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetVariant",
            RequestBody = JsonConvert.SerializeObject(new { variantId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Variant != null,
            StatusCode = response?.Variant != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<VariantResponse>.Fail("No response from Shopify API.");

        return Result<VariantResponse>.Ok(response);
    }

    public async Task<Result<int>> CountVariantsAsync(string shopDomain, string productId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<int>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<int>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<dynamic>(
            shopDomain,
            $"products/{productId}/variants/count.json",
            null,
            token.AccessToken);

        int count = 0;
        if (response != null)
            int.TryParse(response?.count?.ToString(), out count);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CountVariants",
            RequestBody = JsonConvert.SerializeObject(new { productId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<int>.Fail("No response from Shopify API.");

        return Result<int>.Ok(count);
    }

    public async Task<Result<VariantResponse>> CreateVariantAsync(string shopDomain, string productId, CreateVariantRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<VariantResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<VariantResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { variant = request };

        var response = await _apiClient.PostAsync<VariantResponse>(
            shopDomain,
            $"products/{productId}/variants.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CreateVariant",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Variant != null,
            StatusCode = response?.Variant != null ? 201 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<VariantResponse>.Fail("No response from Shopify API.");

        return Result<VariantResponse>.Ok(response);
    }

    public async Task<Result<VariantResponse>> UpdateVariantAsync(string shopDomain, string variantId, UpdateVariantRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<VariantResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<VariantResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { variant = request };

        var response = await _apiClient.PutAsync<VariantResponse>(
            shopDomain,
            $"variants/{variantId}.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "UpdateVariant",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Variant != null,
            StatusCode = response?.Variant != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<VariantResponse>.Fail("No response from Shopify API.");

        return Result<VariantResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteVariantAsync(string shopDomain, string productId, string variantId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<bool>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<bool>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.DeleteAsync<dynamic>(
            shopDomain,
            $"products/{productId}/variants/{variantId}.json",
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "DeleteVariant",
            RequestBody = JsonConvert.SerializeObject(new { productId, variantId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<bool>.Ok(true);
    }
}
