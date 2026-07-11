using ApiToolKit.Application.DTOs.Shopify.Products;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopify;

public interface IShopifyCollectionService
{
    Task<Result<ListCollectionsResponse>> ListCustomCollectionsAsync(string shopDomain, int? limit = null);
    Task<Result<CollectionResponse>> GetCustomCollectionAsync(string shopDomain, string collectionId);
    Task<Result<int>> CountCustomCollectionsAsync(string shopDomain);
    Task<Result<CollectionResponse>> CreateCustomCollectionAsync(string shopDomain, CreateCollectionRequest request);
    Task<Result<CollectionResponse>> UpdateCustomCollectionAsync(string shopDomain, string collectionId, UpdateCollectionRequest request);
    Task<Result<bool>> DeleteCustomCollectionAsync(string shopDomain, string collectionId);
    Task<Result<ListCollectionsResponse>> ListSmartCollectionsAsync(string shopDomain, int? limit = null);
    Task<Result<CollectionResponse>> GetSmartCollectionAsync(string shopDomain, string collectionId);
    Task<Result<int>> CountSmartCollectionsAsync(string shopDomain);
    Task<Result<CollectionResponse>> CreateSmartCollectionAsync(string shopDomain, CreateCollectionRequest request);
    Task<Result<CollectionResponse>> UpdateSmartCollectionAsync(string shopDomain, string collectionId, UpdateCollectionRequest request);
    Task<Result<bool>> DeleteSmartCollectionAsync(string shopDomain, string collectionId);
}
