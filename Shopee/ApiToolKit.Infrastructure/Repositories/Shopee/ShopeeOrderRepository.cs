using ApiToolKit.Domain.Interfaces.Repositories.Shopee;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Shopee;

public class ShopeeOrderRepository : IShopeeOrderRepository
{
    private readonly DapperContext _context;

    public ShopeeOrderRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveOrderAsync(long shopId, string orderSn, string orderStatus, string buyerUserId, string buyerUsername, decimal totalAmount, string currency, long createTime, long updateTime, string items, string address, string note)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync("sp_Shopee_SaveOrder",
            new { ShopId = shopId, OrderSn = orderSn, OrderStatus = orderStatus, BuyerUserId = buyerUserId, BuyerUsername = buyerUsername, TotalAmount = totalAmount, Currency = currency, CreateTime = createTime, UpdateTime = updateTime, Items = items, Address = address, Note = note },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<dynamic>> GetOrdersByShopAsync(long shopId, string? status, int pageNo, int pageSize)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryAsync("sp_Shopee_GetOrdersByShop",
            new { ShopId = shopId, Status = status, PageNo = pageNo, PageSize = pageSize },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<dynamic?> GetOrderDetailAsync(long shopId, string orderSn)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync("sp_Shopee_GetOrderDetail",
            new { ShopId = shopId, OrderSn = orderSn },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> UpdateOrderStatusAsync(long shopId, string orderSn, string status)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync("sp_Shopee_UpdateOrderStatus",
            new { ShopId = shopId, OrderSn = orderSn, Status = status },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SetOrderNoteAsync(long shopId, string orderSn, string note)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync("sp_Shopee_SetOrderNote",
            new { ShopId = shopId, OrderSn = orderSn, Note = note },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
