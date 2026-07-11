namespace ApiToolKit.Domain.Interfaces.Repositories.Shopee;

public interface IShopeeProductRepository
{
    Task<int> SaveProductAsync(long shopId, long itemId, string itemName, string description, long categoryId, string condition, string status, decimal originalPrice, int stock, string images, string variations, string attributes);
    Task<IEnumerable<dynamic>> GetProductsByShopAsync(long shopId, int pageNo, int pageSize);
    Task<dynamic?> GetProductByItemIdAsync(long shopId, long itemId);
    Task<int> UpdateProductPriceAsync(long shopId, long itemId, long modelId, decimal price);
    Task<int> UpdateProductStockAsync(long shopId, long itemId, long modelId, int stock);
    Task<int> DeleteProductAsync(long shopId, long itemId);
}
