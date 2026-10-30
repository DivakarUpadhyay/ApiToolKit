using ApiToolKit.Application.DTOs.BusinessProfile.Verification;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.BusinessProfile;

public interface IBusinessProfileVerificationService
{
    Task<Result<object>> FetchVerificationOptionsAsync(FetchVerificationOptionsRequest request);
    Task<Result<object>> VerifyLocationAsync(VerifyLocationRequest request);
    Task<Result<object>> ListVerificationsAsync(ListVerificationsRequest request);
    Task<Result<object>> CompleteVerificationAsync(CompleteVerificationRequest request);
    Task<Result<object>> GetVoiceOfMerchantStateAsync(GetVoiceOfMerchantStateRequest request);
}
