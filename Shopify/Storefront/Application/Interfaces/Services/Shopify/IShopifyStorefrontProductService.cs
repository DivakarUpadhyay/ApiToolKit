using ApiToolKit.Application.DTOs.Shopify.Storefront;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Shopify;
public interface IShopifyStorefrontProductService
{
    Task<Result<StorefrontProductsResponse>> GetProductsAsync(string shopDomain, int first = 10, string? after = null);
    Task<Result<StorefrontProductResponse>> GetProductByHandleAsync(string shopDomain, string handle);
    Task<Result<StorefrontProductResponse>> GetProductByIdAsync(string shopDomain, string productId);
    Task<Result<StorefrontCollectionsResponse>> GetCollectionsAsync(string shopDomain, int first = 10);
    Task<Result<StorefrontCollectionResponse>> GetCollectionByHandleAsync(string shopDomain, string handle);
}
