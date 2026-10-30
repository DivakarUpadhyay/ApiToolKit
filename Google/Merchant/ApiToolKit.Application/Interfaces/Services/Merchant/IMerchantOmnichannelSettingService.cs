using ApiToolKit.Application.DTOs.Merchant.OmnichannelSetting;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantOmnichannelSettingService
{
    Task<Result<object>> ListOmnichannelSettingsAsync(ListOmnichannelSettingsRequest request);
    Task<Result<object>> RequestInventoryVerificationOmnichannelSettingAsync(RequestInventoryVerificationOmnichannelSettingRequest request);
    Task<Result<object>> CreateOmnichannelSettingAsync(CreateOmnichannelSettingRequest request);
    Task<Result<object>> GetOmnichannelSettingAsync(GetOmnichannelSettingRequest request);
    Task<Result<object>> UpdateOmnichannelSettingAsync(UpdateOmnichannelSettingRequest request);
}
