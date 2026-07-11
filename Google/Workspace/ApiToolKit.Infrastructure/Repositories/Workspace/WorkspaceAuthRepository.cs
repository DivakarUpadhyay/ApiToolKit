using ApiToolKit.Domain.Entities.Workspace;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Workspace;

public class WorkspaceAuthRepository : IWorkspaceAuthRepository
{
    private readonly DapperContext _context;

    public WorkspaceAuthRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<WorkspaceConfig?> GetConfigAsync()
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<WorkspaceConfig>(
            "sp_Workspace_GetConfig",
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<WorkspaceToken?> GetTokenByUserIdAsync(string userId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<WorkspaceToken>(
            "sp_Workspace_GetTokenByUserId",
            new { UserId = userId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task SaveTokenAsync(WorkspaceToken token)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_Workspace_SaveToken",
            new
            {
                token.UserId,
                token.AccessToken,
                token.RefreshToken,
                token.ExpiresAt,
                token.Scope
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task UpdateTokenAsync(WorkspaceToken token)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_Workspace_UpdateToken",
            new
            {
                token.UserId,
                token.AccessToken,
                token.RefreshToken,
                token.ExpiresAt,
                token.Scope
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task SaveApiLogAsync(WorkspaceApiLog log)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_Workspace_SaveApiLog",
            new
            {
                log.UserId,
                log.Product,
                log.ApiName,
                log.RequestBody,
                log.ResponseBody,
                log.IsSuccess,
                log.StatusCode
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
