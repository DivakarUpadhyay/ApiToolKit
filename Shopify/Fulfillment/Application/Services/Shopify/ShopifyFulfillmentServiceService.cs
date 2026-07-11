using ApiToolKit.Application.DTOs.Shopify.Fulfillment;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyFulfillmentServiceService : IShopifyFulfillmentServiceService
{
    private readonly IShopifyAuthRepository _authRepository;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyFulfillmentServiceService(IShopifyAuthRepository authRepository, IShopifyApiClient apiClient)
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

    public async Task<Result<ListFulfillmentServicesResponse>> ListFulfillmentServicesAsync(string shopDomain, string? scope = null)
    {
        var (token, error) = await GetTokenAsync<ListFulfillmentServicesResponse>(shopDomain);
        if (error != null) return error;

        try
        {
            var queryParams = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(scope))
                queryParams["scope"] = scope;

            var response = await _apiClient.GetAsync(shopDomain, "fulfillment_services.json", queryParams.Count > 0 ? queryParams : null);
            var result = JsonConvert.DeserializeObject<ListFulfillmentServicesResponse>(response);
            await LogAsync(shopDomain, "ListFulfillmentServices", null, response, true, 200);
            return Result<ListFulfillmentServicesResponse>.Ok(result ?? new ListFulfillmentServicesResponse());
        }
        catch (Exception ex)
        {
            await LogAsync(shopDomain, "ListFulfillmentServices", null, ex.Message, false, 500);
            return Result<ListFulfillmentServicesResponse>.Fail(ex.Message, 500);
        }
    }

    public async Task<Result<FulfillmentServiceResponse>> GetFulfillmentServiceAsync(string shopDomain, string fulfillmentServiceId)
    {
        var (token, error) = await GetTokenAsync<FulfillmentServiceResponse>(shopDomain);
        if (error != null) return error;

        try
        {
            var response = await _apiClient.GetAsync(shopDomain, $"fulfillment_services/{fulfillmentServiceId}.json");
            var result = JsonConvert.DeserializeObject<FulfillmentServiceResponse>(response);
            await LogAsync(shopDomain, "GetFulfillmentService", null, response, true, 200);
            return Result<FulfillmentServiceResponse>.Ok(result ?? new FulfillmentServiceResponse());
        }
        catch (Exception ex)
        {
            await LogAsync(shopDomain, "GetFulfillmentService", null, ex.Message, false, 500);
            return Result<FulfillmentServiceResponse>.Fail(ex.Message, 500);
        }
    }

    public async Task<Result<FulfillmentServiceResponse>> CreateFulfillmentServiceAsync(string shopDomain, CreateFulfillmentServiceRequest request)
    {
        var (token, error) = await GetTokenAsync<FulfillmentServiceResponse>(shopDomain);
        if (error != null) return error;

        var body = new { fulfillment_service = request };
        var requestBody = JsonConvert.SerializeObject(body);

        try
        {
            var response = await _apiClient.PostAsync(shopDomain, "fulfillment_services.json", body);
            var result = JsonConvert.DeserializeObject<FulfillmentServiceResponse>(response);
            await LogAsync(shopDomain, "CreateFulfillmentService", requestBody, response, true, 201);
            return Result<FulfillmentServiceResponse>.Ok(result ?? new FulfillmentServiceResponse());
        }
        catch (Exception ex)
        {
            await LogAsync(shopDomain, "CreateFulfillmentService", requestBody, ex.Message, false, 500);
            return Result<FulfillmentServiceResponse>.Fail(ex.Message, 500);
        }
    }

    public async Task<Result<FulfillmentServiceResponse>> UpdateFulfillmentServiceAsync(string shopDomain, string fulfillmentServiceId, UpdateFulfillmentServiceRequest request)
    {
        var (token, error) = await GetTokenAsync<FulfillmentServiceResponse>(shopDomain);
        if (error != null) return error;

        var body = new { fulfillment_service = request };
        var requestBody = JsonConvert.SerializeObject(body);

        try
        {
            var response = await _apiClient.PutAsync(shopDomain, $"fulfillment_services/{fulfillmentServiceId}.json", body);
            var result = JsonConvert.DeserializeObject<FulfillmentServiceResponse>(response);
            await LogAsync(shopDomain, "UpdateFulfillmentService", requestBody, response, true, 200);
            return Result<FulfillmentServiceResponse>.Ok(result ?? new FulfillmentServiceResponse());
        }
        catch (Exception ex)
        {
            await LogAsync(shopDomain, "UpdateFulfillmentService", requestBody, ex.Message, false, 500);
            return Result<FulfillmentServiceResponse>.Fail(ex.Message, 500);
        }
    }

    public async Task<Result<bool>> DeleteFulfillmentServiceAsync(string shopDomain, string fulfillmentServiceId)
    {
        var (token, error) = await GetTokenAsync<bool>(shopDomain);
        if (error != null) return error;

        try
        {
            await _apiClient.DeleteAsync(shopDomain, $"fulfillment_services/{fulfillmentServiceId}.json");
            await LogAsync(shopDomain, "DeleteFulfillmentService", null, "Deleted", true, 200);
            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            await LogAsync(shopDomain, "DeleteFulfillmentService", null, ex.Message, false, 500);
            return Result<bool>.Fail(ex.Message, 500);
        }
    }
}
