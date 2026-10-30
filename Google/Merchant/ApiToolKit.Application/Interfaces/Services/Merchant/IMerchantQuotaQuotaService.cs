using ApiToolKit.Application.DTOs.Merchant.QuotaQuota;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantQuotaQuotaService
{
    Task<Result<object>> ListQuotasAsync(ListQuotasRequest request);
}
