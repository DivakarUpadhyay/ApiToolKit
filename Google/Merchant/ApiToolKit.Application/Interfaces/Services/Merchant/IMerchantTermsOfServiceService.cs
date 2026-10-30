using ApiToolKit.Application.DTOs.Merchant.TermsOfService;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantTermsOfServiceService
{
    Task<Result<object>> RetrieveLatestTermsOfServiceAsync(RetrieveLatestTermsOfServiceRequest request);
    Task<Result<object>> AcceptTermsOfServiceAsync(AcceptTermsOfServiceRequest request);
    Task<Result<object>> GetTermsOfServiceAsync(GetTermsOfServiceRequest request);
}
