using ApiToolKit.Application.DTOs.Firebase.Auth;

namespace ApiToolKit.Application.Interfaces.Services.Firebase;

public interface IFirebaseApiClient
{
    string BuildAuthorizationUrl();
    Task<FirebaseTokenResponse?> ExchangeCodeAsync(string code, string redirectUri);
    Task<FirebaseTokenResponse?> RefreshTokenAsync(string refreshToken);
    Task<T?> GetAsync<T>(string url, string? accessToken = null, Dictionary<string, string>? queryParams = null);
    Task<T?> PostAsync<T>(string url, object body, string? accessToken = null);
    Task<T?> PatchAsync<T>(string url, object body, string accessToken, Dictionary<string, string>? headers = null);
    Task<bool> DeleteAsync(string url, string accessToken);
}
