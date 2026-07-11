using ApiToolKit.Application.DTOs.BigQuery.Auth;
using ApiToolKit.Application.Interfaces.Services.BigQuery;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.BigQuery;
using ApiToolKit.Domain.Interfaces.Repositories.BigQuery;

namespace ApiToolKit.Application.Services.BigQuery;

public class BigQueryAuthService : IBigQueryAuthService
{
    private readonly IBigQueryAuthRepository _authRepo;
    private readonly IBigQueryApiClient _apiClient;

    public BigQueryAuthService(IBigQueryAuthRepository authRepo, IBigQueryApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<GetAuthUrlResponse>> GetAuthUrlAsync()
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<GetAuthUrlResponse>.Fail("BigQuery configuration not found.");

        var url = _apiClient.BuildAuthorizationUrl();
        return Result<GetAuthUrlResponse>.Ok(new GetAuthUrlResponse { AuthUrl = url });
    }

    public async Task<Result<BigQueryTokenResponse>> ExchangeCodeAsync(ExchangeCodeRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null)
            return Result<BigQueryTokenResponse>.Fail("BigQuery configuration not found.");

        var tokenResponse = await _apiClient.ExchangeCodeAsync(request.Code, request.RedirectUri);
        if (tokenResponse.AccessToken == null)
            return Result<BigQueryTokenResponse>.Fail("Failed to exchange authorization code.");

        var log = new BigQueryApiLog
        {
            ProjectId = "auth",
            ApiName = "ExchangeCode",
            RequestBody = request.Code,
            ResponseBody = tokenResponse.TokenType,
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        };
        await _authRepo.SaveApiLogAsync(log);

        return Result<BigQueryTokenResponse>.Ok(tokenResponse);
    }

    public async Task<Result<BigQueryTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var existingToken = await _authRepo.GetTokenByProjectIdAsync(request.ProjectId);
        var refreshToken = request.RefreshToken;

        if (existingToken != null && string.IsNullOrEmpty(refreshToken))
            refreshToken = existingToken.RefreshToken ?? string.Empty;

        var tokenResponse = await _apiClient.RefreshTokenAsync(refreshToken);
        if (tokenResponse.AccessToken == null)
            return Result<BigQueryTokenResponse>.Fail("Failed to refresh access token.");

        var updatedToken = new BigQueryToken
        {
            Id = existingToken?.Id ?? 0,
            ProjectId = request.ProjectId,
            AccessToken = tokenResponse.AccessToken,
            RefreshToken = tokenResponse.RefreshToken ?? refreshToken,
            ExpiresAt = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn),
            Scope = tokenResponse.Scope,
            CreatedAt = existingToken?.CreatedAt ?? DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        if (existingToken == null)
            await _authRepo.SaveTokenAsync(updatedToken);
        else
            await _authRepo.UpdateTokenAsync(updatedToken);

        return Result<BigQueryTokenResponse>.Ok(tokenResponse);
    }
}
