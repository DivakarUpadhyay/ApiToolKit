using ApiToolKit.Application.DTOs.Shopify.Orders;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyRefundService : IShopifyRefundService
{
    private readonly IShopifyAuthRepository _authRepo;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyRefundService(IShopifyAuthRepository authRepo, IShopifyApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListRefundsResponse>> ListRefundsAsync(string shopDomain, string orderId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ListRefundsResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListRefundsResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<ListRefundsResponse>(
            shopDomain,
            $"orders/{orderId}/refunds.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListRefunds",
            RequestBody = JsonConvert.SerializeObject(new { orderId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListRefundsResponse>.Fail("No response from Shopify API.");

        return Result<ListRefundsResponse>.Ok(response);
    }

    public async Task<Result<RefundResponse>> GetRefundAsync(string shopDomain, string orderId, string refundId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<RefundResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<RefundResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<RefundResponse>(
            shopDomain,
            $"orders/{orderId}/refunds/{refundId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetRefund",
            RequestBody = JsonConvert.SerializeObject(new { orderId, refundId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Refund != null,
            StatusCode = response?.Refund != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<RefundResponse>.Fail("No response from Shopify API.");

        return Result<RefundResponse>.Ok(response);
    }

    public async Task<Result<RefundResponse>> CreateRefundAsync(string shopDomain, string orderId, CreateRefundRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<RefundResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<RefundResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { refund = request };

        var response = await _apiClient.PostAsync<RefundResponse>(
            shopDomain,
            $"orders/{orderId}/refunds.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CreateRefund",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Refund != null,
            StatusCode = response?.Refund != null ? 201 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<RefundResponse>.Fail("No response from Shopify API.");

        return Result<RefundResponse>.Ok(response);
    }

    public async Task<Result<RefundResponse>> CalculateRefundAsync(string shopDomain, string orderId, CalculateRefundRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<RefundResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<RefundResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { refund = request };

        var response = await _apiClient.PostAsync<RefundResponse>(
            shopDomain,
            $"orders/{orderId}/refunds/calculate.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CalculateRefund",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Refund != null,
            StatusCode = response?.Refund != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<RefundResponse>.Fail("No response from Shopify API.");

        return Result<RefundResponse>.Ok(response);
    }
}
