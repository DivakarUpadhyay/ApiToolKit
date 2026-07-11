using ApiToolKit.Application.DTOs.Shopify.Fulfillment;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopify;

public interface IShopifyFulfillmentServiceService
{
    Task<Result<ListFulfillmentServicesResponse>> ListFulfillmentServicesAsync(string shopDomain, string? scope = null);
    Task<Result<FulfillmentServiceResponse>> GetFulfillmentServiceAsync(string shopDomain, string fulfillmentServiceId);
    Task<Result<FulfillmentServiceResponse>> CreateFulfillmentServiceAsync(string shopDomain, CreateFulfillmentServiceRequest request);
    Task<Result<FulfillmentServiceResponse>> UpdateFulfillmentServiceAsync(string shopDomain, string fulfillmentServiceId, UpdateFulfillmentServiceRequest request);
    Task<Result<bool>> DeleteFulfillmentServiceAsync(string shopDomain, string fulfillmentServiceId);
}
