using ApiToolKit.Application.DTOs.Shopify.Fulfillment;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopify;

public interface IShopifyShippingZoneService
{
    Task<Result<ListShippingZonesResponse>> ListShippingZonesAsync(string shopDomain);
}
