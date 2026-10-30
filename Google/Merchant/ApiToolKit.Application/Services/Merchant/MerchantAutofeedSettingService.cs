using ApiToolKit.Application.DTOs.Merchant.AutofeedSetting;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantAutofeedSettingService : MerchantServiceBase, IMerchantAutofeedSettingService
{
    public MerchantAutofeedSettingService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetAutofeedSettingsAsync(GetAutofeedSettingsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/autofeedSettings";
        return await SendAsync(r.AccountKey, "GetAutofeedSettings", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateAutofeedSettingsAsync(UpdateAutofeedSettingsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/autofeedSettings";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateAutofeedSettings", HttpMethod.Patch, url, query, r.Body);
    }
}
