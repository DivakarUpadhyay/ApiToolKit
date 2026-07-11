using ApiToolKit.Domain.Entities.Shopify;
namespace ApiToolKit.Domain.Interfaces.Repositories.Shopify;
public interface IShopifyStorefrontRepository
{
    Task<ShopifyStorefrontConfig?> GetConfigAsync(string shopDomain);
    Task<int> SaveApiLogAsync(ShopifyApiLog log);
}
