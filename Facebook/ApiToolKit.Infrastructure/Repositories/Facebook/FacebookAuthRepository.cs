using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Facebook;

public class FacebookAuthRepository : IFacebookAuthRepository
{
    private readonly DapperContext _context;

    public FacebookAuthRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<FacebookConfig?> GetConfigAsync()
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FacebookConfig>(
            "sp_Facebook_Auth_GetConfig",
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<FacebookToken?> GetTokenByUserIdAsync(string userId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FacebookToken>(
            "sp_Facebook_Auth_GetTokenByUserId",
            new { UserId = userId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveTokenAsync(FacebookToken token)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Auth_SaveToken",
            new
            {
                token.UserId,
                token.PageId,
                token.AccessToken,
                token.TokenType,
                token.ExpiresAt,
                token.LongLivedToken,
                token.Scope
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> UpdateTokenAsync(FacebookToken token)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Auth_UpdateToken",
            new
            {
                token.UserId,
                token.PageId,
                token.AccessToken,
                token.TokenType,
                token.ExpiresAt,
                token.LongLivedToken,
                token.Scope
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveApiLogAsync(FacebookApiLog log)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Auth_SaveApiLog",
            new
            {
                log.UserId,
                log.PageId,
                log.ApiName,
                log.RequestBody,
                log.ResponseBody,
                log.IsSuccess,
                log.StatusCode
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
