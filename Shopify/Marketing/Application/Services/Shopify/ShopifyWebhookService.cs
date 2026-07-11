using ApiToolKit.Application.DTOs.Shopify.Marketing;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyWebhookService : IShopifyWebhookService
{
    private readonly IShopifyAuthRepository _authRepo;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyWebhookService(IShopifyAuthRepository authRepo, IShopifyApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListWebhooksResponse>> ListWebhooksAsync(string shopDomain, string? topic = null, int? limit = null)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListWebhooksResponse>.Fail($"No access token found for shop: {shopDomain}");

        var queryParams = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(topic)) queryParams["topic"] = topic;
        if (limit.HasValue) queryParams["limit"] = limit.Value.ToString();

        var response = await _apiClient.GetAsync<ListWebhooksResponse>(
            shopDomain,
            "webhooks.json",
            queryParams.Count > 0 ? queryParams : null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListWebhooks",
            RequestBody = JsonConvert.SerializeObject(queryParams),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListWebhooksResponse>.Fail("No response from Shopify API.");

        return Result<ListWebhooksResponse>.Ok(response);
    }

    public async Task<Result<WebhookResponse>> GetWebhookAsync(string shopDomain, string webhookId)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<WebhookResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<WebhookResponse>(
            shopDomain,
            $"webhooks/{webhookId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetWebhook",
            RequestBody = JsonConvert.SerializeObject(new { webhookId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Webhook != null,
            StatusCode = response?.Webhook != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<WebhookResponse>.Fail("No response from Shopify API.");

        return Result<WebhookResponse>.Ok(response);
    }

    public async Task<Result<int>> CountWebhooksAsync(string shopDomain, string? topic = null)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<int>.Fail($"No access token found for shop: {shopDomain}");

        var queryParams = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(topic)) queryParams["topic"] = topic;

        var response = await _apiClient.GetAsync<dynamic>(
            shopDomain,
            "webhooks/count.json",
            queryParams.Count > 0 ? queryParams : null,
            token.AccessToken);

        int count = 0;
        if (response != null)
        {
            int.TryParse(response?.count?.ToString(), out count);
        }

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CountWebhooks",
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

    public async Task<Result<WebhookResponse>> CreateWebhookAsync(string shopDomain, CreateWebhookRequest request)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<WebhookResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { webhook = request };

        var response = await _apiClient.PostAsync<WebhookResponse>(
            shopDomain,
            "webhooks.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CreateWebhook",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Webhook != null,
            StatusCode = response?.Webhook != null ? 201 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<WebhookResponse>.Fail("No response from Shopify API.");

        return Result<WebhookResponse>.Ok(response);
    }

    public async Task<Result<WebhookResponse>> UpdateWebhookAsync(string shopDomain, string webhookId, UpdateWebhookRequest request)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<WebhookResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { webhook = request };

        var response = await _apiClient.PutAsync<WebhookResponse>(
            shopDomain,
            $"webhooks/{webhookId}.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "UpdateWebhook",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Webhook != null,
            StatusCode = response?.Webhook != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<WebhookResponse>.Fail("No response from Shopify API.");

        return Result<WebhookResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteWebhookAsync(string shopDomain, string webhookId)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<bool>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.DeleteAsync<dynamic>(
            shopDomain,
            $"webhooks/{webhookId}.json",
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "DeleteWebhook",
            RequestBody = JsonConvert.SerializeObject(new { webhookId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<bool>.Ok(true);
    }
}
