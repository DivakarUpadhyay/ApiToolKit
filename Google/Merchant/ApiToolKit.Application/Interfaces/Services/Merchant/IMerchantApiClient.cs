using ApiToolKit.Application.DTOs.Merchant.Auth;
using ApiToolKit.Application.DTOs.Merchant.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantApiClient
{
    string BuildAuthorizationUrl(string state);
    Task<MerchantTokenResponse> ExchangeCodeAsync(string code);
    Task<MerchantTokenResponse> RefreshTokenAsync(string refreshToken);
    Task<MerchantApiResponse> SendAsync(HttpMethod method, string url, string accessToken,
        IEnumerable<KeyValuePair<string, string?>>? query = null, object? body = null, bool raw = false);
}
