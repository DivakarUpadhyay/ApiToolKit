using ApiToolKit.Application.DTOs.Merchant.QuotaQuota;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantQuotaQuotaService : MerchantServiceBase, IMerchantQuotaQuotaService
{
    public MerchantQuotaQuotaService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListQuotasAsync(ListQuotasRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/quota/v1/accounts/{Esc(r.AccountId)}/quotas";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListQuotas", HttpMethod.Get, url, query);
    }
}
