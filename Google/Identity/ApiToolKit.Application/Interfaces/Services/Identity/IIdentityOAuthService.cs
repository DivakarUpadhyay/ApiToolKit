using ApiToolKit.Application.DTOs.Identity.OAuth;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Identity;

public interface IIdentityOAuthService
{
    Task<Result<GetAuthUrlResponse>> GetAuthUrlAsync(AuthorizationRequest request);
    Task<Result<OAuthTokenResponse>> ExchangeCodeAsync(string code, string redirectUri);
    Task<Result<OAuthTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
    Task<Result<bool>> RevokeTokenAsync(RevokeTokenRequest request);
    Task<Result<TokenInfoResponse>> GetTokenInfoAsync(string accessToken);
    Task<Result<UserInfoResponse>> GetUserInfoAsync(string accessToken);
}
