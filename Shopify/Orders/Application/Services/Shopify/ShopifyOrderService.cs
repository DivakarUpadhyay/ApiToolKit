using ApiToolKit.Application.DTOs.Shopify.Orders;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyOrderService : IShopifyOrderService
{
    private readonly IShopifyAuthRepository _authRepo;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyOrderService(IShopifyAuthRepository authRepo, IShopifyApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListOrdersResponse>> ListOrdersAsync(string shopDomain, string? status = null, int? limit = null)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ListOrdersResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListOrdersResponse>.Fail($"No access token found for shop: {shopDomain}");

        var queryParams = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(status)) queryParams["status"] = status;
        if (limit.HasValue) queryParams["limit"] = limit.Value.ToString();

        var response = await _apiClient.GetAsync<ListOrdersResponse>(
            shopDomain,
            "orders.json",
            queryParams.Count > 0 ? queryParams : null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListOrders",
            RequestBody = JsonConvert.SerializeObject(queryParams),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListOrdersResponse>.Fail("No response from Shopify API.");

        return Result<ListOrdersResponse>.Ok(response);
    }

    public async Task<Result<OrderResponse>> GetOrderAsync(string shopDomain, string orderId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<OrderResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<OrderResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<OrderResponse>(
            shopDomain,
            $"orders/{orderId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetOrder",
            RequestBody = JsonConvert.SerializeObject(new { orderId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Order != null,
            StatusCode = response?.Order != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<OrderResponse>.Fail("No response from Shopify API.");

        return Result<OrderResponse>.Ok(response);
    }

    public async Task<Result<int>> CountOrdersAsync(string shopDomain, string? status = null)
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
            "orders/count.json",
            queryParams.Count > 0 ? queryParams : null,
            token.AccessToken);

        int count = 0;
        if (response != null)
            int.TryParse(response?.count?.ToString(), out count);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CountOrders",
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

    public async Task<Result<OrderResponse>> CreateOrderAsync(string shopDomain, CreateOrderRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<OrderResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<OrderResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { order = request };

        var response = await _apiClient.PostAsync<OrderResponse>(
            shopDomain,
            "orders.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CreateOrder",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Order != null,
            StatusCode = response?.Order != null ? 201 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<OrderResponse>.Fail("No response from Shopify API.");

        return Result<OrderResponse>.Ok(response);
    }

    public async Task<Result<OrderResponse>> UpdateOrderAsync(string shopDomain, string orderId, UpdateOrderRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<OrderResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<OrderResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { order = request };

        var response = await _apiClient.PutAsync<OrderResponse>(
            shopDomain,
            $"orders/{orderId}.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "UpdateOrder",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Order != null,
            StatusCode = response?.Order != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<OrderResponse>.Fail("No response from Shopify API.");

        return Result<OrderResponse>.Ok(response);
    }

    public async Task<Result<OrderResponse>> CancelOrderAsync(string shopDomain, string orderId, string? reason = null)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<OrderResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<OrderResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { reason };

        var response = await _apiClient.PostAsync<OrderResponse>(
            shopDomain,
            $"orders/{orderId}/cancel.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CancelOrder",
            RequestBody = JsonConvert.SerializeObject(new { orderId, reason }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Order != null,
            StatusCode = response?.Order != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<OrderResponse>.Fail("No response from Shopify API.");

        return Result<OrderResponse>.Ok(response);
    }

    public async Task<Result<OrderResponse>> CloseOrderAsync(string shopDomain, string orderId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<OrderResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<OrderResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.PostAsync<OrderResponse>(
            shopDomain,
            $"orders/{orderId}/close.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CloseOrder",
            RequestBody = JsonConvert.SerializeObject(new { orderId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Order != null,
            StatusCode = response?.Order != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<OrderResponse>.Fail("No response from Shopify API.");

        return Result<OrderResponse>.Ok(response);
    }

    public async Task<Result<OrderResponse>> ReopenOrderAsync(string shopDomain, string orderId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<OrderResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<OrderResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.PostAsync<OrderResponse>(
            shopDomain,
            $"orders/{orderId}/open.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ReopenOrder",
            RequestBody = JsonConvert.SerializeObject(new { orderId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Order != null,
            StatusCode = response?.Order != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<OrderResponse>.Fail("No response from Shopify API.");

        return Result<OrderResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteOrderAsync(string shopDomain, string orderId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<bool>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<bool>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.DeleteAsync<dynamic>(
            shopDomain,
            $"orders/{orderId}.json",
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "DeleteOrder",
            RequestBody = JsonConvert.SerializeObject(new { orderId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<bool>.Ok(true);
    }
}
