using ApiToolKit.Application.DTOs.Shopify.Fulfillment;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopify;

public interface IShopifyFulfillmentOrderService
{
    Task<Result<ListFulfillmentOrdersResponse>> ListFulfillmentOrdersAsync(string shopDomain, string orderId);
    Task<Result<FulfillmentOrderResponse>> GetFulfillmentOrderAsync(string shopDomain, string fulfillmentOrderId);
    Task<Result<FulfillmentOrderResponse>> CancelFulfillmentOrderAsync(string shopDomain, string fulfillmentOrderId);
    Task<Result<FulfillmentOrderResponse>> MoveFulfillmentOrderAsync(string shopDomain, string fulfillmentOrderId, string newLocationId);
    Task<Result<FulfillmentOrderResponse>> ReleaseHoldsAsync(string shopDomain, string fulfillmentOrderId);
}
