using ApiToolKit.Application.DTOs.Merchant.Auth;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantAuthService
{
    Result<GetAuthUrlResponse> GetAuthorizationUrl(string accountKey);
    Task<Result<MerchantTokenResponse>> ExchangeCodeAsync(string code, string accountKey);
    Task<Result<MerchantTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
    Task<string?> GetValidAccessTokenAsync(string accountKey);
}
