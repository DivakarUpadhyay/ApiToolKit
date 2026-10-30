using ApiToolKit.Application.DTOs.Merchant.Homepage;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantHomepageService : MerchantServiceBase, IMerchantHomepageService
{
    public MerchantHomepageService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> UnclaimHomepageAsync(UnclaimHomepageRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/homepage:unclaim";
        return await SendAsync(r.AccountKey, "UnclaimHomepage", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> ClaimHomepageAsync(ClaimHomepageRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/homepage:claim";
        return await SendAsync(r.AccountKey, "ClaimHomepage", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> GetHomepageAsync(GetHomepageRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/homepage";
        return await SendAsync(r.AccountKey, "GetHomepage", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateHomepageAsync(UpdateHomepageRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/homepage";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateHomepage", HttpMethod.Patch, url, query, r.Body);
    }
}
