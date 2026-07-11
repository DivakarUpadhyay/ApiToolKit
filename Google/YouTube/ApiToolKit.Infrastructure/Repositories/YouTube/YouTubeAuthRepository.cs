using ApiToolKit.Domain.Entities.YouTube;
using ApiToolKit.Domain.Interfaces.Repositories.YouTube;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.YouTube;

public class YouTubeAuthRepository : IYouTubeAuthRepository
{
    private readonly DapperContext _context;

    public YouTubeAuthRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<YouTubeConfig?> GetConfigAsync()
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<YouTubeConfig>(
            "sp_YouTube_GetConfig",
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<YouTubeToken?> GetTokenByUserIdAsync(string userId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<YouTubeToken>(
            "sp_YouTube_GetTokenByUserId",
            new { UserId = userId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task SaveTokenAsync(YouTubeToken token)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_YouTube_SaveToken",
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

    public async Task UpdateTokenAsync(YouTubeToken token)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_YouTube_UpdateToken",
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

    public async Task SaveApiLogAsync(YouTubeApiLog log)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_YouTube_SaveApiLog",
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
