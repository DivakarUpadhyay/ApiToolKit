using ApiToolKit.Application.DTOs.Merchant.BusinessIdentity;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantBusinessIdentityService : MerchantServiceBase, IMerchantBusinessIdentityService
{
    public MerchantBusinessIdentityService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetBusinessIdentityAsync(GetBusinessIdentityRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/businessIdentity";
        return await SendAsync(r.AccountKey, "GetBusinessIdentity", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateBusinessIdentityAsync(UpdateBusinessIdentityRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/businessIdentity";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateBusinessIdentity", HttpMethod.Patch, url, query, r.Body);
    }
}
