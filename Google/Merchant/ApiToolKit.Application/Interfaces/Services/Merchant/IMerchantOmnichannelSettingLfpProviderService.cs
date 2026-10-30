using ApiToolKit.Application.DTOs.Merchant.OmnichannelSettingLfpProvider;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantOmnichannelSettingLfpProviderService
{
    Task<Result<object>> FindLfpProviderAsync(FindLfpProviderRequest request);
    Task<Result<object>> LinkLfpProviderAsync(LinkLfpProviderRequest request);
}
