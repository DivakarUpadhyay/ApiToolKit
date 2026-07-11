using ApiToolKit.Domain.Entities.GoogleAds;
using ApiToolKit.Domain.Interfaces.Repositories.GoogleAds;
using ApiToolKit.Infrastructure.Data;
using Dapper;
using System.Data;

namespace ApiToolKit.Infrastructure.Repositories.GoogleAds;

public class GoogleAdsAuthRepository : IGoogleAdsAuthRepository
{
    private readonly DapperContext _context;

    public GoogleAdsAuthRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<GoogleAdsConfig?> GetConfigAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<GoogleAdsConfig>(
            "sp_GoogleAds_GetConfig",
            commandType: CommandType.StoredProcedure);
    }

    public async Task<GoogleAdsToken?> GetTokenByCustomerIdAsync(string customerId)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<GoogleAdsToken>(
            "sp_GoogleAds_GetToken",
            new { CustomerId = customerId },
            commandType: CommandType.StoredProcedure);
    }

    public async Task SaveTokenAsync(GoogleAdsToken token)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_GoogleAds_SaveToken",
            new
            {
                token.CustomerId,
                token.AccessToken,
                token.RefreshToken,
                token.ExpiresAt,
                token.Scope,
                token.CreatedAt,
                token.UpdatedAt
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateTokenAsync(GoogleAdsToken token)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_GoogleAds_UpdateToken",
            new
            {
                token.CustomerId,
                token.AccessToken,
                token.RefreshToken,
                token.ExpiresAt,
                token.Scope,
                UpdatedAt = DateTime.UtcNow
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task SaveApiLogAsync(GoogleAdsApiLog log)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_GoogleAds_SaveApiLog",
            new
            {
                log.CustomerId,
                log.ApiName,
                log.RequestBody,
                log.ResponseBody,
                log.IsSuccess,
                log.StatusCode,
                CreatedAt = DateTime.UtcNow
            },
            commandType: CommandType.StoredProcedure);
    }
}
