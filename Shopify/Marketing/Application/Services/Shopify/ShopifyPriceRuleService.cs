using ApiToolKit.Application.DTOs.Shopify.Marketing;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyPriceRuleService : IShopifyPriceRuleService
{
    private readonly IShopifyAuthRepository _authRepo;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyPriceRuleService(IShopifyAuthRepository authRepo, IShopifyApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListPriceRulesResponse>> ListPriceRulesAsync(string shopDomain, int? limit = null)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListPriceRulesResponse>.Fail($"No access token found for shop: {shopDomain}");

        var queryParams = new Dictionary<string, string>();
        if (limit.HasValue) queryParams["limit"] = limit.Value.ToString();

        var response = await _apiClient.GetAsync<ListPriceRulesResponse>(
            shopDomain,
            "price_rules.json",
            queryParams.Count > 0 ? queryParams : null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListPriceRules",
            RequestBody = JsonConvert.SerializeObject(queryParams),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListPriceRulesResponse>.Fail("No response from Shopify API.");

        return Result<ListPriceRulesResponse>.Ok(response);
    }

    public async Task<Result<PriceRuleResponse>> GetPriceRuleAsync(string shopDomain, string priceRuleId)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<PriceRuleResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<PriceRuleResponse>(
            shopDomain,
            $"price_rules/{priceRuleId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetPriceRule",
            RequestBody = JsonConvert.SerializeObject(new { priceRuleId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.PriceRule != null,
            StatusCode = response?.PriceRule != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<PriceRuleResponse>.Fail("No response from Shopify API.");

        return Result<PriceRuleResponse>.Ok(response);
    }

    public async Task<Result<int>> CountPriceRulesAsync(string shopDomain)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<int>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<dynamic>(
            shopDomain,
            "price_rules/count.json",
            null,
            token.AccessToken);

        int count = 0;
        if (response != null)
        {
            int.TryParse(response?.count?.ToString(), out count);
        }

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CountPriceRules",
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

    public async Task<Result<PriceRuleResponse>> CreatePriceRuleAsync(string shopDomain, CreatePriceRuleRequest request)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<PriceRuleResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { price_rule = request };

        var response = await _apiClient.PostAsync<PriceRuleResponse>(
            shopDomain,
            "price_rules.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CreatePriceRule",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.PriceRule != null,
            StatusCode = response?.PriceRule != null ? 201 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<PriceRuleResponse>.Fail("No response from Shopify API.");

        return Result<PriceRuleResponse>.Ok(response);
    }

    public async Task<Result<PriceRuleResponse>> UpdatePriceRuleAsync(string shopDomain, string priceRuleId, UpdatePriceRuleRequest request)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<PriceRuleResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { price_rule = request };

        var response = await _apiClient.PutAsync<PriceRuleResponse>(
            shopDomain,
            $"price_rules/{priceRuleId}.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "UpdatePriceRule",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.PriceRule != null,
            StatusCode = response?.PriceRule != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<PriceRuleResponse>.Fail("No response from Shopify API.");

        return Result<PriceRuleResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeletePriceRuleAsync(string shopDomain, string priceRuleId)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<bool>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.DeleteAsync<dynamic>(
            shopDomain,
            $"price_rules/{priceRuleId}.json",
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "DeletePriceRule",
            RequestBody = JsonConvert.SerializeObject(new { priceRuleId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<bool>.Ok(true);
    }
}
