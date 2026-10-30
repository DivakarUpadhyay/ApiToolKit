using ApiToolKit.Application.DTOs.BusinessProfile.Verification;
using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.BusinessProfile;

namespace ApiToolKit.Application.Services.BusinessProfile;

public class BusinessProfileVerificationService : BusinessProfileServiceBase, IBusinessProfileVerificationService
{
    public BusinessProfileVerificationService(IBusinessProfileApiClient apiClient, IBusinessProfileAuthService authService, IBusinessProfileAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> FetchVerificationOptionsAsync(FetchVerificationOptionsRequest r)
    {
        var url = $"https://mybusinessverifications.googleapis.com/v1/locations/{r.LocationId}:fetchVerificationOptions";
        return await SendAsync(r.AccountKey, "FetchVerificationOptions", HttpMethod.Post, url, null, new { languageCode = r.LanguageCode, context = r.Context });
    }

    public async Task<Result<object>> VerifyLocationAsync(VerifyLocationRequest r)
    {
        var url = $"https://mybusinessverifications.googleapis.com/v1/locations/{r.LocationId}:verify";
        return await SendAsync(r.AccountKey, "VerifyLocation", HttpMethod.Post, url, null, new { method = r.Method, languageCode = r.LanguageCode, emailAddress = r.EmailAddress, phoneNumber = r.PhoneNumber, mailerContact = r.MailerContact, token = r.Token });
    }

    public async Task<Result<object>> ListVerificationsAsync(ListVerificationsRequest r)
    {
        var url = $"https://mybusinessverifications.googleapis.com/v1/locations/{r.LocationId}/verifications";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListVerifications", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> CompleteVerificationAsync(CompleteVerificationRequest r)
    {
        var url = $"https://mybusinessverifications.googleapis.com/v1/locations/{r.LocationId}/verifications/{r.VerificationId}:complete";
        return await SendAsync(r.AccountKey, "CompleteVerification", HttpMethod.Post, url, null, new { pin = r.Pin });
    }

    public async Task<Result<object>> GetVoiceOfMerchantStateAsync(GetVoiceOfMerchantStateRequest r)
    {
        var url = $"https://mybusinessverifications.googleapis.com/v1/locations/{r.LocationId}/VoiceOfMerchantState";
        return await SendAsync(r.AccountKey, "GetVoiceOfMerchantState", HttpMethod.Get, url);
    }
}
