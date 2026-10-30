using ApiToolKit.Application.DTOs.TagManager.Auth;
using ApiToolKit.Application.DTOs.TagManager.Common;

namespace ApiToolKit.Application.Interfaces.Services.TagManager;

public interface ITagManagerApiClient
{
    string BuildAuthorizationUrl(string state);
    Task<TagManagerTokenResponse> ExchangeCodeAsync(string code);
    Task<TagManagerTokenResponse> RefreshTokenAsync(string refreshToken);
    Task<TagManagerApiResponse> SendAsync(HttpMethod method, string url, string accessToken,
        IEnumerable<KeyValuePair<string, string?>>? query = null, object? body = null, bool raw = false);
}
