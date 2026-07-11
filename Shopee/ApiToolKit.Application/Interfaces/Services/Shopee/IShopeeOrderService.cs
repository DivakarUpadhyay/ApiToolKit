using ApiToolKit.Application.DTOs.Shopee.Order;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeeOrderService
{
    Task<Result<OrderListResponse>> GetOrderListAsync(GetOrderListRequest request);
    Task<Result<List<OrderResponse>>> GetOrderDetailAsync(GetOrderDetailRequest request);
    Task<Result<bool>> CancelOrderAsync(CancelOrderRequest request);
    Task<Result<bool>> SetOrderNoteAsync(SetNoteRequest request);
    Task<Result<bool>> HandleBuyerCancellationAsync(HandleBuyerCancellationRequest request);
    Task<Result<object>> GetEscrowDetailAsync(long shopId, string orderSn);
}
