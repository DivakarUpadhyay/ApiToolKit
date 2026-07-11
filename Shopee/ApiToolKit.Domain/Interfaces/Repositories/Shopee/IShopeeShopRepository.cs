namespace ApiToolKit.Domain.Interfaces.Repositories.Shopee;

public interface IShopeeShopRepository
{
    Task<int> SaveShopInfoAsync(long shopId, string shopName, string region, string status, string shopDescription, string avatar);
    Task<dynamic?> GetShopInfoAsync(long shopId);
}
