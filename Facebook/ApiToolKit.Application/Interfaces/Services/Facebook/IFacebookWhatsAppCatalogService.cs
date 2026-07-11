using ApiToolKit.Application.DTOs.Facebook.WhatsAppCatalog;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Facebook;
public interface IFacebookWhatsAppCatalogService
{
    Task<Result<WabaCatalogsResponse>> GetCatalogsAsync(string wabaId, string accessToken);
    Task<Result<WabaCatalog>> CreateCatalogAsync(string wabaId, CreateWabaCatalogRequest request, string accessToken);
    Task<Result<WabaCatalog>> GetCatalogAsync(string catalogId, string accessToken);
    Task<Result<WabaCatalogProductsResponse>> GetCatalogProductsAsync(string catalogId, string accessToken);
    Task<Result<WabaCatalogProduct>> AddCatalogProductAsync(string catalogId, CreateWabaCatalogProductRequest request, string accessToken);
    Task<Result<WabaCatalogProduct>> GetCatalogProductAsync(string productId, string accessToken);
    Task<Result<bool>> DeleteCatalogProductAsync(string productId, string accessToken);
}
