using ApiToolKit.Application.DTOs.Shopify.Content;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyArticleService : IShopifyArticleService
{
    private readonly IShopifyAuthRepository _authRepo;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyArticleService(IShopifyAuthRepository authRepo, IShopifyApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListArticlesResponse>> ListArticlesAsync(string shopDomain, string blogId, int? limit = null)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ListArticlesResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListArticlesResponse>.Fail($"No access token found for shop: {shopDomain}");

        var queryParams = new Dictionary<string, string>();
        if (limit.HasValue) queryParams["limit"] = limit.Value.ToString();

        var response = await _apiClient.GetAsync<ListArticlesResponse>(
            shopDomain,
            $"blogs/{blogId}/articles.json",
            queryParams.Count > 0 ? queryParams : null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListArticles",
            RequestBody = JsonConvert.SerializeObject(new { blogId, queryParams }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListArticlesResponse>.Fail("No response from Shopify API.");

        return Result<ListArticlesResponse>.Ok(response);
    }

    public async Task<Result<ArticleResponse>> GetArticleAsync(string shopDomain, string blogId, string articleId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ArticleResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ArticleResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<ArticleResponse>(
            shopDomain,
            $"blogs/{blogId}/articles/{articleId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetArticle",
            RequestBody = JsonConvert.SerializeObject(new { blogId, articleId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Article != null,
            StatusCode = response?.Article != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ArticleResponse>.Fail("No response from Shopify API.");

        return Result<ArticleResponse>.Ok(response);
    }

    public async Task<Result<int>> CountArticlesAsync(string shopDomain, string blogId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<int>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<int>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<dynamic>(
            shopDomain,
            $"blogs/{blogId}/articles/count.json",
            null,
            token.AccessToken);

        int count = 0;
        if (response != null)
            int.TryParse(response?.count?.ToString(), out count);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CountArticles",
            RequestBody = JsonConvert.SerializeObject(new { blogId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<int>.Fail("No response from Shopify API.");

        return Result<int>.Ok(count);
    }

    public async Task<Result<ArticleResponse>> CreateArticleAsync(string shopDomain, string blogId, CreateArticleRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ArticleResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ArticleResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { article = request };

        var response = await _apiClient.PostAsync<ArticleResponse>(
            shopDomain,
            $"blogs/{blogId}/articles.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CreateArticle",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Article != null,
            StatusCode = response?.Article != null ? 201 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ArticleResponse>.Fail("No response from Shopify API.");

        return Result<ArticleResponse>.Ok(response);
    }

    public async Task<Result<ArticleResponse>> UpdateArticleAsync(string shopDomain, string blogId, string articleId, UpdateArticleRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<ArticleResponse>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ArticleResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { article = request };

        var response = await _apiClient.PutAsync<ArticleResponse>(
            shopDomain,
            $"blogs/{blogId}/articles/{articleId}.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "UpdateArticle",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Article != null,
            StatusCode = response?.Article != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ArticleResponse>.Fail("No response from Shopify API.");

        return Result<ArticleResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteArticleAsync(string shopDomain, string blogId, string articleId)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<bool>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<bool>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.DeleteAsync<dynamic>(
            shopDomain,
            $"blogs/{blogId}/articles/{articleId}.json",
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "DeleteArticle",
            RequestBody = JsonConvert.SerializeObject(new { blogId, articleId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<bool>.Ok(true);
    }

    public async Task<Result<List<string>>> GetArticleAuthorsAsync(string shopDomain)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<List<string>>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<List<string>>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<List<string>>(
            shopDomain,
            "articles/authors.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetArticleAuthors",
            RequestBody = null,
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<List<string>>.Fail("No response from Shopify API.");

        return Result<List<string>>.Ok(response);
    }

    public async Task<Result<List<string>>> GetArticleTagsAsync(string shopDomain)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<List<string>>.Fail("Shopify configuration not found.");

        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<List<string>>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<List<string>>(
            shopDomain,
            "articles/tags.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetArticleTags",
            RequestBody = null,
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<List<string>>.Fail("No response from Shopify API.");

        return Result<List<string>>.Ok(response);
    }
}
