using ApiToolKit.Application.DTOs.Shopify.Content;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopify;

public interface IShopifyMetafieldService
{
    Task<Result<ListMetafieldsResponse>> ListMetafieldsAsync(string shopDomain, string? resource = null, string? resourceId = null, int? limit = null);
    Task<Result<MetafieldResponse>> GetMetafieldAsync(string shopDomain, string metafieldId);
    Task<Result<int>> CountMetafieldsAsync(string shopDomain, string? resource = null, string? resourceId = null);
    Task<Result<MetafieldResponse>> CreateMetafieldAsync(string shopDomain, CreateMetafieldRequest request, string? resource = null, string? resourceId = null);
    Task<Result<MetafieldResponse>> UpdateMetafieldAsync(string shopDomain, string metafieldId, UpdateMetafieldRequest request);
    Task<Result<bool>> DeleteMetafieldAsync(string shopDomain, string metafieldId);
}
