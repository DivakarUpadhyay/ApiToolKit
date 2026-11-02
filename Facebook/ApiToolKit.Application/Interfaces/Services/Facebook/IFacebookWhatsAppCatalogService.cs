using ApiToolKit.Application.DTOs.Facebook.WhatsAppCatalog;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Facebook;
public interface IFacebookWhatsAppCatalogService
{
    Task<Result<WabaCatalogsResponse>> GetCatalogsAsync(string wabaId, string accessToken);
    Task<Result<WabaCatalog>> CreateCatalogAsync(string businessId, CreateWabaCatalogRequest request, string accessToken);
    Task<Result<WabaCatalogsResponse>> GetBusinessCatalogsAsync(string businessId, string accessToken);
    Task<Result<bool>> ConnectCatalogAsync(string wabaId, string catalogId, string accessToken);
    Task<Result<bool>> DisconnectCatalogAsync(string wabaId, string catalogId, string accessToken);
    Task<Result<bool>> UpdateCatalogProductAsync(string productId, UpdateWabaCatalogProductRequest request, string accessToken);
    Task<Result<WabaCatalog>> GetCatalogAsync(string catalogId, string accessToken);
    Task<Result<WabaCatalogProductsResponse>> GetCatalogProductsAsync(string catalogId, string accessToken);
    Task<Result<WabaCatalogProduct>> AddCatalogProductAsync(string catalogId, CreateWabaCatalogProductRequest request, string accessToken);
    Task<Result<WabaCatalogProduct>> GetCatalogProductAsync(string productId, string accessToken);
    Task<Result<bool>> DeleteCatalogProductAsync(string productId, string accessToken);
}
