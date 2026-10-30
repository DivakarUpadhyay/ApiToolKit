using ApiToolKit.Application.DTOs.Merchant.InventoriesProductLocalInventory;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantInventoriesProductLocalInventoryService
{
    Task<Result<object>> ListLocalInventoriesAsync(ListLocalInventoriesRequest request);
    Task<Result<object>> InsertLocalInventoryAsync(InsertLocalInventoryRequest request);
    Task<Result<object>> DeleteLocalInventoryAsync(DeleteLocalInventoryRequest request);
}
