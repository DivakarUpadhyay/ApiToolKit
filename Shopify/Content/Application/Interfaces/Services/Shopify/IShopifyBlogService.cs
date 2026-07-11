using ApiToolKit.Application.DTOs.Shopify.Content;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopify;

public interface IShopifyBlogService
{
    Task<Result<ListBlogsResponse>> ListBlogsAsync(string shopDomain, int? limit = null);
    Task<Result<BlogResponse>> GetBlogAsync(string shopDomain, string blogId);
    Task<Result<int>> CountBlogsAsync(string shopDomain);
    Task<Result<BlogResponse>> CreateBlogAsync(string shopDomain, CreateBlogRequest request);
    Task<Result<BlogResponse>> UpdateBlogAsync(string shopDomain, string blogId, UpdateBlogRequest request);
    Task<Result<bool>> DeleteBlogAsync(string shopDomain, string blogId);
}
