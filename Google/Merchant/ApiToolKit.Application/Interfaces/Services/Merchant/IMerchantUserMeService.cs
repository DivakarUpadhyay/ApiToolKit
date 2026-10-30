using ApiToolKit.Application.DTOs.Merchant.UserMe;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantUserMeService
{
    Task<Result<object>> VerifySelfUserAsync(VerifySelfUserRequest request);
}
