using ApiToolKit.Application.DTOs.Merchant.IssueresolutionAggregateProductStatuse;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantIssueresolutionAggregateProductStatuseService : MerchantServiceBase, IMerchantIssueresolutionAggregateProductStatuseService
{
    public MerchantIssueresolutionAggregateProductStatuseService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListAggregateProductStatusesAsync(ListAggregateProductStatusesRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/issueresolution/v1/accounts/{Esc(r.AccountId)}/aggregateProductStatuses";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "filter", r.Filter);
        return await SendAsync(r.AccountKey, "ListAggregateProductStatuses", HttpMethod.Get, url, query);
    }
}
