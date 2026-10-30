using ApiToolKit.Application.DTOs.TagManager.Auth;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.TagManager;

public interface ITagManagerAuthService
{
    Result<GetAuthUrlResponse> GetAuthorizationUrl(string accountKey);
    Task<Result<TagManagerTokenResponse>> ExchangeCodeAsync(string code, string accountKey);
    Task<Result<TagManagerTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
    Task<string?> GetValidAccessTokenAsync(string accountKey);
}
