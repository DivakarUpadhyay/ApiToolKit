namespace ApiToolKit.Domain.Interfaces.Repositories.Shopee;

public interface IShopeeVoucherRepository
{
    Task<int> SaveVoucherAsync(long shopId, long voucherId, string voucherCode, string voucherType, string discountType, decimal discountAmount, decimal minimumSpend, long startTime, long endTime, int usageQuantity, string status);
    Task<IEnumerable<dynamic>> GetVouchersAsync(long shopId, int pageNo, int pageSize);
    Task<dynamic?> GetVoucherByIdAsync(long shopId, long voucherId);
    Task<int> UpdateVoucherAsync(long shopId, long voucherId, string status);
    Task<int> DeleteVoucherAsync(long shopId, long voucherId);
}
