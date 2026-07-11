using ApiToolKit.Application.DTOs.GoogleAds.Auth;
using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.GoogleAds;
using ApiToolKit.Domain.Interfaces.Repositories.GoogleAds;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.GoogleAds;

public class GoogleAdsAuthService : IGoogleAdsAuthService
{
    private readonly IGoogleAdsAuthRepository _authRepo;
    private readonly IGoogleAdsApiClient _apiClient;

    public GoogleAdsAuthService(IGoogleAdsAuthRepository authRepo, IGoogleAdsApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<GetAuthUrlResponse>> GetAuthUrlAsync()
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<GetAuthUrlResponse>.Fail("Google Ads configuration not found.");

        var scopes = new[] { "https://www.googleapis.com/auth/adwords" };
        var url = _apiClient.BuildAuthorizationUrl(scopes);
        return Result<GetAuthUrlResponse>.Ok(new GetAuthUrlResponse { AuthUrl = url });
    }

    public async Task<Result<GoogleTokenResponse>> ExchangeCodeAsync(ExchangeCodeRequest request)
    {
        var response = await _apiClient.ExchangeCodeAsync(request.Code, request.RedirectUri);
        if (response == null)
            return Result<GoogleTokenResponse>.Fail("No response from Google OAuth endpoint.");

        await _authRepo.SaveTokenAsync(new GoogleAdsToken
        {
            CustomerId = string.Empty,
            AccessToken = response.AccessToken,
            RefreshToken = response.RefreshToken,
            ExpiresAt = DateTime.UtcNow.AddSeconds(response.ExpiresIn),
            Scope = response.Scope,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        await _authRepo.SaveApiLogAsync(new GoogleAdsApiLog
        {
            CustomerId = string.Empty,
            ApiName = "ExchangeCode",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<GoogleTokenResponse>.Ok(response);
    }

    public async Task<Result<GoogleTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var response = await _apiClient.RefreshTokenAsync(request.RefreshToken);
        if (response == null)
            return Result<GoogleTokenResponse>.Fail("No response from Google OAuth endpoint.");

        var existingToken = await _authRepo.GetTokenByCustomerIdAsync(request.CustomerId);
        if (existingToken != null)
        {
            existingToken.AccessToken = response.AccessToken;
            existingToken.ExpiresAt = DateTime.UtcNow.AddSeconds(response.ExpiresIn);
            existingToken.UpdatedAt = DateTime.UtcNow;
            await _authRepo.UpdateTokenAsync(existingToken);
        }
        else
        {
            await _authRepo.SaveTokenAsync(new GoogleAdsToken
            {
                CustomerId = request.CustomerId,
                AccessToken = response.AccessToken,
                RefreshToken = request.RefreshToken,
                ExpiresAt = DateTime.UtcNow.AddSeconds(response.ExpiresIn),
                Scope = response.Scope,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        return Result<GoogleTokenResponse>.Ok(response);
    }
}
