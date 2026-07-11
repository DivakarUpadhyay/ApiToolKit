using ApiToolKit.Application.DTOs.YouTube.Auth;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.YouTube;

public interface IYouTubeAuthService
{
    Result<GetAuthUrlResponse> GetAuthorizationUrl(string[] scopes);
    Task<Result<YouTubeTokenResponse>> ExchangeCodeAsync(ExchangeCodeRequest request, string userId);
    Task<Result<YouTubeTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
}
