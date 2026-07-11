using ApiToolKit.Domain.Interfaces.Repositories.Shopee;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Shopee;

public class ShopeeVoucherRepository : IShopeeVoucherRepository
{
    private readonly DapperContext _context;

    public ShopeeVoucherRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveVoucherAsync(long shopId, long voucherId, string voucherCode, string voucherType, string discountType, decimal discountAmount, decimal minimumSpend, long startTime, long endTime, int usageQuantity, string status)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync("sp_Shopee_SaveVoucher",
            new { ShopId = shopId, VoucherId = voucherId, VoucherCode = voucherCode, VoucherType = voucherType, DiscountType = discountType, DiscountAmount = discountAmount, MinimumSpend = minimumSpend, StartTime = startTime, EndTime = endTime, UsageQuantity = usageQuantity, Status = status },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<dynamic>> GetVouchersAsync(long shopId, int pageNo, int pageSize)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryAsync("sp_Shopee_GetVouchers",
            new { ShopId = shopId, PageNo = pageNo, PageSize = pageSize },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<dynamic?> GetVoucherByIdAsync(long shopId, long voucherId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync("sp_Shopee_GetVoucherById",
            new { ShopId = shopId, VoucherId = voucherId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> UpdateVoucherAsync(long shopId, long voucherId, string status)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync("sp_Shopee_UpdateVoucher",
            new { ShopId = shopId, VoucherId = voucherId, Status = status },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> DeleteVoucherAsync(long shopId, long voucherId)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync("sp_Shopee_DeleteVoucher",
            new { ShopId = shopId, VoucherId = voucherId },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
