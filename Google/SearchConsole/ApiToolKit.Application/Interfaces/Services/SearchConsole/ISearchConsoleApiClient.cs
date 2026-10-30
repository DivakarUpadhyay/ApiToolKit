using ApiToolKit.Application.DTOs.SearchConsole.Auth;
using ApiToolKit.Application.DTOs.SearchConsole.Common;

namespace ApiToolKit.Application.Interfaces.Services.SearchConsole;

public interface ISearchConsoleApiClient
{
    string BuildAuthorizationUrl(string state);
    Task<SearchConsoleTokenResponse> ExchangeCodeAsync(string code);
    Task<SearchConsoleTokenResponse> RefreshTokenAsync(string refreshToken);
    Task<SearchConsoleApiResponse> SendAsync(HttpMethod method, string url, string accessToken,
        IEnumerable<KeyValuePair<string, string?>>? query = null, object? body = null, bool raw = false);
}
