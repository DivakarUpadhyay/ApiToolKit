using ApiToolKit.Domain.Entities.BigQuery;
using ApiToolKit.Domain.Interfaces.Repositories.BigQuery;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.BigQuery;

public class BigQueryAuthRepository : IBigQueryAuthRepository
{
    private readonly DapperContext _context;

    public BigQueryAuthRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<BigQueryConfig?> GetConfigAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<BigQueryConfig>(
            "sp_BigQuery_GetActiveConfig",
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<BigQueryToken?> GetTokenByProjectIdAsync(string projectId)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<BigQueryToken>(
            "sp_BigQuery_GetTokenByProjectId",
            new { ProjectId = projectId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task SaveTokenAsync(BigQueryToken token)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_BigQuery_SaveToken",
            new
            {
                token.ProjectId,
                token.AccessToken,
                token.RefreshToken,
                token.ExpiresAt,
                token.Scope
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task UpdateTokenAsync(BigQueryToken token)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_BigQuery_UpdateToken",
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

    public async Task SaveApiLogAsync(BigQueryApiLog log)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_BigQuery_SaveApiLog",
            new
            {
                log.ProjectId,
                log.ApiName,
                log.RequestBody,
                log.ResponseBody,
                log.IsSuccess,
                log.StatusCode
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
