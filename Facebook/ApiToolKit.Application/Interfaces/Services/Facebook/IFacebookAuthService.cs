using ApiToolKit.Application.DTOs.Facebook.Auth;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookAuthService
{
    Task<Result<GetAuthUrlResponse>> GetAuthUrlAsync(GetAuthUrlRequest request);
    Task<Result<ExchangeCodeResponse>> ExchangeCodeAsync(ExchangeCodeRequest request);
    Task<Result<GetAppTokenResponse>> GetAppAccessTokenAsync();
    Task<Result<LongLivedTokenResponse>> GetLongLivedTokenAsync(LongLivedTokenRequest request);
    Task<Result<DebugTokenResponse>> DebugTokenAsync(DebugTokenRequest request);
    Task<Result<LongLivedTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
}
