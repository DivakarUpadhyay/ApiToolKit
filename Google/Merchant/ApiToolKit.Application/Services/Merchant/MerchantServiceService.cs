using ApiToolKit.Application.DTOs.Merchant.Service;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantServiceService : MerchantServiceBase, IMerchantServiceService
{
    public MerchantServiceService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> RejectServiceAsync(RejectServiceRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/services/{Esc(r.ServiceId)}:reject";
        return await SendAsync(r.AccountKey, "RejectService", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> ApproveServiceAsync(ApproveServiceRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/services/{Esc(r.ServiceId)}:approve";
        return await SendAsync(r.AccountKey, "ApproveService", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> ProposeServiceAsync(ProposeServiceRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/services:propose";
        return await SendAsync(r.AccountKey, "ProposeService", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> GetServiceAsync(GetServiceRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/services/{Esc(r.ServiceId)}";
        return await SendAsync(r.AccountKey, "GetService", HttpMethod.Get, url);
    }

    public async Task<Result<object>> ListServicesAsync(ListServicesRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/services";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListServices", HttpMethod.Get, url, query);
    }
}
