using ApiToolKit.Application.DTOs.Shopify.Inventory;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyLocationService : IShopifyLocationService
{
    private readonly IShopifyAuthRepository _authRepo;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyLocationService(IShopifyAuthRepository authRepo, IShopifyApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListLocationsResponse>> ListLocationsAsync(string shopDomain)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ListLocationsResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListLocationsResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<ListLocationsResponse>(
            shopDomain,
            "locations.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListLocations",
            RequestBody = null,
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListLocationsResponse>.Fail("No response from Shopify API.");

        return Result<ListLocationsResponse>.Ok(response);
    }

    public async Task<Result<LocationResponse>> GetLocationAsync(string shopDomain, string locationId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<LocationResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<LocationResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<LocationResponse>(
            shopDomain,
            $"locations/{locationId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetLocation",
            RequestBody = JsonConvert.SerializeObject(new { locationId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Location != null,
            StatusCode = response?.Location != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<LocationResponse>.Fail("No response from Shopify API.");

        return Result<LocationResponse>.Ok(response);
    }

    public async Task<Result<int>> CountLocationsAsync(string shopDomain)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<int>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<int>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<dynamic>(
            shopDomain,
            "locations/count.json",
            null,
            token.AccessToken);

        int count = 0;
        if (response != null)
        {
            int.TryParse(response?.count?.ToString(), out count);
        }

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CountLocations",
            RequestBody = null,
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<int>.Fail("No response from Shopify API.");

        return Result<int>.Ok(count);
    }

    public async Task<Result<ListInventoryLevelsResponse>> GetLocationInventoryLevelsAsync(string shopDomain, string locationId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ListInventoryLevelsResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListInventoryLevelsResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<ListInventoryLevelsResponse>(
            shopDomain,
            $"locations/{locationId}/inventory_levels.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetLocationInventoryLevels",
            RequestBody = JsonConvert.SerializeObject(new { locationId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListInventoryLevelsResponse>.Fail("No response from Shopify API.");

        return Result<ListInventoryLevelsResponse>.Ok(response);
    }
}
