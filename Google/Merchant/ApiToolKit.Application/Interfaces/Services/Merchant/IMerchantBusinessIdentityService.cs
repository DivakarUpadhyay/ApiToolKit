using ApiToolKit.Application.DTOs.Merchant.BusinessIdentity;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantBusinessIdentityService
{
    Task<Result<object>> GetBusinessIdentityAsync(GetBusinessIdentityRequest request);
    Task<Result<object>> UpdateBusinessIdentityAsync(UpdateBusinessIdentityRequest request);
}
