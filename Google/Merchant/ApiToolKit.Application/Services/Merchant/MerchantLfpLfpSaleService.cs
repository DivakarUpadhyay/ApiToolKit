using ApiToolKit.Application.DTOs.Merchant.LfpLfpSale;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantLfpLfpSaleService : MerchantServiceBase, IMerchantLfpLfpSaleService
{
    public MerchantLfpLfpSaleService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> InsertLfpSaleAsync(InsertLfpSaleRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/lfp/v1/accounts/{Esc(r.AccountId)}/lfpSales:insert";
        return await SendAsync(r.AccountKey, "InsertLfpSale", HttpMethod.Post, url, null, r.Body);
    }
}
