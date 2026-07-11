using ApiToolKit.Application.DTOs.Shopify.Orders;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyDraftOrderService : IShopifyDraftOrderService
{
    private readonly IShopifyAuthRepository _authRepo;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyDraftOrderService(IShopifyAuthRepository authRepo, IShopifyApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListDraftOrdersResponse>> ListDraftOrdersAsync(string shopDomain, int? limit = null)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ListDraftOrdersResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListDraftOrdersResponse>.Fail($"No access token found for shop: {shopDomain}");

        var queryParams = new Dictionary<string, string>();
        if (limit.HasValue) queryParams["limit"] = limit.Value.ToString();

        var response = await _apiClient.GetAsync<ListDraftOrdersResponse>(
            shopDomain,
            "draft_orders.json",
            queryParams.Count > 0 ? queryParams : null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListDraftOrders",
            RequestBody = JsonConvert.SerializeObject(queryParams),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListDraftOrdersResponse>.Fail("No response from Shopify API.");

        return Result<ListDraftOrdersResponse>.Ok(response);
    }

    public async Task<Result<DraftOrderResponse>> GetDraftOrderAsync(string shopDomain, string draftOrderId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<DraftOrderResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<DraftOrderResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<DraftOrderResponse>(
            shopDomain,
            $"draft_orders/{draftOrderId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetDraftOrder",
            RequestBody = JsonConvert.SerializeObject(new { draftOrderId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.DraftOrder != null,
            StatusCode = response?.DraftOrder != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<DraftOrderResponse>.Fail("No response from Shopify API.");

        return Result<DraftOrderResponse>.Ok(response);
    }

    public async Task<Result<int>> CountDraftOrdersAsync(string shopDomain)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<int>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<int>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<dynamic>(
            shopDomain,
            "draft_orders/count.json",
            null,
            token.AccessToken);

        int count = 0;
        if (response != null)
            int.TryParse(response?.count?.ToString(), out count);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CountDraftOrders",
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

    public async Task<Result<DraftOrderResponse>> CreateDraftOrderAsync(string shopDomain, CreateDraftOrderRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<DraftOrderResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<DraftOrderResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { draft_order = request };

        var response = await _apiClient.PostAsync<DraftOrderResponse>(
            shopDomain,
            "draft_orders.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CreateDraftOrder",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.DraftOrder != null,
            StatusCode = response?.DraftOrder != null ? 201 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<DraftOrderResponse>.Fail("No response from Shopify API.");

        return Result<DraftOrderResponse>.Ok(response);
    }

    public async Task<Result<DraftOrderResponse>> UpdateDraftOrderAsync(string shopDomain, string draftOrderId, UpdateDraftOrderRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<DraftOrderResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<DraftOrderResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { draft_order = request };

        var response = await _apiClient.PutAsync<DraftOrderResponse>(
            shopDomain,
            $"draft_orders/{draftOrderId}.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "UpdateDraftOrder",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.DraftOrder != null,
            StatusCode = response?.DraftOrder != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<DraftOrderResponse>.Fail("No response from Shopify API.");

        return Result<DraftOrderResponse>.Ok(response);
    }

    public async Task<Result<DraftOrderResponse>> CompleteDraftOrderAsync(string shopDomain, string draftOrderId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<DraftOrderResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<DraftOrderResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.PostAsync<DraftOrderResponse>(
            shopDomain,
            $"draft_orders/{draftOrderId}/complete.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CompleteDraftOrder",
            RequestBody = JsonConvert.SerializeObject(new { draftOrderId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.DraftOrder != null,
            StatusCode = response?.DraftOrder != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<DraftOrderResponse>.Fail("No response from Shopify API.");

        return Result<DraftOrderResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteDraftOrderAsync(string shopDomain, string draftOrderId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<bool>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<bool>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.DeleteAsync<dynamic>(
            shopDomain,
            $"draft_orders/{draftOrderId}.json",
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "DeleteDraftOrder",
            RequestBody = JsonConvert.SerializeObject(new { draftOrderId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<bool>.Ok(true);
    }
}
