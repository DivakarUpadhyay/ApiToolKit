using ApiToolKit.Domain.Interfaces.Repositories.Shopee;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Shopee;

public class ShopeeLogisticsRepository : IShopeeLogisticsRepository
{
    private readonly DapperContext _context;

    public ShopeeLogisticsRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveLogisticsInfoAsync(long shopId, string orderSn, long logisticId, string logisticName, bool cod, decimal shippingFee, string status, string trackingNumber, string shipmentMethod, string pickupAddress)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync("sp_Shopee_SaveLogisticsInfo",
            new { ShopId = shopId, OrderSn = orderSn, LogisticId = logisticId, LogisticName = logisticName, Cod = cod, ShippingFee = shippingFee, Status = status, TrackingNumber = trackingNumber, ShipmentMethod = shipmentMethod, PickupAddress = pickupAddress },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<dynamic?> GetLogisticsInfoAsync(long shopId, string orderSn)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync("sp_Shopee_GetLogisticsInfo",
            new { ShopId = shopId, OrderSn = orderSn },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> UpdateTrackingNumberAsync(long shopId, string orderSn, string trackingNumber)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync("sp_Shopee_UpdateTrackingNumber",
            new { ShopId = shopId, OrderSn = orderSn, TrackingNumber = trackingNumber },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
