using ApiToolKit.Application.DTOs.YouTube.Auth;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.YouTube;
using ApiToolKit.Domain.Interfaces.Repositories.YouTube;

namespace ApiToolKit.Application.Services.YouTube;

public class YouTubeAuthService : IYouTubeAuthService
{
    private readonly IYouTubeAuthRepository _authRepo;
    private readonly IYouTubeApiClient _apiClient;

    public YouTubeAuthService(IYouTubeAuthRepository authRepo, IYouTubeApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public Result<GetAuthUrlResponse> GetAuthorizationUrl(string[] scopes)
    {
        var url = _apiClient.BuildAuthorizationUrl(scopes);
        return Result<GetAuthUrlResponse>.Ok(new GetAuthUrlResponse { AuthUrl = url });
    }

    public async Task<Result<YouTubeTokenResponse>> ExchangeCodeAsync(ExchangeCodeRequest request, string userId)
    {
        var tokenResponse = await _apiClient.ExchangeCodeAsync(request.Code, request.RedirectUri);
        if (tokenResponse == null) return Result<YouTubeTokenResponse>.Fail("Failed to exchange authorization code.");

        var token = new YouTubeToken
        {
            UserId = userId,
            AccessToken = tokenResponse.AccessToken,
            RefreshToken = tokenResponse.RefreshToken,
            ExpiresAt = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn),
            Scope = tokenResponse.Scope,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var existing = await _authRepo.GetTokenByUserIdAsync(userId);
        if (existing == null)
            await _authRepo.SaveTokenAsync(token);
        else
            await _authRepo.UpdateTokenAsync(token);

        await _authRepo.SaveApiLogAsync(new YouTubeApiLog
        {
            UserId = userId,
            ApiName = "ExchangeCode",
            RequestBody = request.Code,
            ResponseBody = tokenResponse.AccessToken,
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<YouTubeTokenResponse>.Ok(tokenResponse);
    }

    public async Task<Result<YouTubeTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var tokenResponse = await _apiClient.RefreshTokenAsync(request.RefreshToken);
        if (tokenResponse == null) return Result<YouTubeTokenResponse>.Fail("Failed to refresh token.");

        var existing = await _authRepo.GetTokenByUserIdAsync(request.UserId);
        var token = new YouTubeToken
        {
            UserId = request.UserId,
            AccessToken = tokenResponse.AccessToken,
            RefreshToken = string.IsNullOrEmpty(tokenResponse.RefreshToken) ? request.RefreshToken : tokenResponse.RefreshToken,
            ExpiresAt = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn),
            Scope = tokenResponse.Scope,
            CreatedAt = existing?.CreatedAt ?? DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        if (existing == null)
            await _authRepo.SaveTokenAsync(token);
        else
            await _authRepo.UpdateTokenAsync(token);

        return Result<YouTubeTokenResponse>.Ok(tokenResponse);
    }
}
