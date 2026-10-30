using ApiToolKit.Application.DTOs.Merchant.LfpLfpInventory;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantLfpLfpInventoryService
{
    Task<Result<object>> InsertLfpInventoryAsync(InsertLfpInventoryRequest request);
}
