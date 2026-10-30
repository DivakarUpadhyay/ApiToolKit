using ApiToolKit.Application.DTOs.Merchant.Issue;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantIssueService : MerchantServiceBase, IMerchantIssueService
{
    public MerchantIssueService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListIssuesAsync(ListIssuesRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/issues";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "timeZone", r.TimeZone);
        Add(query, "pageToken", r.PageToken);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "languageCode", r.LanguageCode);
        return await SendAsync(r.AccountKey, "ListIssues", HttpMethod.Get, url, query);
    }
}
