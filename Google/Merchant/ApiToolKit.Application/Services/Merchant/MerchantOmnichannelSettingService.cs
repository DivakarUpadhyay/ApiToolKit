using ApiToolKit.Application.DTOs.Merchant.OmnichannelSetting;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantOmnichannelSettingService : MerchantServiceBase, IMerchantOmnichannelSettingService
{
    public MerchantOmnichannelSettingService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListOmnichannelSettingsAsync(ListOmnichannelSettingsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/omnichannelSettings";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListOmnichannelSettings", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> RequestInventoryVerificationOmnichannelSettingAsync(RequestInventoryVerificationOmnichannelSettingRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/omnichannelSettings/{Esc(r.OmnichannelSettingId)}:requestInventoryVerification";
        return await SendAsync(r.AccountKey, "RequestInventoryVerificationOmnichannelSetting", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> CreateOmnichannelSettingAsync(CreateOmnichannelSettingRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/omnichannelSettings";
        return await SendAsync(r.AccountKey, "CreateOmnichannelSetting", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> GetOmnichannelSettingAsync(GetOmnichannelSettingRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/omnichannelSettings/{Esc(r.OmnichannelSettingId)}";
        return await SendAsync(r.AccountKey, "GetOmnichannelSetting", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateOmnichannelSettingAsync(UpdateOmnichannelSettingRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/omnichannelSettings/{Esc(r.OmnichannelSettingId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateOmnichannelSetting", HttpMethod.Patch, url, query, r.Body);
    }
}
