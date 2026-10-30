using ApiToolKit.Application.DTOs.Merchant.TermsOfServiceAgreementState;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantTermsOfServiceAgreementStateService : MerchantServiceBase, IMerchantTermsOfServiceAgreementStateService
{
    public MerchantTermsOfServiceAgreementStateService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> RetrieveForApplicationTermsOfServiceAgreementStateAsync(RetrieveForApplicationTermsOfServiceAgreementStateRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/termsOfServiceAgreementStates:retrieveForApplication";
        return await SendAsync(r.AccountKey, "RetrieveForApplicationTermsOfServiceAgreementState", HttpMethod.Get, url);
    }

    public async Task<Result<object>> GetTermsOfServiceAgreementStateAsync(GetTermsOfServiceAgreementStateRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/accounts/v1/accounts/{Esc(r.AccountId)}/termsOfServiceAgreementStates/{Esc(r.TermsOfServiceAgreementStateId)}";
        return await SendAsync(r.AccountKey, "GetTermsOfServiceAgreementState", HttpMethod.Get, url);
    }
}
