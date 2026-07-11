using ApiToolKit.Application.DTOs.Cloud.Auth;

namespace ApiToolKit.Application.Interfaces.Services.Cloud;

public interface ICloudApiClient
{
    string BuildAuthorizationUrl(string[] scopes);
    Task<CloudTokenResponse?> ExchangeCodeAsync(string code, string redirectUri);
    Task<CloudTokenResponse?> RefreshTokenAsync(string refreshToken);
    Task<T?> GetAsync<T>(string url, string accessToken, Dictionary<string, string>? queryParams = null);
    Task<T?> PostAsync<T>(string url, object body, string accessToken);
}
