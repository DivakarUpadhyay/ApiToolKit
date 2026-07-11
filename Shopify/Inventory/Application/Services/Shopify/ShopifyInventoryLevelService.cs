using ApiToolKit.Application.DTOs.Shopify.Inventory;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyInventoryLevelService : IShopifyInventoryLevelService
{
    private readonly IShopifyAuthRepository _authRepo;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyInventoryLevelService(IShopifyAuthRepository authRepo, IShopifyApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListInventoryLevelsResponse>> ListInventoryLevelsAsync(string shopDomain, string? inventoryItemIds = null, string? locationIds = null)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ListInventoryLevelsResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListInventoryLevelsResponse>.Fail($"No access token found for shop: {shopDomain}");

        var queryParams = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(inventoryItemIds)) queryParams["inventory_item_ids"] = inventoryItemIds;
        if (!string.IsNullOrEmpty(locationIds)) queryParams["location_ids"] = locationIds;

        var response = await _apiClient.GetAsync<ListInventoryLevelsResponse>(
            shopDomain,
            "inventory_levels.json",
            queryParams.Count > 0 ? queryParams : null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListInventoryLevels",
            RequestBody = JsonConvert.SerializeObject(queryParams),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListInventoryLevelsResponse>.Fail("No response from Shopify API.");

        return Result<ListInventoryLevelsResponse>.Ok(response);
    }

    public async Task<Result<InventoryLevelResponse>> AdjustInventoryLevelAsync(string shopDomain, AdjustInventoryLevelRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<InventoryLevelResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<InventoryLevelResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new
        {
            inventory_item_id = request.InventoryItemId,
            location_id = request.LocationId,
            available_adjustment = request.AvailableAdjustment
        };

        var response = await _apiClient.PostAsync<InventoryLevelResponse>(
            shopDomain,
            "inventory_levels/adjust.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "AdjustInventoryLevel",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.InventoryLevel != null,
            StatusCode = response?.InventoryLevel != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<InventoryLevelResponse>.Fail("No response from Shopify API.");

        return Result<InventoryLevelResponse>.Ok(response);
    }

    public async Task<Result<InventoryLevelResponse>> ConnectInventoryLevelAsync(string shopDomain, ConnectInventoryLevelRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<InventoryLevelResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<InventoryLevelResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new
        {
            inventory_item_id = request.InventoryItemId,
            location_id = request.LocationId,
            relocate_if_necessary = request.RelocateIfNecessary
        };

        var response = await _apiClient.PostAsync<InventoryLevelResponse>(
            shopDomain,
            "inventory_levels/connect.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ConnectInventoryLevel",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.InventoryLevel != null,
            StatusCode = response?.InventoryLevel != null ? 201 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<InventoryLevelResponse>.Fail("No response from Shopify API.");

        return Result<InventoryLevelResponse>.Ok(response);
    }

    public async Task<Result<InventoryLevelResponse>> SetInventoryLevelAsync(string shopDomain, SetInventoryLevelRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<InventoryLevelResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<InventoryLevelResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new
        {
            inventory_item_id = request.InventoryItemId,
            location_id = request.LocationId,
            available = request.Available
        };

        var response = await _apiClient.PostAsync<InventoryLevelResponse>(
            shopDomain,
            "inventory_levels/set.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "SetInventoryLevel",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.InventoryLevel != null,
            StatusCode = response?.InventoryLevel != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<InventoryLevelResponse>.Fail("No response from Shopify API.");

        return Result<InventoryLevelResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteInventoryLevelAsync(string shopDomain, string inventoryItemId, string locationId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<bool>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<bool>.Fail($"No access token found for shop: {shopDomain}");

        var endpoint = $"inventory_levels.json?inventory_item_id={Uri.EscapeDataString(inventoryItemId)}&location_id={Uri.EscapeDataString(locationId)}";

        var response = await _apiClient.DeleteAsync<dynamic>(
            shopDomain,
            endpoint,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "DeleteInventoryLevel",
            RequestBody = JsonConvert.SerializeObject(new { inventoryItemId, locationId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<bool>.Ok(true);
    }
}
