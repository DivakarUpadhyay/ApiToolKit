using ApiToolKit.Application.DTOs.Merchant.ShippingSetting;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantShippingSettingService
{
    Task<Result<object>> InsertShippingSettingAsync(InsertShippingSettingRequest request);
    Task<Result<object>> GetShippingSettingsAsync(GetShippingSettingsRequest request);
}
