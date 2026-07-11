using ApiToolKit.Application.DTOs.GoogleAds.Auth;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.GoogleAds;

public interface IGoogleAdsAuthService
{
    Task<Result<GetAuthUrlResponse>> GetAuthUrlAsync();
    Task<Result<GoogleTokenResponse>> ExchangeCodeAsync(ExchangeCodeRequest request);
    Task<Result<GoogleTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
}
