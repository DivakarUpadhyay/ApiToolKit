using ApiToolKit.Application.DTOs.Facebook.Commerce;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Facebook;
public interface IFacebookCommerceService
{
    Task<Result<FbShopsResponse>> GetShopsAsync(string pageId, string accessToken);
    Task<Result<FbShop>> GetShopAsync(string shopId, string accessToken);
    Task<Result<CommerceOrdersResponse>> GetOrdersAsync(string pageId, string accessToken, string? state = null);
    Task<Result<CommerceOrder>> GetOrderAsync(string orderId, string accessToken);
    Task<Result<bool>> AcknowledgeOrderAsync(string orderId, AcknowledgeOrderRequest request, string accessToken);
    Task<Result<bool>> AddShipmentAsync(string orderId, AddShipmentRequest request, string accessToken);
    Task<Result<bool>> CancelOrderAsync(string orderId, CancelOrderRequest request, string accessToken);
    Task<Result<bool>> RefundOrderAsync(string orderId, RefundOrderRequest request, string accessToken);
    Task<Result<FulfillmentsResponse>> GetFulfillmentsAsync(string orderId, string accessToken);
    Task<Result<ReturnsResponse>> GetReturnsAsync(string orderId, string accessToken);
    Task<Result<CommerceAccount>> GetCommerceAccountAsync(string pageId, string accessToken);
    Task<Result<CommerceSettings>> GetCommerceSettingsAsync(string commerceAccountId, string accessToken);
    Task<Result<TaxSettings>> GetTaxSettingsAsync(string commerceAccountId, string accessToken);
    Task<Result<bool>> UpdateTaxSettingsAsync(string commerceAccountId, UpdateTaxSettingsRequest request, string accessToken);
    Task<Result<ReturnSettings>> GetReturnSettingsAsync(string commerceAccountId, string accessToken);
    Task<Result<bool>> UpdateReturnSettingsAsync(string commerceAccountId, UpdateReturnSettingsRequest request, string accessToken);
    Task<Result<ShippingProfilesResponse>> GetShippingProfilesAsync(string commerceAccountId, string accessToken);
    Task<Result<ShopCollectionsResponse>> GetShopCollectionsAsync(string shopId, string accessToken);

    // Shop setup
    Task<Result<object>> CreateShopAsync(CreateShopRequest request, string accessToken);
    Task<Result<bool>> UpdateShopAsync(UpdateShopRequest request, string accessToken);

    // Catalog connection to commerce account
    Task<Result<ConnectedCatalogsResponse>> GetConnectedCatalogsAsync(string commerceAccountId, string accessToken);
    Task<Result<bool>> ConnectCatalogAsync(ConnectCatalogRequest request, string accessToken);
    Task<Result<bool>> DisconnectCatalogAsync(string commerceAccountId, string catalogId, string accessToken);

    // Collections (product groupings visible in the Shop)
    Task<Result<List<CollectionResponse>>> GetCollectionsAsync(string commerceAccountId, string accessToken);
    Task<Result<object>> CreateCollectionAsync(CreateCollectionRequest request, string accessToken);
    Task<Result<bool>> UpdateCollectionAsync(UpdateCollectionRequest request, string accessToken);
    Task<Result<bool>> DeleteCollectionAsync(string collectionId, string accessToken);
    Task<Result<CollectionProductsResponse>> GetCollectionProductsAsync(string collectionId, string accessToken);
    Task<Result<bool>> AddProductToCollectionAsync(AddProductToCollectionRequest request, string accessToken);
    Task<Result<bool>> RemoveProductFromCollectionAsync(RemoveProductFromCollectionRequest request, string accessToken);
}
