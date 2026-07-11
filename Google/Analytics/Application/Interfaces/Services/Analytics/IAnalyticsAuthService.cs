using ApiToolKit.Application.DTOs.Analytics.Auth;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Analytics;

public interface IAnalyticsAuthService
{
    Result<GetAuthUrlResponse> GetAuthorizationUrl();
    Task<Result<AnalyticsTokenResponse>> ExchangeCodeAsync(ExchangeCodeRequest request);
    Task<Result<AnalyticsTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
}
