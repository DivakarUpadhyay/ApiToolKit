using ApiToolKit.Domain.Entities.Shopify;

namespace ApiToolKit.Domain.Interfaces.Repositories.Shopify;

public interface IShopifyProductRepository
{
    Task<int> UpsertProductAsync(ShopifyProduct product);
    Task<int> SaveApiLogAsync(ShopifyApiLog log);
}
