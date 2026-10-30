using ApiToolKit.Application.DTOs.Merchant.LfpLfpMerchantState;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantLfpLfpMerchantStateService
{
    Task<Result<object>> GetLfpMerchantStateAsync(GetLfpMerchantStateRequest request);
}
