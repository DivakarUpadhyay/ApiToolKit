using ApiToolKit.Application.DTOs.Shopify.Content;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyBlogService : IShopifyBlogService
{
    private readonly IShopifyAuthRepository _authRepo;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyBlogService(IShopifyAuthRepository authRepo, IShopifyApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListBlogsResponse>> ListBlogsAsync(string shopDomain, int? limit = null)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ListBlogsResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListBlogsResponse>.Fail($"No access token found for shop: {shopDomain}");

        var queryParams = new Dictionary<string, string>();
        if (limit.HasValue) queryParams["limit"] = limit.Value.ToString();

        var response = await _apiClient.GetAsync<ListBlogsResponse>(
            shopDomain,
            "blogs.json",
            queryParams.Count > 0 ? queryParams : null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListBlogs",
            RequestBody = JsonConvert.SerializeObject(queryParams),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListBlogsResponse>.Fail("No response from Shopify API.");

        return Result<ListBlogsResponse>.Ok(response);
    }

    public async Task<Result<BlogResponse>> GetBlogAsync(string shopDomain, string blogId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<BlogResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<BlogResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<BlogResponse>(
            shopDomain,
            $"blogs/{blogId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetBlog",
            RequestBody = JsonConvert.SerializeObject(new { blogId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Blog != null,
            StatusCode = response?.Blog != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<BlogResponse>.Fail("No response from Shopify API.");

        return Result<BlogResponse>.Ok(response);
    }

    public async Task<Result<int>> CountBlogsAsync(string shopDomain)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<int>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<int>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<dynamic>(
            shopDomain,
            "blogs/count.json",
            null,
            token.AccessToken);

        int count = 0;
        if (response != null)
            int.TryParse(response?.count?.ToString(), out count);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CountBlogs",
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

    public async Task<Result<BlogResponse>> CreateBlogAsync(string shopDomain, CreateBlogRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<BlogResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<BlogResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { blog = request };

        var response = await _apiClient.PostAsync<BlogResponse>(
            shopDomain,
            "blogs.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CreateBlog",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Blog != null,
            StatusCode = response?.Blog != null ? 201 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<BlogResponse>.Fail("No response from Shopify API.");

        return Result<BlogResponse>.Ok(response);
    }

    public async Task<Result<BlogResponse>> UpdateBlogAsync(string shopDomain, string blogId, UpdateBlogRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<BlogResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<BlogResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { blog = request };

        var response = await _apiClient.PutAsync<BlogResponse>(
            shopDomain,
            $"blogs/{blogId}.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "UpdateBlog",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Blog != null,
            StatusCode = response?.Blog != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<BlogResponse>.Fail("No response from Shopify API.");

        return Result<BlogResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteBlogAsync(string shopDomain, string blogId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<bool>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<bool>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.DeleteAsync<dynamic>(
            shopDomain,
            $"blogs/{blogId}.json",
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "DeleteBlog",
            RequestBody = JsonConvert.SerializeObject(new { blogId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<bool>.Ok(true);
    }
}
