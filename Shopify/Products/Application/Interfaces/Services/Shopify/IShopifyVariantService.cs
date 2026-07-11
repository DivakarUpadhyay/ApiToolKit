using ApiToolKit.Application.DTOs.Shopify.Products;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopify;

public interface IShopifyVariantService
{
    Task<Result<ListVariantsResponse>> ListVariantsAsync(string shopDomain, string productId, int? limit = null);
    Task<Result<VariantResponse>> GetVariantAsync(string shopDomain, string variantId);
    Task<Result<int>> CountVariantsAsync(string shopDomain, string productId);
    Task<Result<VariantResponse>> CreateVariantAsync(string shopDomain, string productId, CreateVariantRequest request);
    Task<Result<VariantResponse>> UpdateVariantAsync(string shopDomain, string variantId, UpdateVariantRequest request);
    Task<Result<bool>> DeleteVariantAsync(string shopDomain, string productId, string variantId);
}
