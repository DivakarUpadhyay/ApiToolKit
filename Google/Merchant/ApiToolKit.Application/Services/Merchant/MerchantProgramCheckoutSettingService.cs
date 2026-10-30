using ApiToolKit.Application.DTOs.Merchant.ProgramCheckoutSetting;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantProgramCheckoutSettingService : MerchantServiceBase, IMerchantProgramCheckoutSettingService
{
    public MerchantProgramCheckoutSettingService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> DeleteCheckoutSettingsAsync(DeleteCheckoutSettingsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/programs/{Esc(r.ProgramId)}/checkoutSettings";
        return await SendAsync(r.AccountKey, "DeleteCheckoutSettings", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> CreateCheckoutSettingAsync(CreateCheckoutSettingRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/programs/{Esc(r.ProgramId)}/checkoutSettings";
        return await SendAsync(r.AccountKey, "CreateCheckoutSetting", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> UpdateCheckoutSettingsAsync(UpdateCheckoutSettingsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/programs/{Esc(r.ProgramId)}/checkoutSettings";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateCheckoutSettings", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> GetCheckoutSettingsAsync(GetCheckoutSettingsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/programs/{Esc(r.ProgramId)}/checkoutSettings";
        return await SendAsync(r.AccountKey, "GetCheckoutSettings", HttpMethod.Get, url);
    }
}
