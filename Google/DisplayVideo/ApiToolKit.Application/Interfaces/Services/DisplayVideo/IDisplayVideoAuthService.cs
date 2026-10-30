using ApiToolKit.Application.DTOs.DisplayVideo.Auth;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoAuthService
{
    Result<GetAuthUrlResponse> GetAuthorizationUrl(string accountKey);
    Task<Result<DisplayVideoTokenResponse>> ExchangeCodeAsync(string code, string accountKey);
    Task<Result<DisplayVideoTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
    Task<string?> GetValidAccessTokenAsync(string accountKey);
}
