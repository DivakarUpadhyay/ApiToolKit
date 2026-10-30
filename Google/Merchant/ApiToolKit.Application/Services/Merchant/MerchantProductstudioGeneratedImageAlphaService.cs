using ApiToolKit.Application.DTOs.Merchant.ProductstudioGeneratedImageAlpha;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantProductstudioGeneratedImageAlphaService : MerchantServiceBase, IMerchantProductstudioGeneratedImageAlphaService
{
    public MerchantProductstudioGeneratedImageAlphaService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> UpscaleProductImageGeneratedImageAsync(UpscaleProductImageGeneratedImageRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/productstudio/v1alpha/accounts/{Esc(r.AccountId)}/generatedImages:upscaleProductImage";
        return await SendAsync(r.AccountKey, "UpscaleProductImageGeneratedImage", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> RemoveProductImageBackgroundGeneratedImageAsync(RemoveProductImageBackgroundGeneratedImageRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/productstudio/v1alpha/accounts/{Esc(r.AccountId)}/generatedImages:removeProductImageBackground";
        return await SendAsync(r.AccountKey, "RemoveProductImageBackgroundGeneratedImage", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> GenerateProductImageBackgroundGeneratedImageAsync(GenerateProductImageBackgroundGeneratedImageRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/productstudio/v1alpha/accounts/{Esc(r.AccountId)}/generatedImages:generateProductImageBackground";
        return await SendAsync(r.AccountKey, "GenerateProductImageBackgroundGeneratedImage", HttpMethod.Post, url, null, r.Body);
    }
}
