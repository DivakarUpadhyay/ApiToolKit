using ApiToolKit.Application.DTOs.Merchant.LfpLfpInventory;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantLfpLfpInventoryService : MerchantServiceBase, IMerchantLfpLfpInventoryService
{
    public MerchantLfpLfpInventoryService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> InsertLfpInventoryAsync(InsertLfpInventoryRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/lfp/v1/accounts/{Esc(r.AccountId)}/lfpInventories:insert";
        return await SendAsync(r.AccountKey, "InsertLfpInventory", HttpMethod.Post, url, null, r.Body);
    }
}
