using ApiToolKit.Application.DTOs.BusinessProfile.Auth;
using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.BusinessProfile;
using ApiToolKit.Domain.Interfaces.Repositories.BusinessProfile;

namespace ApiToolKit.Application.Services.BusinessProfile;

public class BusinessProfileAuthService : IBusinessProfileAuthService
{
    private readonly IBusinessProfileApiClient _apiClient;
    private readonly IBusinessProfileAuthRepository _authRepo;

    public BusinessProfileAuthService(IBusinessProfileApiClient apiClient, IBusinessProfileAuthRepository authRepo)
    {
        _apiClient = apiClient;
        _authRepo = authRepo;
    }

    public Result<GetAuthUrlResponse> GetAuthorizationUrl(string accountKey)
    {
        var url = _apiClient.BuildAuthorizationUrl(accountKey);
        return Result<GetAuthUrlResponse>.Ok(new GetAuthUrlResponse { AuthUrl = url });
    }

    public async Task<Result<BusinessProfileTokenResponse>> ExchangeCodeAsync(string code, string accountKey)
    {
        var tokenResponse = await _apiClient.ExchangeCodeAsync(code);
        if (!string.IsNullOrEmpty(tokenResponse.Error))
            return Result<BusinessProfileTokenResponse>.Fail($"Token exchange failed: {tokenResponse.Error} - {tokenResponse.ErrorDescription}");

        await SaveTokenAsync(accountKey, tokenResponse);
        return Result<BusinessProfileTokenResponse>.Ok(tokenResponse);
    }

    public async Task<Result<BusinessProfileTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var storedToken = await _authRepo.GetTokenAsync(request.AccountKey);
        if (storedToken == null || string.IsNullOrEmpty(storedToken.RefreshToken))
            return Result<BusinessProfileTokenResponse>.Fail("No refresh token found for the given account key.");

        var tokenResponse = await _apiClient.RefreshTokenAsync(storedToken.RefreshToken);
        if (!string.IsNullOrEmpty(tokenResponse.Error))
            return Result<BusinessProfileTokenResponse>.Fail($"Token refresh failed: {tokenResponse.Error} - {tokenResponse.ErrorDescription}");

        await SaveTokenAsync(request.AccountKey, tokenResponse);
        return Result<BusinessProfileTokenResponse>.Ok(tokenResponse);
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

    private Task SaveTokenAsync(string accountKey, BusinessProfileTokenResponse tokenResponse) =>
        _authRepo.SaveTokenAsync(new BusinessProfileToken
        {
            AccountKey = accountKey,
            AccessToken = tokenResponse.AccessToken,
            RefreshToken = tokenResponse.RefreshToken,
            ExpiresAt = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn),
            Scope = tokenResponse.Scope
        });
}
