using ApiToolKit.Domain.Entities.Shopee;
using ApiToolKit.Domain.Interfaces.Repositories.Shopee;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Shopee;

public class ShopeeAuthRepository : IShopeeAuthRepository
{
    private readonly DapperContext _context;

    public ShopeeAuthRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<ShopeeConfig?> GetConfigAsync()
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ShopeeConfig>(
            "sp_Shopee_GetConfig",
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<ShopeeToken?> GetTokenByShopIdAsync(long shopId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ShopeeToken>(
            "sp_Shopee_GetTokenByShopId",
            new { ShopId = shopId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveTokenAsync(ShopeeToken token)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Shopee_SaveToken",
            new
            {
                token.ShopId,
                token.AccessToken,
                token.RefreshToken,
                token.ExpireIn,
                token.RefreshTokenExpireIn
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> UpdateTokenAsync(ShopeeToken token)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Shopee_UpdateToken",
            new
            {
                token.ShopId,
                token.AccessToken,
                token.RefreshToken,
                token.ExpireIn,
                token.RefreshTokenExpireIn
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveApiLogAsync(ShopeeApiLog log)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Shopee_SaveApiLog",
            new
            {
                log.ShopId,
                log.ApiName,
                log.RequestBody,
                log.ResponseBody,
                log.IsSuccess,
                log.StatusCode
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
