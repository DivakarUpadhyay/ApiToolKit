using ApiToolKit.Application.DTOs.Merchant.DeveloperRegistration;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantDeveloperRegistrationService
{
    Task<Result<object>> GetDeveloperRegistrationAsync(GetDeveloperRegistrationRequest request);
    Task<Result<object>> GetAccountForGcpRegistrationDeveloperRegistrationAsync(GetAccountForGcpRegistrationDeveloperRegistrationRequest request);
    Task<Result<object>> UnregisterGcpDeveloperRegistrationAsync(UnregisterGcpDeveloperRegistrationRequest request);
    Task<Result<object>> RegisterGcpDeveloperRegistrationAsync(RegisterGcpDeveloperRegistrationRequest request);
}
