using ApiToolKit.Application.DTOs.Shopify.Analytics;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyPaymentsService : IShopifyPaymentsService
{
    private readonly IShopifyAuthRepository _authRepo;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyPaymentsService(IShopifyAuthRepository authRepo, IShopifyApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListPayoutsResponse>> ListPayoutsAsync(string shopDomain, string? status = null, int? limit = null)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListPayoutsResponse>.Fail($"No access token found for shop: {shopDomain}");

        var queryParams = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(status)) queryParams["status"] = status;
        if (limit.HasValue) queryParams["limit"] = limit.Value.ToString();

        var response = await _apiClient.GetAsync<ListPayoutsResponse>(
            shopDomain,
            "shopify_payments/payouts.json",
            queryParams.Count > 0 ? queryParams : null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListPayouts",
            RequestBody = JsonConvert.SerializeObject(queryParams),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListPayoutsResponse>.Fail("No response from Shopify API.");

        return Result<ListPayoutsResponse>.Ok(response);
    }

    public async Task<Result<PayoutResponse>> GetPayoutAsync(string shopDomain, string payoutId)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<PayoutResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<PayoutResponse>(
            shopDomain,
            $"shopify_payments/payouts/{payoutId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetPayout",
            RequestBody = JsonConvert.SerializeObject(new { payoutId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Payout != null,
            StatusCode = response?.Payout != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<PayoutResponse>.Fail("No response from Shopify API.");

        return Result<PayoutResponse>.Ok(response);
    }

    public async Task<Result<ListDisputesResponse>> ListDisputesAsync(string shopDomain, string? initiatedAt = null, int? limit = null)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListDisputesResponse>.Fail($"No access token found for shop: {shopDomain}");

        var queryParams = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(initiatedAt)) queryParams["initiated_at"] = initiatedAt;
        if (limit.HasValue) queryParams["limit"] = limit.Value.ToString();

        var response = await _apiClient.GetAsync<ListDisputesResponse>(
            shopDomain,
            "shopify_payments/disputes.json",
            queryParams.Count > 0 ? queryParams : null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListDisputes",
            RequestBody = JsonConvert.SerializeObject(queryParams),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListDisputesResponse>.Fail("No response from Shopify API.");

        return Result<ListDisputesResponse>.Ok(response);
    }

    public async Task<Result<DisputeResponse>> GetDisputeAsync(string shopDomain, string disputeId)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<DisputeResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<DisputeResponse>(
            shopDomain,
            $"shopify_payments/disputes/{disputeId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetDispute",
            RequestBody = JsonConvert.SerializeObject(new { disputeId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Dispute != null,
            StatusCode = response?.Dispute != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<DisputeResponse>.Fail("No response from Shopify API.");

        return Result<DisputeResponse>.Ok(response);
    }

    public async Task<Result<ListBalanceTransactionsResponse>> ListBalanceTransactionsAsync(string shopDomain, string? payoutId = null, int? limit = null)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListBalanceTransactionsResponse>.Fail($"No access token found for shop: {shopDomain}");

        var queryParams = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(payoutId)) queryParams["payout_id"] = payoutId;
        if (limit.HasValue) queryParams["limit"] = limit.Value.ToString();

        var response = await _apiClient.GetAsync<ListBalanceTransactionsResponse>(
            shopDomain,
            "shopify_payments/balance/transactions.json",
            queryParams.Count > 0 ? queryParams : null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListBalanceTransactions",
            RequestBody = JsonConvert.SerializeObject(queryParams),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListBalanceTransactionsResponse>.Fail("No response from Shopify API.");

        return Result<ListBalanceTransactionsResponse>.Ok(response);
    }
}
