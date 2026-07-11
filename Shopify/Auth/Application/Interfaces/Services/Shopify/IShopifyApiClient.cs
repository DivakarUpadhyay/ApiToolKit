namespace ApiToolKit.Application.Interfaces.Services.Shopify;

public interface IShopifyApiClient
{
    string BuildInstallUrl(string shopDomain, string apiKey, string scopes, string redirectUri, string state);
    Task<T?> ExchangeCodeAsync<T>(string shopDomain, string apiKey, string apiSecret, string code);
    Task<T?> GetAsync<T>(string shopDomain, string endpoint, Dictionary<string, string>? queryParams = null, string? accessToken = null);
    Task<T?> PostAsync<T>(string shopDomain, string endpoint, object? body = null, string? accessToken = null);
    Task<T?> PutAsync<T>(string shopDomain, string endpoint, object? body = null, string? accessToken = null);
    Task<T?> DeleteAsync<T>(string shopDomain, string endpoint, string? accessToken = null);
}
