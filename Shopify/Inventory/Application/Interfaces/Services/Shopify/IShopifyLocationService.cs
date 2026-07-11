using ApiToolKit.Application.DTOs.Shopify.Inventory;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Shopify;
public interface IShopifyLocationService
{
    Task<Result<ListLocationsResponse>> ListLocationsAsync(string shopDomain);
    Task<Result<LocationResponse>> GetLocationAsync(string shopDomain, string locationId);
    Task<Result<int>> CountLocationsAsync(string shopDomain);
    Task<Result<ListInventoryLevelsResponse>> GetLocationInventoryLevelsAsync(string shopDomain, string locationId);
}
