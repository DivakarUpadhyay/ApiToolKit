using ApiToolKit.Application.DTOs.Shopify.Products;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopify;

public interface IShopifyImageService
{
    Task<Result<ListImagesResponse>> ListImagesAsync(string shopDomain, string productId);
    Task<Result<ImageResponse>> GetImageAsync(string shopDomain, string productId, string imageId);
    Task<Result<int>> CountImagesAsync(string shopDomain, string productId);
    Task<Result<ImageResponse>> CreateImageAsync(string shopDomain, string productId, CreateImageRequest request);
    Task<Result<ImageResponse>> UpdateImageAsync(string shopDomain, string productId, string imageId, UpdateImageRequest request);
    Task<Result<bool>> DeleteImageAsync(string shopDomain, string productId, string imageId);
}
