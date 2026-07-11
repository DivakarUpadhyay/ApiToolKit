using ApiToolKit.Application.DTOs.Identity.OAuth;
using ApiToolKit.Application.Interfaces.Services.Identity;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Identity;
using ApiToolKit.Domain.Interfaces.Repositories.Identity;

namespace ApiToolKit.Application.Services.Identity;

public class IdentityOAuthService : IIdentityOAuthService
{
    private readonly IIdentityApiClient _apiClient;
    private readonly IIdentityAuthRepository _authRepo;

    public IdentityOAuthService(IIdentityApiClient apiClient, IIdentityAuthRepository authRepo)
    {
        _apiClient = apiClient;
        _authRepo = authRepo;
    }

    public async Task<Result<GetAuthUrlResponse>> GetAuthUrlAsync(AuthorizationRequest request)
    {
        try
        {
            var state = request.State ?? Guid.NewGuid().ToString("N");
            var url = _apiClient.BuildAuthorizationUrl(request.Scopes, state);
            return Result<GetAuthUrlResponse>.Ok(new GetAuthUrlResponse { AuthUrl = url, State = state });
        }
        catch (Exception ex)
        {
            return Result<GetAuthUrlResponse>.Fail($"Failed to build authorization URL: {ex.Message}");
        }
    }

    public async Task<Result<OAuthTokenResponse>> ExchangeCodeAsync(string code, string redirectUri)
    {
        try
        {
            var response = await _apiClient.ExchangeCodeAsync(code, redirectUri);
            if (response == null) return Result<OAuthTokenResponse>.Fail("No response from Google OAuth.");

            await _authRepo.SaveApiLogAsync(new IdentityApiLog
            {
                ApiName = "ExchangeCode",
                RequestBody = $"code={code}",
                ResponseBody = $"token_type={response.TokenType}",
                IsSuccess = true,
                StatusCode = 200,
                CreatedAt = DateTime.UtcNow
            });

            return Result<OAuthTokenResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<OAuthTokenResponse>.Fail($"Token exchange failed: {ex.Message}");
        }
    }

    public async Task<Result<OAuthTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        try
        {
            var response = await _apiClient.RefreshTokenAsync(request.RefreshToken);
            if (response == null) return Result<OAuthTokenResponse>.Fail("No response from Google OAuth.");
            return Result<OAuthTokenResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<OAuthTokenResponse>.Fail($"Token refresh failed: {ex.Message}");
        }
    }

    public async Task<Result<bool>> RevokeTokenAsync(RevokeTokenRequest request)
    {
        try
        {
            var success = await _apiClient.RevokeTokenAsync(request.Token);
            if (!success) return Result<bool>.Fail("Token revocation failed.");
            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Fail($"Token revocation error: {ex.Message}");
        }
    }

    public async Task<Result<TokenInfoResponse>> GetTokenInfoAsync(string accessToken)
    {
        try
        {
            var queryParams = new Dictionary<string, string> { ["id_token"] = accessToken };
            var response = await _apiClient.GetAsync<TokenInfoResponse>(
                "https://oauth2.googleapis.com/tokeninfo", null, queryParams);
            if (response == null) return Result<TokenInfoResponse>.Fail("Failed to get token info.");
            return Result<TokenInfoResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<TokenInfoResponse>.Fail($"Token info error: {ex.Message}");
        }
    }

    public async Task<Result<UserInfoResponse>> GetUserInfoAsync(string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<UserInfoResponse>(
                "https://openidconnect.googleapis.com/v1/userinfo", accessToken);
            if (response == null) return Result<UserInfoResponse>.Fail("Failed to get user info.");
            return Result<UserInfoResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<UserInfoResponse>.Fail($"User info error: {ex.Message}");
        }
    }
}
