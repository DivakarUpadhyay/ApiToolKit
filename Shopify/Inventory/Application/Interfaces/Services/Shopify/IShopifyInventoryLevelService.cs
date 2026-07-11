using ApiToolKit.Application.DTOs.Shopify.Inventory;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Shopify;
public interface IShopifyInventoryLevelService
{
    Task<Result<ListInventoryLevelsResponse>> ListInventoryLevelsAsync(string shopDomain, string? inventoryItemIds = null, string? locationIds = null);
    Task<Result<InventoryLevelResponse>> AdjustInventoryLevelAsync(string shopDomain, AdjustInventoryLevelRequest request);
    Task<Result<InventoryLevelResponse>> ConnectInventoryLevelAsync(string shopDomain, ConnectInventoryLevelRequest request);
    Task<Result<InventoryLevelResponse>> SetInventoryLevelAsync(string shopDomain, SetInventoryLevelRequest request);
    Task<Result<bool>> DeleteInventoryLevelAsync(string shopDomain, string inventoryItemId, string locationId);
}
