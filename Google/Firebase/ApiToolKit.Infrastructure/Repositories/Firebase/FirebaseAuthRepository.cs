using ApiToolKit.Domain.Entities.Firebase;
using ApiToolKit.Domain.Interfaces.Repositories.Firebase;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Firebase;

public class FirebaseAuthRepository : IFirebaseAuthRepository
{
    private readonly DapperContext _context;

    public FirebaseAuthRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<FirebaseConfig?> GetConfigAsync()
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FirebaseConfig>(
            "sp_Firebase_GetConfig",
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<FirebaseToken?> GetTokenByProjectIdAsync(string projectId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FirebaseToken>(
            "sp_Firebase_GetTokenByProjectId",
            new { ProjectId = projectId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveTokenAsync(FirebaseToken token)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Firebase_SaveToken",
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

    public async Task<int> UpdateTokenAsync(FirebaseToken token)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Firebase_UpdateToken",
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

    public async Task<int> SaveApiLogAsync(FirebaseApiLog log)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Firebase_SaveApiLog",
            new
            {
                log.ProjectId,
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
