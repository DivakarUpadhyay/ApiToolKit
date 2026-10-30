using ApiToolKit.Application.DTOs.Merchant.EmailPreference;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantEmailPreferenceService : MerchantServiceBase, IMerchantEmailPreferenceService
{
    public MerchantEmailPreferenceService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetEmailPreferencesAsync(GetEmailPreferencesRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/users/{Esc(r.UserId)}/emailPreferences";
        return await SendAsync(r.AccountKey, "GetEmailPreferences", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateEmailPreferencesAsync(UpdateEmailPreferencesRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/users/{Esc(r.UserId)}/emailPreferences";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateEmailPreferences", HttpMethod.Patch, url, query, r.Body);
    }
}
