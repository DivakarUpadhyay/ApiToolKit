using ApiToolKit.Application.DTOs.Shopify.Inventory;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyInventoryItemService : IShopifyInventoryItemService
{
    private readonly IShopifyAuthRepository _authRepo;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyInventoryItemService(IShopifyAuthRepository authRepo, IShopifyApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListInventoryItemsResponse>> ListInventoryItemsAsync(string shopDomain, string ids, int? limit = null)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ListInventoryItemsResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListInventoryItemsResponse>.Fail($"No access token found for shop: {shopDomain}");

        var queryParams = new Dictionary<string, string> { ["ids"] = ids };
        if (limit.HasValue) queryParams["limit"] = limit.Value.ToString();

        var response = await _apiClient.GetAsync<ListInventoryItemsResponse>(
            shopDomain,
            "inventory_items.json",
            queryParams,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListInventoryItems",
            RequestBody = JsonConvert.SerializeObject(queryParams),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListInventoryItemsResponse>.Fail("No response from Shopify API.");

        return Result<ListInventoryItemsResponse>.Ok(response);
    }

    public async Task<Result<InventoryItemResponse>> GetInventoryItemAsync(string shopDomain, string inventoryItemId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<InventoryItemResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<InventoryItemResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<InventoryItemResponse>(
            shopDomain,
            $"inventory_items/{inventoryItemId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetInventoryItem",
            RequestBody = JsonConvert.SerializeObject(new { inventoryItemId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.InventoryItem != null,
            StatusCode = response?.InventoryItem != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<InventoryItemResponse>.Fail("No response from Shopify API.");

        return Result<InventoryItemResponse>.Ok(response);
    }

    public async Task<Result<InventoryItemResponse>> UpdateInventoryItemAsync(string shopDomain, string inventoryItemId, UpdateInventoryItemRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<InventoryItemResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<InventoryItemResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { inventory_item = request };

        var response = await _apiClient.PutAsync<InventoryItemResponse>(
            shopDomain,
            $"inventory_items/{inventoryItemId}.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "UpdateInventoryItem",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.InventoryItem != null,
            StatusCode = response?.InventoryItem != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<InventoryItemResponse>.Fail("No response from Shopify API.");

        return Result<InventoryItemResponse>.Ok(response);
    }
}
