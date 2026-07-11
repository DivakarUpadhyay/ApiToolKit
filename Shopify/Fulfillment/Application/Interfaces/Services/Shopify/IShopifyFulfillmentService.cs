using ApiToolKit.Application.DTOs.Shopify.Fulfillment;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopify;

public interface IShopifyFulfillmentService
{
    Task<Result<ListFulfillmentsResponse>> ListFulfillmentsByOrderAsync(string shopDomain, string orderId);
    Task<Result<FulfillmentResponse>> GetFulfillmentAsync(string shopDomain, string orderId, string fulfillmentId);
    Task<Result<int>> CountFulfillmentsAsync(string shopDomain, string orderId);
    Task<Result<FulfillmentResponse>> CreateFulfillmentAsync(string shopDomain, CreateFulfillmentRequest request);
    Task<Result<FulfillmentResponse>> CancelFulfillmentAsync(string shopDomain, string fulfillmentId);
    Task<Result<FulfillmentResponse>> UpdateTrackingAsync(string shopDomain, string fulfillmentId, UpdateTrackingRequest request);
}
