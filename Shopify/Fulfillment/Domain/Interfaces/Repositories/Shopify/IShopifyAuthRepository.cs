using ApiToolKit.Domain.Entities.Shopify;

namespace ApiToolKit.Domain.Interfaces.Repositories.Shopify;

public interface IShopifyAuthRepository
{
    Task<ShopifyConfig?> GetConfigAsync();
    Task<ShopifyToken?> GetTokenByShopAsync(string shopDomain);
    Task SaveApiLogAsync(ShopifyApiLog log);
}
