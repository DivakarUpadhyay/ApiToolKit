using ApiToolKit.Application.DTOs.SearchConsole.Auth;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.SearchConsole;

public interface ISearchConsoleAuthService
{
    Result<GetAuthUrlResponse> GetAuthorizationUrl(string accountKey);
    Task<Result<SearchConsoleTokenResponse>> ExchangeCodeAsync(string code, string accountKey);
    Task<Result<SearchConsoleTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
    Task<string?> GetValidAccessTokenAsync(string accountKey);
}
