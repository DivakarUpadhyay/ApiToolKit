using ApiToolKit.Domain.Interfaces.Repositories.Shopee;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Shopee;

public class ShopeeDiscountRepository : IShopeeDiscountRepository
{
    private readonly DapperContext _context;

    public ShopeeDiscountRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveDiscountAsync(long shopId, long discountId, string discountName, long startTime, long endTime, string status)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync("sp_Shopee_SaveDiscount",
            new { ShopId = shopId, DiscountId = discountId, DiscountName = discountName, StartTime = startTime, EndTime = endTime, Status = status },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<dynamic>> GetDiscountsAsync(long shopId, int pageNo, int pageSize)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryAsync("sp_Shopee_GetDiscounts",
            new { ShopId = shopId, PageNo = pageNo, PageSize = pageSize },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<dynamic?> GetDiscountByIdAsync(long shopId, long discountId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync("sp_Shopee_GetDiscountById",
            new { ShopId = shopId, DiscountId = discountId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> UpdateDiscountAsync(long shopId, long discountId, string discountName, long startTime, long endTime)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync("sp_Shopee_UpdateDiscount",
            new { ShopId = shopId, DiscountId = discountId, DiscountName = discountName, StartTime = startTime, EndTime = endTime },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> DeleteDiscountAsync(long shopId, long discountId)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync("sp_Shopee_DeleteDiscount",
            new { ShopId = shopId, DiscountId = discountId },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
