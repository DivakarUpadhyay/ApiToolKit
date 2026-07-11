using ApiToolKit.Application.DTOs.Firebase.Auth;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Firebase;

public interface IFirebaseAuthService2
{
    Task<Result<GetAuthUrlResponse>> GetAuthUrlAsync();
    Task<Result<FirebaseTokenResponse>> ExchangeCodeAsync(ExchangeCodeRequest request);
    Task<Result<FirebaseTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
}
