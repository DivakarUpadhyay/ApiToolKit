using ApiToolKit.Application.DTOs.Shopify.Content;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopify;

public interface IShopifyPageService
{
    Task<Result<ListPagesResponse>> ListPagesAsync(string shopDomain, int? limit = null, string? sinceId = null);
    Task<Result<PageResponse>> GetPageAsync(string shopDomain, string pageId);
    Task<Result<int>> CountPagesAsync(string shopDomain);
    Task<Result<PageResponse>> CreatePageAsync(string shopDomain, CreatePageRequest request);
    Task<Result<PageResponse>> UpdatePageAsync(string shopDomain, string pageId, UpdatePageRequest request);
    Task<Result<bool>> DeletePageAsync(string shopDomain, string pageId);
}
