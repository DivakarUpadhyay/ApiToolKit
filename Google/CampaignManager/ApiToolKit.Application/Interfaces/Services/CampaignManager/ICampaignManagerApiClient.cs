using ApiToolKit.Application.DTOs.CampaignManager.Auth;
using ApiToolKit.Application.DTOs.CampaignManager.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerApiClient
{
    string BuildAuthorizationUrl(string state);
    Task<CampaignManagerTokenResponse> ExchangeCodeAsync(string code);
    Task<CampaignManagerTokenResponse> RefreshTokenAsync(string refreshToken);
    Task<CampaignManagerApiResponse> SendAsync(HttpMethod method, string url, string accessToken,
        IEnumerable<KeyValuePair<string, string?>>? query = null, object? body = null, bool raw = false);
}
