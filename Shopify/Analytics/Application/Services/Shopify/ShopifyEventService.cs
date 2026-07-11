using ApiToolKit.Application.DTOs.Shopify.Analytics;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyEventService : IShopifyEventService
{
    private readonly IShopifyAuthRepository _authRepo;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyEventService(IShopifyAuthRepository authRepo, IShopifyApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListEventsResponse>> ListEventsAsync(string shopDomain, int? limit = null, string? sinceId = null, string? filter = null, string? verb = null)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListEventsResponse>.Fail($"No access token found for shop: {shopDomain}");

        var queryParams = new Dictionary<string, string>();
        if (limit.HasValue) queryParams["limit"] = limit.Value.ToString();
        if (!string.IsNullOrEmpty(sinceId)) queryParams["since_id"] = sinceId;
        if (!string.IsNullOrEmpty(filter)) queryParams["filter"] = filter;
        if (!string.IsNullOrEmpty(verb)) queryParams["verb"] = verb;

        var response = await _apiClient.GetAsync<ListEventsResponse>(
            shopDomain,
            "events.json",
            queryParams.Count > 0 ? queryParams : null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListEvents",
            RequestBody = JsonConvert.SerializeObject(queryParams),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListEventsResponse>.Fail("No response from Shopify API.");

        return Result<ListEventsResponse>.Ok(response);
    }

    public async Task<Result<EventResponse>> GetEventAsync(string shopDomain, string eventId)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<EventResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<EventResponse>(
            shopDomain,
            $"events/{eventId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetEvent",
            RequestBody = JsonConvert.SerializeObject(new { eventId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Event != null,
            StatusCode = response?.Event != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<EventResponse>.Fail("No response from Shopify API.");

        return Result<EventResponse>.Ok(response);
    }

    public async Task<Result<int>> CountEventsAsync(string shopDomain, string? filter = null, string? verb = null)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<int>.Fail($"No access token found for shop: {shopDomain}");

        var queryParams = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(filter)) queryParams["filter"] = filter;
        if (!string.IsNullOrEmpty(verb)) queryParams["verb"] = verb;

        var response = await _apiClient.GetAsync<dynamic>(
            shopDomain,
            "events/count.json",
            queryParams.Count > 0 ? queryParams : null,
            token.AccessToken);

        int count = 0;
        if (response != null)
            int.TryParse(response?.count?.ToString(), out count);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CountEvents",
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
}
