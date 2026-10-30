using ApiToolKit.Application.DTOs.DisplayVideo.Auth;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.DisplayVideo;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoAuthService : IDisplayVideoAuthService
{
    private readonly IDisplayVideoApiClient _apiClient;
    private readonly IDisplayVideoAuthRepository _authRepo;

    public DisplayVideoAuthService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthRepository authRepo)
    {
        _apiClient = apiClient;
        _authRepo = authRepo;
    }

    public Result<GetAuthUrlResponse> GetAuthorizationUrl(string accountKey)
    {
        var url = _apiClient.BuildAuthorizationUrl(accountKey);
        return Result<GetAuthUrlResponse>.Ok(new GetAuthUrlResponse { AuthUrl = url });
    }

    public async Task<Result<DisplayVideoTokenResponse>> ExchangeCodeAsync(string code, string accountKey)
    {
        var tokenResponse = await _apiClient.ExchangeCodeAsync(code);
        if (!string.IsNullOrEmpty(tokenResponse.Error))
            return Result<DisplayVideoTokenResponse>.Fail($"Token exchange failed: {tokenResponse.Error} - {tokenResponse.ErrorDescription}");

        await SaveTokenAsync(accountKey, tokenResponse);
        return Result<DisplayVideoTokenResponse>.Ok(tokenResponse);
    }

    public async Task<Result<DisplayVideoTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var storedToken = await _authRepo.GetTokenAsync(request.AccountKey);
        if (storedToken == null || string.IsNullOrEmpty(storedToken.RefreshToken))
            return Result<DisplayVideoTokenResponse>.Fail("No refresh token found for the given account key.");

        var tokenResponse = await _apiClient.RefreshTokenAsync(storedToken.RefreshToken);
        if (!string.IsNullOrEmpty(tokenResponse.Error))
            return Result<DisplayVideoTokenResponse>.Fail($"Token refresh failed: {tokenResponse.Error} - {tokenResponse.ErrorDescription}");

        await SaveTokenAsync(request.AccountKey, tokenResponse);
        return Result<DisplayVideoTokenResponse>.Ok(tokenResponse);
    }

    public async Task<string?> GetValidAccessTokenAsync(string accountKey)
    {
        var storedToken = await _authRepo.GetTokenAsync(accountKey);
        if (storedToken == null) return null;
        if (storedToken.ExpiresAt > DateTime.UtcNow.AddMinutes(1)) return storedToken.AccessToken;
        if (string.IsNullOrEmpty(storedToken.RefreshToken)) return null;

        var tokenResponse = await _apiClient.RefreshTokenAsync(storedToken.RefreshToken);
        if (!string.IsNullOrEmpty(tokenResponse.Error) || string.IsNullOrEmpty(tokenResponse.AccessToken)) return null;

        await SaveTokenAsync(accountKey, tokenResponse);
        return tokenResponse.AccessToken;
    }

    private Task SaveTokenAsync(string accountKey, DisplayVideoTokenResponse tokenResponse) =>
        _authRepo.SaveTokenAsync(new DisplayVideoToken
        {
            AccountKey = accountKey,
            AccessToken = tokenResponse.AccessToken,
            RefreshToken = tokenResponse.RefreshToken,
            ExpiresAt = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn),
            Scope = tokenResponse.Scope
        });
}
