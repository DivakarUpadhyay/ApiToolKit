using ApiToolKit.Domain.Interfaces.Repositories.Shopee;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Shopee;

public class ShopeeShopRepository : IShopeeShopRepository
{
    private readonly DapperContext _context;

    public ShopeeShopRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveShopInfoAsync(long shopId, string shopName, string region, string status, string shopDescription, string avatar)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Shopee_SaveShopInfo",
            new { ShopId = shopId, ShopName = shopName, Region = region, Status = status, ShopDescription = shopDescription, Avatar = avatar },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<dynamic?> GetShopInfoAsync(long shopId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync(
            "sp_Shopee_GetShopInfo",
            new { ShopId = shopId },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
