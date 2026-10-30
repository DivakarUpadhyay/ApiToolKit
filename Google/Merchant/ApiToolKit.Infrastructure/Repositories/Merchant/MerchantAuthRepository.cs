using ApiToolKit.Domain.Entities.Merchant;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Merchant;

public class MerchantAuthRepository : IMerchantAuthRepository
{
    private readonly DapperContext _context;

    public MerchantAuthRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<MerchantToken?> GetTokenAsync(string accountKey)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<MerchantToken>(
            "sp_Merchant_GetToken",
            new { AccountKey = accountKey },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task SaveTokenAsync(MerchantToken token)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_Merchant_SaveToken",
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

    public async Task SaveApiLogAsync(MerchantApiLog log)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_Merchant_SaveApiLog",
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
