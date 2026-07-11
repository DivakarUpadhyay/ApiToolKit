using ApiToolKit.Application.DTOs.Workspace.Auth;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Workspace;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Workspace;

public class WorkspaceAuthService : IWorkspaceAuthService
{
    private readonly IWorkspaceAuthRepository _authRepo;
    private readonly IWorkspaceApiClient _apiClient;

    public WorkspaceAuthService(IWorkspaceAuthRepository authRepo, IWorkspaceApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public Result<GetAuthUrlResponse> GetAuthorizationUrl(string[] scopes, string state = "")
    {
        var url = _apiClient.BuildAuthorizationUrl(scopes, state);
        return Result<GetAuthUrlResponse>.Ok(new GetAuthUrlResponse { AuthUrl = url });
    }

    public async Task<Result<WorkspaceTokenResponse>> ExchangeCodeAsync(ExchangeCodeRequest request)
    {
        var response = await _apiClient.ExchangeCodeAsync<WorkspaceTokenResponse>(request.Code, request.RedirectUri);
        if (response == null) return Result<WorkspaceTokenResponse>.Fail("Failed to exchange code for token.");

        var token = new WorkspaceToken
        {
            UserId = "default",
            AccessToken = response.AccessToken,
            RefreshToken = response.RefreshToken ?? string.Empty,
            ExpiresAt = DateTime.UtcNow.AddSeconds(response.ExpiresIn),
            Scope = response.Scope ?? string.Empty,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var existing = await _authRepo.GetTokenByUserIdAsync(token.UserId);
        if (existing == null)
            await _authRepo.SaveTokenAsync(token);
        else
            await _authRepo.UpdateTokenAsync(token);

        return Result<WorkspaceTokenResponse>.Ok(response);
    }

    public async Task<Result<WorkspaceTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var response = await _apiClient.RefreshTokenAsync<WorkspaceTokenResponse>(request.RefreshToken);
        if (response == null) return Result<WorkspaceTokenResponse>.Fail("Failed to refresh token.");

        var existing = await _authRepo.GetTokenByUserIdAsync(request.UserId);
        if (existing != null)
        {
            existing.AccessToken = response.AccessToken;
            if (!string.IsNullOrEmpty(response.RefreshToken))
                existing.RefreshToken = response.RefreshToken;
            existing.ExpiresAt = DateTime.UtcNow.AddSeconds(response.ExpiresIn);
            existing.UpdatedAt = DateTime.UtcNow;
            await _authRepo.UpdateTokenAsync(existing);
        }

        return Result<WorkspaceTokenResponse>.Ok(response);
    }

    public async Task<string?> GetAccessTokenAsync(string userId)
    {
        var token = await _authRepo.GetTokenByUserIdAsync(userId);
        if (token == null) return null;

        if (token.ExpiresAt <= DateTime.UtcNow.AddMinutes(5))
        {
            var refreshed = await _apiClient.RefreshTokenAsync<WorkspaceTokenResponse>(token.RefreshToken);
            if (refreshed != null)
            {
                token.AccessToken = refreshed.AccessToken;
                if (!string.IsNullOrEmpty(refreshed.RefreshToken))
                    token.RefreshToken = refreshed.RefreshToken;
                token.ExpiresAt = DateTime.UtcNow.AddSeconds(refreshed.ExpiresIn);
                token.UpdatedAt = DateTime.UtcNow;
                await _authRepo.UpdateTokenAsync(token);
            }
        }

        return token.AccessToken;
    }
}
