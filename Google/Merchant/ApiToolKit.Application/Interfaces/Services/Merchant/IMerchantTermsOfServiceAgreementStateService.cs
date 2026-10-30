using ApiToolKit.Application.DTOs.Merchant.TermsOfServiceAgreementState;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantTermsOfServiceAgreementStateService
{
    Task<Result<object>> RetrieveForApplicationTermsOfServiceAgreementStateAsync(RetrieveForApplicationTermsOfServiceAgreementStateRequest request);
    Task<Result<object>> GetTermsOfServiceAgreementStateAsync(GetTermsOfServiceAgreementStateRequest request);
}
