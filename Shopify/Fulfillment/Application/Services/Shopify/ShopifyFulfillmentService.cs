using ApiToolKit.Application.DTOs.Shopify.Fulfillment;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyFulfillmentService : IShopifyFulfillmentService
{
    private readonly IShopifyAuthRepository _authRepository;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyFulfillmentService(IShopifyAuthRepository authRepository, IShopifyApiClient apiClient)
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

    public async Task<Result<ListFulfillmentsResponse>> ListFulfillmentsByOrderAsync(string shopDomain, string orderId)
    {
        var (token, error) = await GetTokenAsync<ListFulfillmentsResponse>(shopDomain);
        if (error != null) return error;

        try
        {
            var response = await _apiClient.GetAsync(shopDomain, $"orders/{orderId}/fulfillments.json");
            var result = JsonConvert.DeserializeObject<ListFulfillmentsResponse>(response);
            await LogAsync(shopDomain, "ListFulfillmentsByOrder", null, response, true, 200);
            return Result<ListFulfillmentsResponse>.Ok(result ?? new ListFulfillmentsResponse());
        }
        catch (Exception ex)
        {
            await LogAsync(shopDomain, "ListFulfillmentsByOrder", null, ex.Message, false, 500);
            return Result<ListFulfillmentsResponse>.Fail(ex.Message, 500);
        }
    }

    public async Task<Result<FulfillmentResponse>> GetFulfillmentAsync(string shopDomain, string orderId, string fulfillmentId)
    {
        var (token, error) = await GetTokenAsync<FulfillmentResponse>(shopDomain);
        if (error != null) return error;

        try
        {
            var response = await _apiClient.GetAsync(shopDomain, $"orders/{orderId}/fulfillments/{fulfillmentId}.json");
            var result = JsonConvert.DeserializeObject<FulfillmentResponse>(response);
            await LogAsync(shopDomain, "GetFulfillment", null, response, true, 200);
            return Result<FulfillmentResponse>.Ok(result ?? new FulfillmentResponse());
        }
        catch (Exception ex)
        {
            await LogAsync(shopDomain, "GetFulfillment", null, ex.Message, false, 500);
            return Result<FulfillmentResponse>.Fail(ex.Message, 500);
        }
    }

    public async Task<Result<int>> CountFulfillmentsAsync(string shopDomain, string orderId)
    {
        var (token, error) = await GetTokenAsync<int>(shopDomain);
        if (error != null) return error;

        try
        {
            var response = await _apiClient.GetAsync(shopDomain, $"orders/{orderId}/fulfillments/count.json");
            var result = JsonConvert.DeserializeObject<dynamic>(response);
            int count = (int)(result?.count ?? 0);
            await LogAsync(shopDomain, "CountFulfillments", null, response, true, 200);
            return Result<int>.Ok(count);
        }
        catch (Exception ex)
        {
            await LogAsync(shopDomain, "CountFulfillments", null, ex.Message, false, 500);
            return Result<int>.Fail(ex.Message, 500);
        }
    }

    public async Task<Result<FulfillmentResponse>> CreateFulfillmentAsync(string shopDomain, CreateFulfillmentRequest request)
    {
        var (token, error) = await GetTokenAsync<FulfillmentResponse>(shopDomain);
        if (error != null) return error;

        var body = new
        {
            fulfillment = new
            {
                line_items_by_fulfillment_order = request.FulfillmentOrderLineItems,
                tracking_info = request.TrackingInfo,
                notify_customer = request.NotifyCustomer
            }
        };
        var requestBody = JsonConvert.SerializeObject(body);

        try
        {
            var response = await _apiClient.PostAsync(shopDomain, "fulfillments.json", body);
            var result = JsonConvert.DeserializeObject<FulfillmentResponse>(response);
            await LogAsync(shopDomain, "CreateFulfillment", requestBody, response, true, 201);
            return Result<FulfillmentResponse>.Ok(result ?? new FulfillmentResponse());
        }
        catch (Exception ex)
        {
            await LogAsync(shopDomain, "CreateFulfillment", requestBody, ex.Message, false, 500);
            return Result<FulfillmentResponse>.Fail(ex.Message, 500);
        }
    }

    public async Task<Result<FulfillmentResponse>> CancelFulfillmentAsync(string shopDomain, string fulfillmentId)
    {
        var (token, error) = await GetTokenAsync<FulfillmentResponse>(shopDomain);
        if (error != null) return error;

        try
        {
            var response = await _apiClient.PostAsync(shopDomain, $"fulfillments/{fulfillmentId}/cancel.json");
            var result = JsonConvert.DeserializeObject<FulfillmentResponse>(response);
            await LogAsync(shopDomain, "CancelFulfillment", null, response, true, 200);
            return Result<FulfillmentResponse>.Ok(result ?? new FulfillmentResponse());
        }
        catch (Exception ex)
        {
            await LogAsync(shopDomain, "CancelFulfillment", null, ex.Message, false, 500);
            return Result<FulfillmentResponse>.Fail(ex.Message, 500);
        }
    }

    public async Task<Result<FulfillmentResponse>> UpdateTrackingAsync(string shopDomain, string fulfillmentId, UpdateTrackingRequest request)
    {
        var (token, error) = await GetTokenAsync<FulfillmentResponse>(shopDomain);
        if (error != null) return error;

        var body = new
        {
            fulfillment = new
            {
                tracking_info = request.TrackingInfo,
                notify_customer = request.NotifyCustomer
            }
        };
        var requestBody = JsonConvert.SerializeObject(body);

        try
        {
            var response = await _apiClient.PostAsync(shopDomain, $"fulfillments/{fulfillmentId}/update_tracking.json", body);
            var result = JsonConvert.DeserializeObject<FulfillmentResponse>(response);
            await LogAsync(shopDomain, "UpdateTracking", requestBody, response, true, 200);
            return Result<FulfillmentResponse>.Ok(result ?? new FulfillmentResponse());
        }
        catch (Exception ex)
        {
            await LogAsync(shopDomain, "UpdateTracking", requestBody, ex.Message, false, 500);
            return Result<FulfillmentResponse>.Fail(ex.Message, 500);
        }
    }
}
