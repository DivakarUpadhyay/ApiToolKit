using ApiToolKit.Application.DTOs.Shopify.Marketing;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyDiscountCodeService : IShopifyDiscountCodeService
{
    private readonly IShopifyAuthRepository _authRepo;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyDiscountCodeService(IShopifyAuthRepository authRepo, IShopifyApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListDiscountCodesResponse>> ListDiscountCodesAsync(string shopDomain, string priceRuleId, int? limit = null)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListDiscountCodesResponse>.Fail($"No access token found for shop: {shopDomain}");

        var queryParams = new Dictionary<string, string>();
        if (limit.HasValue) queryParams["limit"] = limit.Value.ToString();

        var response = await _apiClient.GetAsync<ListDiscountCodesResponse>(
            shopDomain,
            $"price_rules/{priceRuleId}/discount_codes.json",
            queryParams.Count > 0 ? queryParams : null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListDiscountCodes",
            RequestBody = JsonConvert.SerializeObject(new { priceRuleId, queryParams }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListDiscountCodesResponse>.Fail("No response from Shopify API.");

        return Result<ListDiscountCodesResponse>.Ok(response);
    }

    public async Task<Result<DiscountCodeResponse>> GetDiscountCodeAsync(string shopDomain, string priceRuleId, string discountCodeId)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<DiscountCodeResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<DiscountCodeResponse>(
            shopDomain,
            $"price_rules/{priceRuleId}/discount_codes/{discountCodeId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetDiscountCode",
            RequestBody = JsonConvert.SerializeObject(new { priceRuleId, discountCodeId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.DiscountCode != null,
            StatusCode = response?.DiscountCode != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<DiscountCodeResponse>.Fail("No response from Shopify API.");

        return Result<DiscountCodeResponse>.Ok(response);
    }

    public async Task<Result<int>> CountDiscountCodesAsync(string shopDomain, string priceRuleId)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<int>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<dynamic>(
            shopDomain,
            $"price_rules/{priceRuleId}/discount_codes/count.json",
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
            ApiName = "CountDiscountCodes",
            RequestBody = JsonConvert.SerializeObject(new { priceRuleId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<int>.Fail("No response from Shopify API.");

        return Result<int>.Ok(count);
    }

    public async Task<Result<DiscountCodeResponse>> CreateDiscountCodeAsync(string shopDomain, string priceRuleId, CreateDiscountCodeRequest request)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<DiscountCodeResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { discount_code = request };

        var response = await _apiClient.PostAsync<DiscountCodeResponse>(
            shopDomain,
            $"price_rules/{priceRuleId}/discount_codes.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CreateDiscountCode",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.DiscountCode != null,
            StatusCode = response?.DiscountCode != null ? 201 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<DiscountCodeResponse>.Fail("No response from Shopify API.");

        return Result<DiscountCodeResponse>.Ok(response);
    }

    public async Task<Result<DiscountCodeResponse>> UpdateDiscountCodeAsync(string shopDomain, string priceRuleId, string discountCodeId, UpdateDiscountCodeRequest request)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<DiscountCodeResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { discount_code = request };

        var response = await _apiClient.PutAsync<DiscountCodeResponse>(
            shopDomain,
            $"price_rules/{priceRuleId}/discount_codes/{discountCodeId}.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "UpdateDiscountCode",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.DiscountCode != null,
            StatusCode = response?.DiscountCode != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<DiscountCodeResponse>.Fail("No response from Shopify API.");

        return Result<DiscountCodeResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteDiscountCodeAsync(string shopDomain, string priceRuleId, string discountCodeId)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<bool>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.DeleteAsync<dynamic>(
            shopDomain,
            $"price_rules/{priceRuleId}/discount_codes/{discountCodeId}.json",
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "DeleteDiscountCode",
            RequestBody = JsonConvert.SerializeObject(new { priceRuleId, discountCodeId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<bool>.Ok(true);
    }

    public async Task<Result<BatchDiscountCodeResponse>> CreateBatchDiscountCodesAsync(string shopDomain, string priceRuleId, BatchDiscountCodeRequest request)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<BatchDiscountCodeResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { discount_code_creation_job = new { codes = request.Codes } };

        var response = await _apiClient.PostAsync<BatchDiscountCodeResponse>(
            shopDomain,
            $"price_rules/{priceRuleId}/batch_discount_codes.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CreateBatchDiscountCodes",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.DiscountCodeCreationJob != null,
            StatusCode = response?.DiscountCodeCreationJob != null ? 201 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<BatchDiscountCodeResponse>.Fail("No response from Shopify API.");

        return Result<BatchDiscountCodeResponse>.Ok(response);
    }

    public async Task<Result<BatchDiscountCodeResponse>> GetBatchDiscountCodeStatusAsync(string shopDomain, string priceRuleId, string batchId)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<BatchDiscountCodeResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<BatchDiscountCodeResponse>(
            shopDomain,
            $"price_rules/{priceRuleId}/batch_discount_codes/{batchId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetBatchDiscountCodeStatus",
            RequestBody = JsonConvert.SerializeObject(new { priceRuleId, batchId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.DiscountCodeCreationJob != null,
            StatusCode = response?.DiscountCodeCreationJob != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<BatchDiscountCodeResponse>.Fail("No response from Shopify API.");

        return Result<BatchDiscountCodeResponse>.Ok(response);
    }
}
