using ApiToolKit.Application.DTOs.Analytics.Auth;
using ApiToolKit.Application.Interfaces.Services.Analytics;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Analytics;
using ApiToolKit.Domain.Interfaces.Repositories.Analytics;

namespace ApiToolKit.Application.Services.Analytics;

public class AnalyticsAuthService : IAnalyticsAuthService
{
    private readonly IAnalyticsApiClient _apiClient;
    private readonly IAnalyticsAuthRepository _authRepo;

    public AnalyticsAuthService(IAnalyticsApiClient apiClient, IAnalyticsAuthRepository authRepo)
    {
        _apiClient = apiClient;
        _authRepo = authRepo;
    }

    public Result<GetAuthUrlResponse> GetAuthorizationUrl()
    {
        var url = _apiClient.BuildAuthorizationUrl();
        return Result<GetAuthUrlResponse>.Ok(new GetAuthUrlResponse { AuthUrl = url });
    }

    public async Task<Result<AnalyticsTokenResponse>> ExchangeCodeAsync(ExchangeCodeRequest request)
    {
        var tokenResponse = await _apiClient.ExchangeCodeAsync(request.Code);

        if (!string.IsNullOrEmpty(tokenResponse.Error))
            return Result<AnalyticsTokenResponse>.Fail($"Token exchange failed: {tokenResponse.Error} - {tokenResponse.ErrorDescription}");

        var existing = await _authRepo.GetTokenByPropertyIdAsync(request.PropertyId);
        if (existing != null)
        {
            existing.AccessToken = tokenResponse.AccessToken;
            if (!string.IsNullOrEmpty(tokenResponse.RefreshToken))
                existing.RefreshToken = tokenResponse.RefreshToken;
            existing.ExpiresAt = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn);
            existing.Scope = tokenResponse.Scope;
            existing.UpdatedAt = DateTime.UtcNow;
            await _authRepo.UpdateTokenAsync(existing);
        }
        else
        {
            var token = new AnalyticsToken
            {
                PropertyId = request.PropertyId,
                AccessToken = tokenResponse.AccessToken,
                RefreshToken = tokenResponse.RefreshToken,
                ExpiresAt = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn),
                Scope = tokenResponse.Scope,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await _authRepo.SaveTokenAsync(token);
        }

        return Result<AnalyticsTokenResponse>.Ok(tokenResponse);
    }

    public async Task<Result<AnalyticsTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var storedToken = await _authRepo.GetTokenByPropertyIdAsync(request.PropertyId);
        if (storedToken == null || string.IsNullOrEmpty(storedToken.RefreshToken))
            return Result<AnalyticsTokenResponse>.Fail("No refresh token found for the given property.");

        var tokenResponse = await _apiClient.RefreshTokenAsync(storedToken.RefreshToken);

        if (!string.IsNullOrEmpty(tokenResponse.Error))
            return Result<AnalyticsTokenResponse>.Fail($"Token refresh failed: {tokenResponse.Error} - {tokenResponse.ErrorDescription}");

        storedToken.AccessToken = tokenResponse.AccessToken;
        storedToken.ExpiresAt = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn);
        storedToken.UpdatedAt = DateTime.UtcNow;
        await _authRepo.UpdateTokenAsync(storedToken);

        return Result<AnalyticsTokenResponse>.Ok(tokenResponse);
    }
}
