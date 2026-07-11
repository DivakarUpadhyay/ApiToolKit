using ApiToolKit.Application.DTOs.Shopify.Content;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopify;

public interface IShopifyArticleService
{
    Task<Result<ListArticlesResponse>> ListArticlesAsync(string shopDomain, string blogId, int? limit = null);
    Task<Result<ArticleResponse>> GetArticleAsync(string shopDomain, string blogId, string articleId);
    Task<Result<int>> CountArticlesAsync(string shopDomain, string blogId);
    Task<Result<ArticleResponse>> CreateArticleAsync(string shopDomain, string blogId, CreateArticleRequest request);
    Task<Result<ArticleResponse>> UpdateArticleAsync(string shopDomain, string blogId, string articleId, UpdateArticleRequest request);
    Task<Result<bool>> DeleteArticleAsync(string shopDomain, string blogId, string articleId);
    Task<Result<List<string>>> GetArticleAuthorsAsync(string shopDomain);
    Task<Result<List<string>>> GetArticleTagsAsync(string shopDomain);
}
