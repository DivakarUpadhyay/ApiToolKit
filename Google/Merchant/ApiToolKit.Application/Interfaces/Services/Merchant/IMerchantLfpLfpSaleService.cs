using ApiToolKit.Application.DTOs.Merchant.LfpLfpSale;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantLfpLfpSaleService
{
    Task<Result<object>> InsertLfpSaleAsync(InsertLfpSaleRequest request);
}
