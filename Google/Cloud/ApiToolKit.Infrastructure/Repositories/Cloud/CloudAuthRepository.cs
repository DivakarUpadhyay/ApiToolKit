using ApiToolKit.Domain.Entities.Cloud;
using ApiToolKit.Domain.Interfaces.Repositories.Cloud;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Cloud;

public class CloudAuthRepository : ICloudAuthRepository
{
    private readonly DapperContext _context;

    public CloudAuthRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<CloudConfig?> GetConfigAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<CloudConfig>(
            "sp_Cloud_GetConfig",
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<CloudToken?> GetTokenByProjectIdAsync(string projectId)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<CloudToken>(
            "sp_Cloud_GetTokenByProjectId",
            new { ProjectId = projectId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task SaveTokenAsync(CloudToken token)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_Cloud_SaveToken",
            new
            {
                token.ProjectId,
                token.AccessToken,
                token.RefreshToken,
                token.ExpiresAt,
                token.Scope,
                token.CreatedAt,
                token.UpdatedAt
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task UpdateTokenAsync(CloudToken token)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_Cloud_UpdateToken",
            new
            {
                token.Id,
                token.ProjectId,
                token.AccessToken,
                token.RefreshToken,
                token.ExpiresAt,
                token.Scope,
                token.UpdatedAt
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task SaveApiLogAsync(CloudApiLog log)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_Cloud_SaveApiLog",
            new
            {
                log.ProjectId,
                log.Product,
                log.ApiName,
                log.RequestBody,
                log.ResponseBody,
                log.IsSuccess,
                log.StatusCode,
                log.CreatedAt
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
