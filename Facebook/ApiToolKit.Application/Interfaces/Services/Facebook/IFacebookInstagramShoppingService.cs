using ApiToolKit.Application.DTOs.Facebook.InstagramShopping;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Facebook;
public interface IFacebookInstagramShoppingService
{
    Task<Result<IgProductCatalog>> GetLinkedCatalogAsync(string igUserId, string accessToken);
    Task<Result<bool>> TagProductsOnMediaAsync(string mediaId, TagProductsRequest request, string accessToken);
    Task<Result<IgProductTagsResponse>> GetProductTagsOnMediaAsync(string mediaId, string accessToken);
    Task<Result<bool>> DeleteProductTagsOnMediaAsync(string mediaId, string accessToken);
    Task<Result<IgTaggedMediaResponse>> GetTaggedProductMediaAsync(string igUserId, string accessToken);
    Task<Result<bool>> AppealProductAsync(string igUserId, ProductAppealRequest request, string accessToken);
    Task<Result<IgShoppingEligibility>> GetShoppingEligibilityAsync(string igUserId, string accessToken);
    Task<Result<IgCatalogProductsResponse>> GetCatalogProductsAsync(string catalogId, string accessToken);
    Task<Result<IgShopsResponse>> GetIgShopsAsync(string igUserId, string accessToken);
    Task<Result<AvailableCatalogsResponse>> GetAvailableCatalogsAsync(string igUserId, string accessToken);
}
