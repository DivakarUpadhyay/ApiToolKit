using ApiToolKit.Application.DTOs.Merchant.OmnichannelSettingLfpProvider;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantOmnichannelSettingLfpProviderService : MerchantServiceBase, IMerchantOmnichannelSettingLfpProviderService
{
    public MerchantOmnichannelSettingLfpProviderService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> FindLfpProviderAsync(FindLfpProviderRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/omnichannelSettings/{Esc(r.OmnichannelSettingId)}/lfpProviders:find";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "FindLfpProvider", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> LinkLfpProviderAsync(LinkLfpProviderRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/omnichannelSettings/{Esc(r.OmnichannelSettingId)}/lfpProviders/{Esc(r.LfpProviderId)}:linkLfpProvider";
        return await SendAsync(r.AccountKey, "LinkLfpProvider", HttpMethod.Post, url, null, r.Body);
    }
}
