using ApiToolKit.Application.DTOs.Merchant.Homepage;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantHomepageService
{
    Task<Result<object>> UnclaimHomepageAsync(UnclaimHomepageRequest request);
    Task<Result<object>> ClaimHomepageAsync(ClaimHomepageRequest request);
    Task<Result<object>> GetHomepageAsync(GetHomepageRequest request);
    Task<Result<object>> UpdateHomepageAsync(UpdateHomepageRequest request);
}
