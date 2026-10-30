using ApiToolKit.Application.DTOs.BusinessProfile.Auth;
using ApiToolKit.Application.DTOs.BusinessProfile.Common;

namespace ApiToolKit.Application.Interfaces.Services.BusinessProfile;

public interface IBusinessProfileApiClient
{
    string BuildAuthorizationUrl(string state);
    Task<BusinessProfileTokenResponse> ExchangeCodeAsync(string code);
    Task<BusinessProfileTokenResponse> RefreshTokenAsync(string refreshToken);
    Task<BusinessProfileApiResponse> SendAsync(HttpMethod method, string url, string accessToken,
        IEnumerable<KeyValuePair<string, string?>>? query = null, object? body = null, bool raw = false);
}
