using ApiToolKit.Application.DTOs.Shopify.Orders;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyTransactionService : IShopifyTransactionService
{
    private readonly IShopifyAuthRepository _authRepo;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyTransactionService(IShopifyAuthRepository authRepo, IShopifyApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListTransactionsResponse>> ListTransactionsAsync(string shopDomain, string orderId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ListTransactionsResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListTransactionsResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<ListTransactionsResponse>(
            shopDomain,
            $"orders/{orderId}/transactions.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListTransactions",
            RequestBody = JsonConvert.SerializeObject(new { orderId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListTransactionsResponse>.Fail("No response from Shopify API.");

        return Result<ListTransactionsResponse>.Ok(response);
    }

    public async Task<Result<TransactionResponse>> GetTransactionAsync(string shopDomain, string orderId, string transactionId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<TransactionResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<TransactionResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<TransactionResponse>(
            shopDomain,
            $"orders/{orderId}/transactions/{transactionId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetTransaction",
            RequestBody = JsonConvert.SerializeObject(new { orderId, transactionId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Transaction != null,
            StatusCode = response?.Transaction != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<TransactionResponse>.Fail("No response from Shopify API.");

        return Result<TransactionResponse>.Ok(response);
    }

    public async Task<Result<int>> CountTransactionsAsync(string shopDomain, string orderId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<int>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<int>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<dynamic>(
            shopDomain,
            $"orders/{orderId}/transactions/count.json",
            null,
            token.AccessToken);

        int count = 0;
        if (response != null)
            int.TryParse(response?.count?.ToString(), out count);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CountTransactions",
            RequestBody = JsonConvert.SerializeObject(new { orderId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<int>.Fail("No response from Shopify API.");

        return Result<int>.Ok(count);
    }

    public async Task<Result<TransactionResponse>> CreateTransactionAsync(string shopDomain, string orderId, CreateTransactionRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<TransactionResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<TransactionResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { transaction = request };

        var response = await _apiClient.PostAsync<TransactionResponse>(
            shopDomain,
            $"orders/{orderId}/transactions.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CreateTransaction",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Transaction != null,
            StatusCode = response?.Transaction != null ? 201 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<TransactionResponse>.Fail("No response from Shopify API.");

        return Result<TransactionResponse>.Ok(response);
    }
}
