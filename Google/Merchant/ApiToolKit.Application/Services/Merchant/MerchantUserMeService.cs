using ApiToolKit.Application.DTOs.Merchant.UserMe;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantUserMeService : MerchantServiceBase, IMerchantUserMeService
{
    public MerchantUserMeService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> VerifySelfUserAsync(VerifySelfUserRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/users/me:verifySelf";
        return await SendAsync(r.AccountKey, "VerifySelfUser", HttpMethod.Patch, url, null, r.Body);
    }
}
