using ApiToolKit.Application.DTOs.Merchant.LfpLfpMerchantState;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantLfpLfpMerchantStateService : MerchantServiceBase, IMerchantLfpLfpMerchantStateService
{
    public MerchantLfpLfpMerchantStateService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetLfpMerchantStateAsync(GetLfpMerchantStateRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/lfp/v1/accounts/{Esc(r.AccountId)}/lfpMerchantStates/{Esc(r.LfpMerchantStateId)}";
        return await SendAsync(r.AccountKey, "GetLfpMerchantState", HttpMethod.Get, url);
    }
}
