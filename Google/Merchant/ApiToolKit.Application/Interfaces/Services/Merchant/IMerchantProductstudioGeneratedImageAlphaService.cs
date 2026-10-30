using ApiToolKit.Application.DTOs.Merchant.ProductstudioGeneratedImageAlpha;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantProductstudioGeneratedImageAlphaService
{
    Task<Result<object>> UpscaleProductImageGeneratedImageAsync(UpscaleProductImageGeneratedImageRequest request);
    Task<Result<object>> RemoveProductImageBackgroundGeneratedImageAsync(RemoveProductImageBackgroundGeneratedImageRequest request);
    Task<Result<object>> GenerateProductImageBackgroundGeneratedImageAsync(GenerateProductImageBackgroundGeneratedImageRequest request);
}
