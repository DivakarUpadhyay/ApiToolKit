namespace ApiToolKit.Domain.Interfaces.Repositories.Shopee;

public interface IShopeeOrderRepository
{
    Task<int> SaveOrderAsync(long shopId, string orderSn, string orderStatus, string buyerUserId, string buyerUsername, decimal totalAmount, string currency, long createTime, long updateTime, string items, string address, string note);
    Task<IEnumerable<dynamic>> GetOrdersByShopAsync(long shopId, string? status, int pageNo, int pageSize);
    Task<dynamic?> GetOrderDetailAsync(long shopId, string orderSn);
    Task<int> UpdateOrderStatusAsync(long shopId, string orderSn, string status);
    Task<int> SetOrderNoteAsync(long shopId, string orderSn, string note);
}
