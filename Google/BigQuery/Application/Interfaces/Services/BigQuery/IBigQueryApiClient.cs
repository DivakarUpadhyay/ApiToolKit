using ApiToolKit.Application.DTOs.BigQuery.Auth;

namespace ApiToolKit.Application.Interfaces.Services.BigQuery;

public interface IBigQueryApiClient
{
    string BuildAuthorizationUrl(string state = "");
    Task<BigQueryTokenResponse> ExchangeCodeAsync(string code, string redirectUri);
    Task<BigQueryTokenResponse> RefreshTokenAsync(string refreshToken);
    Task<T?> GetAsync<T>(string url, string accessToken);
    Task<T?> PostAsync<T>(string url, object body, string accessToken);
    Task<T?> PatchAsync<T>(string url, object body, string accessToken);
    Task DeleteAsync(string url, string accessToken);
}
