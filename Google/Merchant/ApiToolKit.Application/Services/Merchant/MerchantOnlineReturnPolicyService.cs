using ApiToolKit.Application.DTOs.Merchant.OnlineReturnPolicy;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantOnlineReturnPolicyService : MerchantServiceBase, IMerchantOnlineReturnPolicyService
{
    public MerchantOnlineReturnPolicyService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListOnlineReturnPoliciesAsync(ListOnlineReturnPoliciesRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/onlineReturnPolicies";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        Add(query, "pageSize", r.PageSize?.ToString());
        return await SendAsync(r.AccountKey, "ListOnlineReturnPolicies", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> CreateOnlineReturnPolicyAsync(CreateOnlineReturnPolicyRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/onlineReturnPolicies";
        return await SendAsync(r.AccountKey, "CreateOnlineReturnPolicy", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> GetOnlineReturnPolicyAsync(GetOnlineReturnPolicyRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/onlineReturnPolicies/{Esc(r.OnlineReturnPolicyId)}";
        return await SendAsync(r.AccountKey, "GetOnlineReturnPolicy", HttpMethod.Get, url);
    }

    public async Task<Result<object>> DeleteOnlineReturnPolicyAsync(DeleteOnlineReturnPolicyRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/onlineReturnPolicies/{Esc(r.OnlineReturnPolicyId)}";
        return await SendAsync(r.AccountKey, "DeleteOnlineReturnPolicy", HttpMethod.Delete, url);
    }
}
