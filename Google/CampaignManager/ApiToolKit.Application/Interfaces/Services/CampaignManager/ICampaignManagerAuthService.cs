using ApiToolKit.Application.DTOs.CampaignManager.Auth;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerAuthService
{
    Result<GetAuthUrlResponse> GetAuthorizationUrl(string accountKey);
    Task<Result<CampaignManagerTokenResponse>> ExchangeCodeAsync(string code, string accountKey);
    Task<Result<CampaignManagerTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
    Task<string?> GetValidAccessTokenAsync(string accountKey);
}
