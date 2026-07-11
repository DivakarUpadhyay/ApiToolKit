using ApiToolKit.Application.DTOs.Facebook.Catalog;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookCatalogService
{
    Task<Result<CreateCatalogResponse>> CreateCatalogAsync(CreateCatalogRequest request, string accessToken);
    Task<Result<CatalogResponse>> GetCatalogAsync(string catalogId, string accessToken);
    Task<Result<GetCatalogsResponse>> GetCatalogsAsync(GetCatalogsRequest request, string accessToken);
    Task<Result<bool>> DeleteCatalogAsync(string catalogId, string accessToken);
    Task<Result<CreateProductResponse>> CreateProductAsync(CreateProductRequest request, string accessToken);
    Task<Result<ProductResponse>> GetProductAsync(string productId, string accessToken);
    Task<Result<GetProductsResponse>> GetProductsAsync(GetProductsRequest request, string accessToken);
    Task<Result<bool>> UpdateProductAsync(UpdateProductRequest request, string accessToken);
    Task<Result<bool>> DeleteProductAsync(string productId, string accessToken);
    Task<Result<ProductSetResponse>> CreateProductSetAsync(CreateProductSetRequest request, string accessToken);
    Task<Result<List<ProductSetResponse>>> GetProductSetsAsync(string catalogId, string accessToken);
}
