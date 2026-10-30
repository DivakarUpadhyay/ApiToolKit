using ApiToolKit.Application.DTOs.Merchant.QuotaLimit;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantQuotaLimitService : MerchantServiceBase, IMerchantQuotaLimitService
{
    public MerchantQuotaLimitService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListLimitsAsync(ListLimitsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/quota/v1/accounts/{Esc(r.AccountId)}/limits";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        Add(query, "filter", r.Filter);
        return await SendAsync(r.AccountKey, "ListLimits", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetLimitAsync(GetLimitRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/quota/v1/accounts/{Esc(r.AccountId)}/limits/{Esc(r.LimitId)}";
        return await SendAsync(r.AccountKey, "GetLimit", HttpMethod.Get, url);
    }
}
