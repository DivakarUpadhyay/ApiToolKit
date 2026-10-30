using ApiToolKit.Application.DTOs.Merchant.ShippingSetting;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantShippingSettingService : MerchantServiceBase, IMerchantShippingSettingService
{
    public MerchantShippingSettingService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> InsertShippingSettingAsync(InsertShippingSettingRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/shippingSettings:insert";
        return await SendAsync(r.AccountKey, "InsertShippingSetting", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> GetShippingSettingsAsync(GetShippingSettingsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/shippingSettings";
        return await SendAsync(r.AccountKey, "GetShippingSettings", HttpMethod.Get, url);
    }
}
