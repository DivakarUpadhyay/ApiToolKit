using ApiToolKit.Application.DTOs.Merchant.AutofeedSetting;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantAutofeedSettingService
{
    Task<Result<object>> GetAutofeedSettingsAsync(GetAutofeedSettingsRequest request);
    Task<Result<object>> UpdateAutofeedSettingsAsync(UpdateAutofeedSettingsRequest request);
}
