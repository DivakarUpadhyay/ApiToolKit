using ApiToolKit.Application.DTOs.BigQuery.Auth;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.BigQuery;

public interface IBigQueryAuthService
{
    Task<Result<GetAuthUrlResponse>> GetAuthUrlAsync();
    Task<Result<BigQueryTokenResponse>> ExchangeCodeAsync(ExchangeCodeRequest request);
    Task<Result<BigQueryTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
}
