using ApiToolKit.Application.DTOs.Merchant.BusinessInfo;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantBusinessInfoService : MerchantServiceBase, IMerchantBusinessInfoService
{
    public MerchantBusinessInfoService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetBusinessInfoAsync(GetBusinessInfoRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/businessInfo";
        return await SendAsync(r.AccountKey, "GetBusinessInfo", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateBusinessInfoAsync(UpdateBusinessInfoRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/businessInfo";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateBusinessInfo", HttpMethod.Patch, url, query, r.Body);
    }
}
