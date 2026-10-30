using ApiToolKit.Domain.Entities.DisplayVideo;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.DisplayVideo;

public class DisplayVideoAuthRepository : IDisplayVideoAuthRepository
{
    private readonly DapperContext _context;

    public DisplayVideoAuthRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<DisplayVideoToken?> GetTokenAsync(string accountKey)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<DisplayVideoToken>(
            "sp_DisplayVideo_GetToken",
            new { AccountKey = accountKey },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task SaveTokenAsync(DisplayVideoToken token)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_DisplayVideo_SaveToken",
            new
            {
                token.AccountKey,
                token.AccessToken,
                token.RefreshToken,
                token.ExpiresAt,
                token.Scope
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task SaveApiLogAsync(DisplayVideoApiLog log)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_DisplayVideo_SaveApiLog",
            new
            {
                log.AccountKey,
                log.ApiName,
                log.HttpMethod,
                log.Url,
                log.RequestBody,
                log.ResponseBody,
                log.IsSuccess,
                log.StatusCode
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
