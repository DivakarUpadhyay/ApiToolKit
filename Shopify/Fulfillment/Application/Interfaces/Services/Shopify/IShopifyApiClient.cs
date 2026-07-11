namespace ApiToolKit.Application.Interfaces.Services.Shopify;

public interface IShopifyApiClient
{
    Task<string> GetAsync(string shopDomain, string endpoint, Dictionary<string, string>? queryParams = null);
    Task<string> PostAsync(string shopDomain, string endpoint, object? body = null);
    Task<string> PutAsync(string shopDomain, string endpoint, object? body = null);
    Task<string> DeleteAsync(string shopDomain, string endpoint);
}
