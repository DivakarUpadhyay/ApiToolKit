using ApiToolKit.Application.DTOs.Shopify.Content;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyMetafieldService : IShopifyMetafieldService
{
    private readonly IShopifyAuthRepository _authRepo;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyMetafieldService(IShopifyAuthRepository authRepo, IShopifyApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    private static string BuildListEndpoint(string? resource, string? resourceId)
    {
        if (!string.IsNullOrEmpty(resource) && !string.IsNullOrEmpty(resourceId))
            return $"{resource}/{resourceId}/metafields.json";
        return "metafields.json";
    }

    private static string BuildCountEndpoint(string? resource, string? resourceId)
    {
        if (!string.IsNullOrEmpty(resource) && !string.IsNullOrEmpty(resourceId))
            return $"{resource}/{resourceId}/metafields/count.json";
        return "metafields/count.json";
    }

    public async Task<Result<ListMetafieldsResponse>> ListMetafieldsAsync(string shopDomain, string? resource = null, string? resourceId = null, int? limit = null)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ListMetafieldsResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListMetafieldsResponse>.Fail($"No access token found for shop: {shopDomain}");

        var queryParams = new Dictionary<string, string>();
        if (limit.HasValue) queryParams["limit"] = limit.Value.ToString();

        var endpoint = BuildListEndpoint(resource, resourceId);

        var response = await _apiClient.GetAsync<ListMetafieldsResponse>(
            shopDomain,
            endpoint,
            queryParams.Count > 0 ? queryParams : null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListMetafields",
            RequestBody = JsonConvert.SerializeObject(new { resource, resourceId, limit }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListMetafieldsResponse>.Fail("No response from Shopify API.");

        return Result<ListMetafieldsResponse>.Ok(response);
    }

    public async Task<Result<MetafieldResponse>> GetMetafieldAsync(string shopDomain, string metafieldId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<MetafieldResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<MetafieldResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<MetafieldResponse>(
            shopDomain,
            $"metafields/{metafieldId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetMetafield",
            RequestBody = JsonConvert.SerializeObject(new { metafieldId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Metafield != null,
            StatusCode = response?.Metafield != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<MetafieldResponse>.Fail("No response from Shopify API.");

        return Result<MetafieldResponse>.Ok(response);
    }

    public async Task<Result<int>> CountMetafieldsAsync(string shopDomain, string? resource = null, string? resourceId = null)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<int>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<int>.Fail($"No access token found for shop: {shopDomain}");

        var endpoint = BuildCountEndpoint(resource, resourceId);

        var response = await _apiClient.GetAsync<dynamic>(
            shopDomain,
            endpoint,
            null,
            token.AccessToken);

        int count = 0;
        if (response != null)
            int.TryParse(response?.count?.ToString(), out count);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CountMetafields",
            RequestBody = JsonConvert.SerializeObject(new { resource, resourceId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<int>.Fail("No response from Shopify API.");

        return Result<int>.Ok(count);
    }

    public async Task<Result<MetafieldResponse>> CreateMetafieldAsync(string shopDomain, CreateMetafieldRequest request, string? resource = null, string? resourceId = null)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<MetafieldResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<MetafieldResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { metafield = request };
        var endpoint = BuildListEndpoint(resource, resourceId);

        var response = await _apiClient.PostAsync<MetafieldResponse>(
            shopDomain,
            endpoint,
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CreateMetafield",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Metafield != null,
            StatusCode = response?.Metafield != null ? 201 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<MetafieldResponse>.Fail("No response from Shopify API.");

        return Result<MetafieldResponse>.Ok(response);
    }

    public async Task<Result<MetafieldResponse>> UpdateMetafieldAsync(string shopDomain, string metafieldId, UpdateMetafieldRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<MetafieldResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<MetafieldResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { metafield = request };

        var response = await _apiClient.PutAsync<MetafieldResponse>(
            shopDomain,
            $"metafields/{metafieldId}.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "UpdateMetafield",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Metafield != null,
            StatusCode = response?.Metafield != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<MetafieldResponse>.Fail("No response from Shopify API.");

        return Result<MetafieldResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteMetafieldAsync(string shopDomain, string metafieldId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<bool>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<bool>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.DeleteAsync<dynamic>(
            shopDomain,
            $"metafields/{metafieldId}.json",
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "DeleteMetafield",
            RequestBody = JsonConvert.SerializeObject(new { metafieldId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<bool>.Ok(true);
    }
}
