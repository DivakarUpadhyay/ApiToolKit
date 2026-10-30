using ApiToolKit.Application.DTOs.Merchant.ProgramCheckoutSetting;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantProgramCheckoutSettingService
{
    Task<Result<object>> DeleteCheckoutSettingsAsync(DeleteCheckoutSettingsRequest request);
    Task<Result<object>> CreateCheckoutSettingAsync(CreateCheckoutSettingRequest request);
    Task<Result<object>> UpdateCheckoutSettingsAsync(UpdateCheckoutSettingsRequest request);
    Task<Result<object>> GetCheckoutSettingsAsync(GetCheckoutSettingsRequest request);
}
