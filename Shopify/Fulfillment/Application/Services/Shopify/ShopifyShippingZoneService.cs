using ApiToolKit.Application.DTOs.Shopify.Fulfillment;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyShippingZoneService : IShopifyShippingZoneService
{
    private readonly IShopifyAuthRepository _authRepository;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyShippingZoneService(IShopifyAuthRepository authRepository, IShopifyApiClient apiClient)
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

    public async Task<Result<ListShippingZonesResponse>> ListShippingZonesAsync(string shopDomain)
    {
        var (token, error) = await GetTokenAsync<ListShippingZonesResponse>(shopDomain);
        if (error != null) return error;

        try
        {
            var response = await _apiClient.GetAsync(shopDomain, "shipping_zones.json");
            var result = JsonConvert.DeserializeObject<ListShippingZonesResponse>(response);
            await LogAsync(shopDomain, "ListShippingZones", null, response, true, 200);
            return Result<ListShippingZonesResponse>.Ok(result ?? new ListShippingZonesResponse());
        }
        catch (Exception ex)
        {
            await LogAsync(shopDomain, "ListShippingZones", null, ex.Message, false, 500);
            return Result<ListShippingZonesResponse>.Fail(ex.Message, 500);
        }
    }
}
