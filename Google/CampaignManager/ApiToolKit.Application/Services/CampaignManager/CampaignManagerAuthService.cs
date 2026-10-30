using ApiToolKit.Application.DTOs.CampaignManager.Auth;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.CampaignManager;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerAuthService : ICampaignManagerAuthService
{
    private readonly ICampaignManagerApiClient _apiClient;
    private readonly ICampaignManagerAuthRepository _authRepo;

    public CampaignManagerAuthService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthRepository authRepo)
    {
        _apiClient = apiClient;
        _authRepo = authRepo;
    }

    public Result<GetAuthUrlResponse> GetAuthorizationUrl(string accountKey)
    {
        var url = _apiClient.BuildAuthorizationUrl(accountKey);
        return Result<GetAuthUrlResponse>.Ok(new GetAuthUrlResponse { AuthUrl = url });
    }

    public async Task<Result<CampaignManagerTokenResponse>> ExchangeCodeAsync(string code, string accountKey)
    {
        var tokenResponse = await _apiClient.ExchangeCodeAsync(code);
        if (!string.IsNullOrEmpty(tokenResponse.Error))
            return Result<CampaignManagerTokenResponse>.Fail($"Token exchange failed: {tokenResponse.Error} - {tokenResponse.ErrorDescription}");

        await SaveTokenAsync(accountKey, tokenResponse);
        return Result<CampaignManagerTokenResponse>.Ok(tokenResponse);
    }

    public async Task<Result<CampaignManagerTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var storedToken = await _authRepo.GetTokenAsync(request.AccountKey);
        if (storedToken == null || string.IsNullOrEmpty(storedToken.RefreshToken))
            return Result<CampaignManagerTokenResponse>.Fail("No refresh token found for the given account key.");

        var tokenResponse = await _apiClient.RefreshTokenAsync(storedToken.RefreshToken);
        if (!string.IsNullOrEmpty(tokenResponse.Error))
            return Result<CampaignManagerTokenResponse>.Fail($"Token refresh failed: {tokenResponse.Error} - {tokenResponse.ErrorDescription}");

        await SaveTokenAsync(request.AccountKey, tokenResponse);
        return Result<CampaignManagerTokenResponse>.Ok(tokenResponse);
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

    private Task SaveTokenAsync(string accountKey, CampaignManagerTokenResponse tokenResponse) =>
        _authRepo.SaveTokenAsync(new CampaignManagerToken
        {
            AccountKey = accountKey,
            AccessToken = tokenResponse.AccessToken,
            RefreshToken = tokenResponse.RefreshToken,
            ExpiresAt = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn),
            Scope = tokenResponse.Scope
        });
}
