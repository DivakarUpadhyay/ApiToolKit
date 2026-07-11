using ApiToolKit.Domain.Entities.Identity;
using ApiToolKit.Domain.Interfaces.Repositories.Identity;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Identity;

public class IdentityAuthRepository : IIdentityAuthRepository
{
    private readonly DapperContext _context;

    public IdentityAuthRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IdentityConfig?> GetConfigAsync()
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<IdentityConfig>(
            "sp_Identity_GetConfig",
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IdentityToken?> GetTokenByUserIdAsync(string userId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<IdentityToken>(
            "sp_Identity_GetTokenByUserId",
            new { UserId = userId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveTokenAsync(IdentityToken token)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Identity_SaveToken",
            new
            {
                token.UserId,
                token.AccessToken,
                token.RefreshToken,
                token.IdToken,
                token.ExpiresAt,
                token.Scope,
                token.TokenType
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> UpdateTokenAsync(IdentityToken token)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Identity_UpdateToken",
            new
            {
                token.UserId,
                token.AccessToken,
                token.RefreshToken,
                token.IdToken,
                token.ExpiresAt,
                token.Scope,
                token.TokenType
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveApiLogAsync(IdentityApiLog log)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Identity_SaveApiLog",
            new
            {
                log.UserId,
                log.ApiName,
                log.RequestBody,
                log.ResponseBody,
                log.IsSuccess,
                log.StatusCode
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
