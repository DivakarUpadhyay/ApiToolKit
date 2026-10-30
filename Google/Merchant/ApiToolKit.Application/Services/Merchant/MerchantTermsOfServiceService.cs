using ApiToolKit.Application.DTOs.Merchant.TermsOfService;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantTermsOfServiceService : MerchantServiceBase, IMerchantTermsOfServiceService
{
    public MerchantTermsOfServiceService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> RetrieveLatestTermsOfServiceAsync(RetrieveLatestTermsOfServiceRequest r)
    {
        var url = "https://merchantapi.googleapis.com/accounts/v1/termsOfService:retrieveLatest";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "regionCode", r.RegionCode);
        Add(query, "kind", r.Kind);
        return await SendAsync(r.AccountKey, "RetrieveLatestTermsOfService", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> AcceptTermsOfServiceAsync(AcceptTermsOfServiceRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/termsOfService/{Esc(r.TermsOfServiceId)}:accept";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "account", r.Account);
        Add(query, "regionCode", r.RegionCode);
        return await SendAsync(r.AccountKey, "AcceptTermsOfService", HttpMethod.Post, url, query);
    }

    public async Task<Result<object>> GetTermsOfServiceAsync(GetTermsOfServiceRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/termsOfService/{Esc(r.TermsOfServiceId)}";
        return await SendAsync(r.AccountKey, "GetTermsOfService", HttpMethod.Get, url);
    }
}
