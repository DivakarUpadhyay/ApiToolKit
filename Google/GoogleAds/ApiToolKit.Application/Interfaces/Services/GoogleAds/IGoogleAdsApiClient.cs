using ApiToolKit.Application.DTOs.GoogleAds.Auth;

namespace ApiToolKit.Application.Interfaces.Services.GoogleAds;

public interface IGoogleAdsApiClient
{
    string BuildAuthorizationUrl(string[] scopes);
    Task<GoogleTokenResponse?> ExchangeCodeAsync(string code, string redirectUri);
    Task<GoogleTokenResponse?> RefreshTokenAsync(string refreshToken);
    Task<T?> GetAsync<T>(string url, string accessToken, Dictionary<string, string>? queryParams = null);
    Task<T?> PostAsync<T>(string url, object body, string accessToken, string? customerId = null);
    Task<T?> SearchAsync<T>(string customerId, string query, string accessToken);
    Task<T?> MutateAsync<T>(string customerId, string resource, object operations, string accessToken);
    Task<(int StatusCode, string Body)> SendRawAsync(HttpMethod method, string url, string accessToken, string? jsonBody, string? loginCustomerId);
}
