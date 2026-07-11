using ApiToolKit.Application.DTOs.Cloud.Auth;
using ApiToolKit.Application.Interfaces.Services.Cloud;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Cloud;
using ApiToolKit.Domain.Interfaces.Repositories.Cloud;

namespace ApiToolKit.Application.Services.Cloud;

public class CloudAuthService : ICloudAuthService
{
    private readonly ICloudAuthRepository _authRepo;
    private readonly ICloudApiClient _apiClient;

    private static readonly string[] CloudScopes = new[]
    {
        "https://www.googleapis.com/auth/cloud-platform"
    };

    public CloudAuthService(ICloudAuthRepository authRepo, ICloudApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<GetAuthUrlResponse>> GetAuthUrlAsync()
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetAuthUrlResponse>.Fail("Cloud configuration not found.");

        var url = _apiClient.BuildAuthorizationUrl(CloudScopes);
        return Result<GetAuthUrlResponse>.Ok(new GetAuthUrlResponse { AuthUrl = url });
    }

    public async Task<Result<CloudTokenResponse>> ExchangeCodeAsync(ExchangeCodeRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<CloudTokenResponse>.Fail("Cloud configuration not found.");

        var tokenResponse = await _apiClient.ExchangeCodeAsync(request.Code, request.RedirectUri);
        if (tokenResponse == null) return Result<CloudTokenResponse>.Fail("Failed to exchange authorization code.");

        var token = new CloudToken
        {
            ProjectId = config.ProjectId,
            AccessToken = tokenResponse.AccessToken,
            RefreshToken = tokenResponse.RefreshToken,
            ExpiresAt = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn),
            Scope = tokenResponse.Scope,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var existing = await _authRepo.GetTokenByProjectIdAsync(config.ProjectId);
        if (existing == null)
            await _authRepo.SaveTokenAsync(token);
        else
        {
            token.Id = existing.Id;
            await _authRepo.UpdateTokenAsync(token);
        }

        await _authRepo.SaveApiLogAsync(new CloudApiLog
        {
            ProjectId = config.ProjectId,
            Product = "Auth",
            ApiName = "ExchangeCode",
            RequestBody = request.Code,
            ResponseBody = tokenResponse.AccessToken,
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<CloudTokenResponse>.Ok(tokenResponse);
    }

    public async Task<Result<CloudTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var tokenResponse = await _apiClient.RefreshTokenAsync(request.RefreshToken);
        if (tokenResponse == null) return Result<CloudTokenResponse>.Fail("Failed to refresh token.");

        var existing = await _authRepo.GetTokenByProjectIdAsync(request.ProjectId);
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

        await _authRepo.SaveApiLogAsync(new CloudApiLog
        {
            ProjectId = request.ProjectId,
            Product = "Auth",
            ApiName = "RefreshToken",
            RequestBody = request.RefreshToken,
            ResponseBody = tokenResponse.AccessToken,
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<CloudTokenResponse>.Ok(tokenResponse);
    }
}
