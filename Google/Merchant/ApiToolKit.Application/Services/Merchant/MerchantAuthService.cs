using ApiToolKit.Application.DTOs.Merchant.Auth;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Merchant;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantAuthService : IMerchantAuthService
{
    private readonly IMerchantApiClient _apiClient;
    private readonly IMerchantAuthRepository _authRepo;

    public MerchantAuthService(IMerchantApiClient apiClient, IMerchantAuthRepository authRepo)
    {
        _apiClient = apiClient;
        _authRepo = authRepo;
    }

    public Result<GetAuthUrlResponse> GetAuthorizationUrl(string accountKey)
    {
        var url = _apiClient.BuildAuthorizationUrl(accountKey);
        return Result<GetAuthUrlResponse>.Ok(new GetAuthUrlResponse { AuthUrl = url });
    }

    public async Task<Result<MerchantTokenResponse>> ExchangeCodeAsync(string code, string accountKey)
    {
        var tokenResponse = await _apiClient.ExchangeCodeAsync(code);
        if (!string.IsNullOrEmpty(tokenResponse.Error))
            return Result<MerchantTokenResponse>.Fail($"Token exchange failed: {tokenResponse.Error} - {tokenResponse.ErrorDescription}");

        await SaveTokenAsync(accountKey, tokenResponse);
        return Result<MerchantTokenResponse>.Ok(tokenResponse);
    }

    public async Task<Result<MerchantTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var storedToken = await _authRepo.GetTokenAsync(request.AccountKey);
        if (storedToken == null || string.IsNullOrEmpty(storedToken.RefreshToken))
            return Result<MerchantTokenResponse>.Fail("No refresh token found for the given account key.");

        var tokenResponse = await _apiClient.RefreshTokenAsync(storedToken.RefreshToken);
        if (!string.IsNullOrEmpty(tokenResponse.Error))
            return Result<MerchantTokenResponse>.Fail($"Token refresh failed: {tokenResponse.Error} - {tokenResponse.ErrorDescription}");

        await SaveTokenAsync(request.AccountKey, tokenResponse);
        return Result<MerchantTokenResponse>.Ok(tokenResponse);
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

    private Task SaveTokenAsync(string accountKey, MerchantTokenResponse tokenResponse) =>
        _authRepo.SaveTokenAsync(new MerchantToken
        {
            AccountKey = accountKey,
            AccessToken = tokenResponse.AccessToken,
            RefreshToken = tokenResponse.RefreshToken,
            ExpiresAt = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn),
            Scope = tokenResponse.Scope
        });
}
