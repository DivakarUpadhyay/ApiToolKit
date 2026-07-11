using ApiToolKit.Domain.Entities.Shopee;

namespace ApiToolKit.Domain.Interfaces.Repositories.Shopee;

public interface IShopeeAuthRepository
{
    Task<ShopeeConfig?> GetConfigAsync();
    Task<ShopeeToken?> GetTokenByShopIdAsync(long shopId);
    Task<int> SaveTokenAsync(ShopeeToken token);
    Task<int> UpdateTokenAsync(ShopeeToken token);
    Task<int> SaveApiLogAsync(ShopeeApiLog log);
}
