using ApiToolKit.Application.DTOs.Shopify.Auth;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopify;

public interface IShopifyAuthService
{
    Task<Result<GetInstallUrlResponse>> GetInstallUrlAsync(GetInstallUrlRequest request);
    Task<Result<CallbackResponse>> HandleCallbackAsync(CallbackRequest request);
    Task<Result<ShopInfoResponse>> GetShopInfoAsync(string shopDomain);
    Task<Result<bool>> VerifyHmacAsync(VerifyHmacRequest request);
    Task<Result<ShopifyTokenResponse>> GetStoredTokenAsync(string shopDomain);
    Task<Result<bool>> RevokeTokenAsync(string shopDomain);
}
