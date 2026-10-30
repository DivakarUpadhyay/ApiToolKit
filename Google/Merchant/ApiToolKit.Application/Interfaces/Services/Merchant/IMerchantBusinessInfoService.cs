using ApiToolKit.Application.DTOs.Merchant.BusinessInfo;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantBusinessInfoService
{
    Task<Result<object>> GetBusinessInfoAsync(GetBusinessInfoRequest request);
    Task<Result<object>> UpdateBusinessInfoAsync(UpdateBusinessInfoRequest request);
}
