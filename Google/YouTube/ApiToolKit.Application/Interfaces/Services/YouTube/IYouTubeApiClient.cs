using ApiToolKit.Application.DTOs.YouTube.Auth;

namespace ApiToolKit.Application.Interfaces.Services.YouTube;

public interface IYouTubeApiClient
{
    string BuildAuthorizationUrl(string[] scopes);
    Task<YouTubeTokenResponse?> ExchangeCodeAsync(string code, string redirectUri);
    Task<YouTubeTokenResponse?> RefreshTokenAsync(string refreshToken);
    Task<T?> GetAsync<T>(string url, string? accessToken, Dictionary<string, string>? queryParams = null);
    Task<T?> PostAsync<T>(string url, object body, string accessToken);
    Task<T?> PutAsync<T>(string url, object body, string accessToken);
    Task<bool> DeleteAsync(string url, string accessToken);
    Task<T?> UploadVideoAsync<T>(object metadata, Stream videoStream, string mimeType, string accessToken);
}
