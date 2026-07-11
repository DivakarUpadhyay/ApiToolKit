using ApiToolKit.Domain.Entities.Analytics;
using ApiToolKit.Domain.Interfaces.Repositories.Analytics;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Analytics;

public class AnalyticsAuthRepository : IAnalyticsAuthRepository
{
    private readonly DapperContext _context;

    public AnalyticsAuthRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<AnalyticsConfig?> GetConfigAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<AnalyticsConfig>(
            "sp_Analytics_GetActiveConfig",
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<AnalyticsToken?> GetTokenByPropertyIdAsync(string propertyId)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<AnalyticsToken>(
            "sp_Analytics_GetTokenByPropertyId",
            new { PropertyId = propertyId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task SaveTokenAsync(AnalyticsToken token)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_Analytics_SaveToken",
            new
            {
                token.PropertyId,
                token.AccessToken,
                token.RefreshToken,
                token.ExpiresAt,
                token.Scope
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task UpdateTokenAsync(AnalyticsToken token)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_Analytics_UpdateToken",
            new
            {
                token.Id,
                token.AccessToken,
                token.RefreshToken,
                token.ExpiresAt,
                token.Scope,
                token.UpdatedAt
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task SaveApiLogAsync(AnalyticsApiLog log)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_Analytics_SaveApiLog",
            new
            {
                log.PropertyId,
                log.ReportType,
                log.RequestBody,
                log.ResponseBody,
                log.IsSuccess,
                log.StatusCode
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
