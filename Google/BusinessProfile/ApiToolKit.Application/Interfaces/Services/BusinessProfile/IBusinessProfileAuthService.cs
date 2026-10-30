using ApiToolKit.Application.DTOs.BusinessProfile.Auth;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.BusinessProfile;

public interface IBusinessProfileAuthService
{
    Result<GetAuthUrlResponse> GetAuthorizationUrl(string accountKey);
    Task<Result<BusinessProfileTokenResponse>> ExchangeCodeAsync(string code, string accountKey);
    Task<Result<BusinessProfileTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
    Task<string?> GetValidAccessTokenAsync(string accountKey);
}
