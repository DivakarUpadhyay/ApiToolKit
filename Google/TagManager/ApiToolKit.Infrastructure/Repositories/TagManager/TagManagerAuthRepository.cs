using ApiToolKit.Domain.Entities.TagManager;
using ApiToolKit.Domain.Interfaces.Repositories.TagManager;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.TagManager;

public class TagManagerAuthRepository : ITagManagerAuthRepository
{
    private readonly DapperContext _context;

    public TagManagerAuthRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<TagManagerToken?> GetTokenAsync(string accountKey)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<TagManagerToken>(
            "sp_TagManager_GetToken",
            new { AccountKey = accountKey },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task SaveTokenAsync(TagManagerToken token)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_TagManager_SaveToken",
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

    public async Task SaveApiLogAsync(TagManagerApiLog log)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_TagManager_SaveApiLog",
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
