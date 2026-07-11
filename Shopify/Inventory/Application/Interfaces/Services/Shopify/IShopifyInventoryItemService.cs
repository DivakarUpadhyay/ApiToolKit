using ApiToolKit.Application.DTOs.Shopify.Inventory;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Shopify;
public interface IShopifyInventoryItemService
{
    Task<Result<ListInventoryItemsResponse>> ListInventoryItemsAsync(string shopDomain, string ids, int? limit = null);
    Task<Result<InventoryItemResponse>> GetInventoryItemAsync(string shopDomain, string inventoryItemId);
    Task<Result<InventoryItemResponse>> UpdateInventoryItemAsync(string shopDomain, string inventoryItemId, UpdateInventoryItemRequest request);
}
