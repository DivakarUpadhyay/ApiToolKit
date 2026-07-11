using ApiToolKit.Application.DTOs.Shopify.Content;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyPageService : IShopifyPageService
{
    private readonly IShopifyAuthRepository _authRepo;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyPageService(IShopifyAuthRepository authRepo, IShopifyApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListPagesResponse>> ListPagesAsync(string shopDomain, int? limit = null, string? sinceId = null)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ListPagesResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListPagesResponse>.Fail($"No access token found for shop: {shopDomain}");

        var queryParams = new Dictionary<string, string>();
        if (limit.HasValue) queryParams["limit"] = limit.Value.ToString();
        if (!string.IsNullOrEmpty(sinceId)) queryParams["since_id"] = sinceId;

        var response = await _apiClient.GetAsync<ListPagesResponse>(
            shopDomain,
            "pages.json",
            queryParams.Count > 0 ? queryParams : null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListPages",
            RequestBody = JsonConvert.SerializeObject(queryParams),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListPagesResponse>.Fail("No response from Shopify API.");

        return Result<ListPagesResponse>.Ok(response);
    }

    public async Task<Result<PageResponse>> GetPageAsync(string shopDomain, string pageId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<PageResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<PageResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<PageResponse>(
            shopDomain,
            $"pages/{pageId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetPage",
            RequestBody = JsonConvert.SerializeObject(new { pageId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Page != null,
            StatusCode = response?.Page != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<PageResponse>.Fail("No response from Shopify API.");

        return Result<PageResponse>.Ok(response);
    }

    public async Task<Result<int>> CountPagesAsync(string shopDomain)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<int>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<int>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<dynamic>(
            shopDomain,
            "pages/count.json",
            null,
            token.AccessToken);

        int count = 0;
        if (response != null)
            int.TryParse(response?.count?.ToString(), out count);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CountPages",
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

    public async Task<Result<PageResponse>> CreatePageAsync(string shopDomain, CreatePageRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<PageResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<PageResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { page = request };

        var response = await _apiClient.PostAsync<PageResponse>(
            shopDomain,
            "pages.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CreatePage",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Page != null,
            StatusCode = response?.Page != null ? 201 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<PageResponse>.Fail("No response from Shopify API.");

        return Result<PageResponse>.Ok(response);
    }

    public async Task<Result<PageResponse>> UpdatePageAsync(string shopDomain, string pageId, UpdatePageRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<PageResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<PageResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { page = request };

        var response = await _apiClient.PutAsync<PageResponse>(
            shopDomain,
            $"pages/{pageId}.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "UpdatePage",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Page != null,
            StatusCode = response?.Page != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<PageResponse>.Fail("No response from Shopify API.");

        return Result<PageResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeletePageAsync(string shopDomain, string pageId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<bool>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<bool>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.DeleteAsync<dynamic>(
            shopDomain,
            $"pages/{pageId}.json",
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "DeletePage",
            RequestBody = JsonConvert.SerializeObject(new { pageId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<bool>.Ok(true);
    }
}
