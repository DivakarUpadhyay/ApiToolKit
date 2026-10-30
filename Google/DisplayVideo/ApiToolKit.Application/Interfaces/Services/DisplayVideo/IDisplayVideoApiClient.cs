using ApiToolKit.Application.DTOs.DisplayVideo.Auth;
using ApiToolKit.Application.DTOs.DisplayVideo.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoApiClient
{
    string BuildAuthorizationUrl(string state);
    Task<DisplayVideoTokenResponse> ExchangeCodeAsync(string code);
    Task<DisplayVideoTokenResponse> RefreshTokenAsync(string refreshToken);
    Task<DisplayVideoApiResponse> SendAsync(HttpMethod method, string url, string accessToken,
        IEnumerable<KeyValuePair<string, string?>>? query = null, object? body = null, bool raw = false);
}
