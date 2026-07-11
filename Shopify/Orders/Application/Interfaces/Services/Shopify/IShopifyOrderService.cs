using ApiToolKit.Application.DTOs.Shopify.Orders;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Shopify;
public interface IShopifyOrderService
{
    Task<Result<ListOrdersResponse>> ListOrdersAsync(string shopDomain, string? status = null, int? limit = null);
    Task<Result<OrderResponse>> GetOrderAsync(string shopDomain, string orderId);
    Task<Result<int>> CountOrdersAsync(string shopDomain, string? status = null);
    Task<Result<OrderResponse>> CreateOrderAsync(string shopDomain, CreateOrderRequest request);
    Task<Result<OrderResponse>> UpdateOrderAsync(string shopDomain, string orderId, UpdateOrderRequest request);
    Task<Result<OrderResponse>> CancelOrderAsync(string shopDomain, string orderId, string? reason = null);
    Task<Result<OrderResponse>> CloseOrderAsync(string shopDomain, string orderId);
    Task<Result<OrderResponse>> ReopenOrderAsync(string shopDomain, string orderId);
    Task<Result<bool>> DeleteOrderAsync(string shopDomain, string orderId);
}
