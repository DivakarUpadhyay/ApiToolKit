using ApiToolKit.Application.DTOs.Merchant.EmailPreference;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantEmailPreferenceService
{
    Task<Result<object>> GetEmailPreferencesAsync(GetEmailPreferencesRequest request);
    Task<Result<object>> UpdateEmailPreferencesAsync(UpdateEmailPreferencesRequest request);
}
