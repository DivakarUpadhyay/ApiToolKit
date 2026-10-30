using ApiToolKit.Application.DTOs.Merchant.QuotaLimit;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantQuotaLimitService
{
    Task<Result<object>> ListLimitsAsync(ListLimitsRequest request);
    Task<Result<object>> GetLimitAsync(GetLimitRequest request);
}
