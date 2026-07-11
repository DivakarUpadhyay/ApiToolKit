using ApiToolKit.Application.DTOs.Cloud.Auth;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Cloud;

public interface ICloudAuthService
{
    Task<Result<GetAuthUrlResponse>> GetAuthUrlAsync();
    Task<Result<CloudTokenResponse>> ExchangeCodeAsync(ExchangeCodeRequest request);
    Task<Result<CloudTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
}
