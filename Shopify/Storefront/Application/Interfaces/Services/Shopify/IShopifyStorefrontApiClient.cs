namespace ApiToolKit.Application.Interfaces.Services.Shopify;
public interface IShopifyStorefrontApiClient
{
    Task<T?> QueryAsync<T>(string shopDomain, string query, object? variables = null, string? storefrontAccessToken = null);
    Task<T?> MutateAsync<T>(string shopDomain, string mutation, object? variables = null, string? storefrontAccessToken = null);
}
