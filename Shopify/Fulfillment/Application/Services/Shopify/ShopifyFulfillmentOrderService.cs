using ApiToolKit.Application.DTOs.Shopify.Fulfillment;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyFulfillmentOrderService : IShopifyFulfillmentOrderService
{
    private readonly IShopifyAuthRepository _authRepository;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyFulfillmentOrderService(IShopifyAuthRepository authRepository, IShopifyApiClient apiClient)
    {
        _authRepository = authRepository;
        _apiClient = apiClient;
    }

    private async Task<(ShopifyToken? token, Result<T>? error)> GetTokenAsync<T>(string shopDomain)
    {
        var token = await _authRepository.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return (null, Result<T>.Fail($"No access token found for shop: {shopDomain}", 401));
        return (token, null);
    }

    private async Task LogAsync(string shopDomain, string apiName, string? requestBody, string responseBody, bool isSuccess, int statusCode)
    {
        await _authRepository.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = apiName,
            RequestBody = requestBody,
            ResponseBody = responseBody,
            IsSuccess = isSuccess,
            StatusCode = statusCode,
            CreatedAt = DateTime.UtcNow
        });
    }

    public async Task<Result<ListFulfillmentOrdersResponse>> ListFulfillmentOrdersAsync(string shopDomain, string orderId)
    {
        var (token, error) = await GetTokenAsync<ListFulfillmentOrdersResponse>(shopDomain);
        if (error != null) return error;

        try
        {
            var response = await _apiClient.GetAsync(shopDomain, $"orders/{orderId}/fulfillment_orders.json");
            var result = JsonConvert.DeserializeObject<ListFulfillmentOrdersResponse>(response);
            await LogAsync(shopDomain, "ListFulfillmentOrders", null, response, true, 200);
            return Result<ListFulfillmentOrdersResponse>.Ok(result ?? new ListFulfillmentOrdersResponse());
        }
        catch (Exception ex)
        {
            await LogAsync(shopDomain, "ListFulfillmentOrders", null, ex.Message, false, 500);
            return Result<ListFulfillmentOrdersResponse>.Fail(ex.Message, 500);
        }
    }

    public async Task<Result<FulfillmentOrderResponse>> GetFulfillmentOrderAsync(string shopDomain, string fulfillmentOrderId)
    {
        var (token, error) = await GetTokenAsync<FulfillmentOrderResponse>(shopDomain);
        if (error != null) return error;

        try
        {
            var response = await _apiClient.GetAsync(shopDomain, $"fulfillment_orders/{fulfillmentOrderId}.json");
            var result = JsonConvert.DeserializeObject<FulfillmentOrderResponse>(response);
            await LogAsync(shopDomain, "GetFulfillmentOrder", null, response, true, 200);
            return Result<FulfillmentOrderResponse>.Ok(result ?? new FulfillmentOrderResponse());
        }
        catch (Exception ex)
        {
            await LogAsync(shopDomain, "GetFulfillmentOrder", null, ex.Message, false, 500);
            return Result<FulfillmentOrderResponse>.Fail(ex.Message, 500);
        }
    }

    public async Task<Result<FulfillmentOrderResponse>> CancelFulfillmentOrderAsync(string shopDomain, string fulfillmentOrderId)
    {
        var (token, error) = await GetTokenAsync<FulfillmentOrderResponse>(shopDomain);
        if (error != null) return error;

        try
        {
            var response = await _apiClient.PostAsync(shopDomain, $"fulfillment_orders/{fulfillmentOrderId}/cancel.json");
            var result = JsonConvert.DeserializeObject<FulfillmentOrderResponse>(response);
            await LogAsync(shopDomain, "CancelFulfillmentOrder", null, response, true, 200);
            return Result<FulfillmentOrderResponse>.Ok(result ?? new FulfillmentOrderResponse());
        }
        catch (Exception ex)
        {
            await LogAsync(shopDomain, "CancelFulfillmentOrder", null, ex.Message, false, 500);
            return Result<FulfillmentOrderResponse>.Fail(ex.Message, 500);
        }
    }

    public async Task<Result<FulfillmentOrderResponse>> MoveFulfillmentOrderAsync(string shopDomain, string fulfillmentOrderId, string newLocationId)
    {
        var (token, error) = await GetTokenAsync<FulfillmentOrderResponse>(shopDomain);
        if (error != null) return error;

        var body = new
        {
            fulfillment_order = new
            {
                new_location_id = newLocationId
            }
        };
        var requestBody = JsonConvert.SerializeObject(body);

        try
        {
            var response = await _apiClient.PostAsync(shopDomain, $"fulfillment_orders/{fulfillmentOrderId}/move.json", body);
            var result = JsonConvert.DeserializeObject<FulfillmentOrderResponse>(response);
            await LogAsync(shopDomain, "MoveFulfillmentOrder", requestBody, response, true, 200);
            return Result<FulfillmentOrderResponse>.Ok(result ?? new FulfillmentOrderResponse());
        }
        catch (Exception ex)
        {
            await LogAsync(shopDomain, "MoveFulfillmentOrder", requestBody, ex.Message, false, 500);
            return Result<FulfillmentOrderResponse>.Fail(ex.Message, 500);
        }
    }

    public async Task<Result<FulfillmentOrderResponse>> ReleaseHoldsAsync(string shopDomain, string fulfillmentOrderId)
    {
        var (token, error) = await GetTokenAsync<FulfillmentOrderResponse>(shopDomain);
        if (error != null) return error;

        try
        {
            var response = await _apiClient.PostAsync(shopDomain, $"fulfillment_orders/{fulfillmentOrderId}/release_holds.json");
            var result = JsonConvert.DeserializeObject<FulfillmentOrderResponse>(response);
            await LogAsync(shopDomain, "ReleaseHolds", null, response, true, 200);
            return Result<FulfillmentOrderResponse>.Ok(result ?? new FulfillmentOrderResponse());
        }
        catch (Exception ex)
        {
            await LogAsync(shopDomain, "ReleaseHolds", null, ex.Message, false, 500);
            return Result<FulfillmentOrderResponse>.Fail(ex.Message, 500);
        }
    }
}
