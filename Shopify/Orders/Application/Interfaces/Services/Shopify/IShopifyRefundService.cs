using ApiToolKit.Application.DTOs.Shopify.Orders;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Shopify;
public interface IShopifyRefundService
{
    Task<Result<ListRefundsResponse>> ListRefundsAsync(string shopDomain, string orderId);
    Task<Result<RefundResponse>> GetRefundAsync(string shopDomain, string orderId, string refundId);
    Task<Result<RefundResponse>> CreateRefundAsync(string shopDomain, string orderId, CreateRefundRequest request);
    Task<Result<RefundResponse>> CalculateRefundAsync(string shopDomain, string orderId, CalculateRefundRequest request);
}
