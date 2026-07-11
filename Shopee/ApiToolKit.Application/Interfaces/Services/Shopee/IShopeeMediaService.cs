using ApiToolKit.Application.DTOs.Shopee.Media;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeeMediaService
{
    Task<Result<UploadImageResponse>> UploadImageAsync(UploadImageRequest request);
    Task<Result<UploadImageResponse>> UploadImageByUrlAsync(UploadImageByUrlRequest request);
    Task<Result<UploadVideoResponse>> UploadVideoAsync(UploadVideoRequest request);
}
