using ApiToolKit.Domain.Entities.SearchConsole;
using ApiToolKit.Domain.Interfaces.Repositories.SearchConsole;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.SearchConsole;

public class SearchConsoleAuthRepository : ISearchConsoleAuthRepository
{
    private readonly DapperContext _context;

    public SearchConsoleAuthRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<SearchConsoleToken?> GetTokenAsync(string accountKey)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<SearchConsoleToken>(
            "sp_SearchConsole_GetToken",
            new { AccountKey = accountKey },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task SaveTokenAsync(SearchConsoleToken token)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_SearchConsole_SaveToken",
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

    public async Task SaveApiLogAsync(SearchConsoleApiLog log)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_SearchConsole_SaveApiLog",
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
