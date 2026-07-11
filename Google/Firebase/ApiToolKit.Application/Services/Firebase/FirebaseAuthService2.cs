using ApiToolKit.Application.DTOs.Firebase.Auth;
using ApiToolKit.Application.Interfaces.Services.Firebase;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Firebase;
using ApiToolKit.Domain.Interfaces.Repositories.Firebase;

namespace ApiToolKit.Application.Services.Firebase;

public class FirebaseAuthService2 : IFirebaseAuthService2
{
    private readonly IFirebaseApiClient _apiClient;
    private readonly IFirebaseAuthRepository _authRepo;

    public FirebaseAuthService2(IFirebaseApiClient apiClient, IFirebaseAuthRepository authRepo)
    {
        _apiClient = apiClient;
        _authRepo = authRepo;
    }

    public async Task<Result<GetAuthUrlResponse>> GetAuthUrlAsync()
    {
        try
        {
            var url = _apiClient.BuildAuthorizationUrl();
            return Result<GetAuthUrlResponse>.Ok(new GetAuthUrlResponse { AuthUrl = url });
        }
        catch (Exception ex)
        {
            return Result<GetAuthUrlResponse>.Fail($"Failed to build authorization URL: {ex.Message}");
        }
    }

    public async Task<Result<FirebaseTokenResponse>> ExchangeCodeAsync(ExchangeCodeRequest request)
    {
        try
        {
            var response = await _apiClient.ExchangeCodeAsync(request.Code, request.RedirectUri);
            if (response == null) return Result<FirebaseTokenResponse>.Fail("No response from Google OAuth.");

            await _authRepo.SaveApiLogAsync(new FirebaseApiLog
            {
                Product = "Auth",
                ApiName = "ExchangeCode",
                RequestBody = $"code={request.Code}",
                ResponseBody = $"token_type={response.TokenType}",
                IsSuccess = true,
                StatusCode = 200,
                CreatedAt = DateTime.UtcNow
            });

            return Result<FirebaseTokenResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<FirebaseTokenResponse>.Fail($"Token exchange failed: {ex.Message}");
        }
    }

    public async Task<Result<FirebaseTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        try
        {
            var response = await _apiClient.RefreshTokenAsync(request.RefreshToken);
            if (response == null) return Result<FirebaseTokenResponse>.Fail("No response from Google OAuth.");
            return Result<FirebaseTokenResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<FirebaseTokenResponse>.Fail($"Token refresh failed: {ex.Message}");
        }
    }
}
