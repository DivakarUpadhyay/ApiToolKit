using ApiToolKit.Application.DTOs.Shopify.Products;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopify;

public interface IShopifyProductService
{
    Task<Result<ListProductsResponse>> ListProductsAsync(string shopDomain, int? limit = null, string? status = null);
    Task<Result<ProductResponse>> GetProductAsync(string shopDomain, string productId);
    Task<Result<int>> CountProductsAsync(string shopDomain, string? status = null);
    Task<Result<ProductResponse>> CreateProductAsync(string shopDomain, CreateProductRequest request);
    Task<Result<ProductResponse>> UpdateProductAsync(string shopDomain, string productId, UpdateProductRequest request);
    Task<Result<bool>> DeleteProductAsync(string shopDomain, string productId);
}
