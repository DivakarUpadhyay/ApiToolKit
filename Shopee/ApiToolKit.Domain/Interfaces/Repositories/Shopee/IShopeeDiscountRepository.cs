namespace ApiToolKit.Domain.Interfaces.Repositories.Shopee;

public interface IShopeeDiscountRepository
{
    Task<int> SaveDiscountAsync(long shopId, long discountId, string discountName, long startTime, long endTime, string status);
    Task<IEnumerable<dynamic>> GetDiscountsAsync(long shopId, int pageNo, int pageSize);
    Task<dynamic?> GetDiscountByIdAsync(long shopId, long discountId);
    Task<int> UpdateDiscountAsync(long shopId, long discountId, string discountName, long startTime, long endTime);
    Task<int> DeleteDiscountAsync(long shopId, long discountId);
}
