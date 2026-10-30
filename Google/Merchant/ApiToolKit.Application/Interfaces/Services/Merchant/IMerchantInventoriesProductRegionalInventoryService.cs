using ApiToolKit.Application.DTOs.Merchant.InventoriesProductRegionalInventory;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantInventoriesProductRegionalInventoryService
{
    Task<Result<object>> DeleteRegionalInventoryAsync(DeleteRegionalInventoryRequest request);
    Task<Result<object>> ListRegionalInventoriesAsync(ListRegionalInventoriesRequest request);
    Task<Result<object>> InsertRegionalInventoryAsync(InsertRegionalInventoryRequest request);
}
