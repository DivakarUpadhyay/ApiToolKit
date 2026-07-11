namespace ApiToolKit.Domain.Interfaces.Repositories.Shopee;

public interface IShopeeLogisticsRepository
{
    Task<int> SaveLogisticsInfoAsync(long shopId, string orderSn, long logisticId, string logisticName, bool cod, decimal shippingFee, string status, string trackingNumber, string shipmentMethod, string pickupAddress);
    Task<dynamic?> GetLogisticsInfoAsync(long shopId, string orderSn);
    Task<int> UpdateTrackingNumberAsync(long shopId, string orderSn, string trackingNumber);
}
