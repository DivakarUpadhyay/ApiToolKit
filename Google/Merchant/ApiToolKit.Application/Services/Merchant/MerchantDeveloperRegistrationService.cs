using ApiToolKit.Application.DTOs.Merchant.DeveloperRegistration;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantDeveloperRegistrationService : MerchantServiceBase, IMerchantDeveloperRegistrationService
{
    public MerchantDeveloperRegistrationService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetDeveloperRegistrationAsync(GetDeveloperRegistrationRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/developerRegistration";
        return await SendAsync(r.AccountKey, "GetDeveloperRegistration", HttpMethod.Get, url);
    }

    public async Task<Result<object>> GetAccountForGcpRegistrationDeveloperRegistrationAsync(GetAccountForGcpRegistrationDeveloperRegistrationRequest r)
    {
        var url = "https://merchantapi.googleapis.com/accounts/v1/accounts:getAccountForGcpRegistration";
        return await SendAsync(r.AccountKey, "GetAccountForGcpRegistrationDeveloperRegistration", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UnregisterGcpDeveloperRegistrationAsync(UnregisterGcpDeveloperRegistrationRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/developerRegistration:unregisterGcp";
        return await SendAsync(r.AccountKey, "UnregisterGcpDeveloperRegistration", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> RegisterGcpDeveloperRegistrationAsync(RegisterGcpDeveloperRegistrationRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/developerRegistration:registerGcp";
        return await SendAsync(r.AccountKey, "RegisterGcpDeveloperRegistration", HttpMethod.Post, url, null, r.Body);
    }
}
