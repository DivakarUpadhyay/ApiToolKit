using ApiToolKit.Application.DTOs.Identity.OAuth;

namespace ApiToolKit.Application.Interfaces.Services.Identity;

public interface IIdentityApiClient
{
    string BuildAuthorizationUrl(List<string> scopes, string state);
    Task<OAuthTokenResponse?> ExchangeCodeAsync(string code, string redirectUri);
    Task<OAuthTokenResponse?> RefreshTokenAsync(string refreshToken);
    Task<bool> RevokeTokenAsync(string token);
    Task<T?> GetAsync<T>(string url, string? accessToken = null, Dictionary<string, string>? queryParams = null);
    Task<T?> PostAsync<T>(string url, object body, string? accessToken = null);
    Task<T?> PatchAsync<T>(string url, object body, string accessToken);
    Task<bool> DeleteAsync(string url, string accessToken);
}
