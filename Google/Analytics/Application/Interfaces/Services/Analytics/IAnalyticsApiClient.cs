using ApiToolKit.Application.DTOs.Analytics.Auth;

namespace ApiToolKit.Application.Interfaces.Services.Analytics;

public interface IAnalyticsApiClient
{
    string BuildAuthorizationUrl(string state = "");
    Task<AnalyticsTokenResponse> ExchangeCodeAsync(string code);
    Task<AnalyticsTokenResponse> RefreshTokenAsync(string refreshToken);
    Task<T?> GetAsync<T>(string url, string accessToken);
    Task<T?> PostAsync<T>(string url, object body, string accessToken);
}
