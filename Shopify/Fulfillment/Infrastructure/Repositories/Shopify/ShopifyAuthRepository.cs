using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Shopify;

public class ShopifyAuthRepository : IShopifyAuthRepository
{
    private readonly DapperContext _context;

    public ShopifyAuthRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<ShopifyConfig?> GetConfigAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<ShopifyConfig>(
            "sp_Shopify_Auth_GetConfig",
            commandType: System.Data.CommandType.StoredProcedure
        );
    }

    public async Task<ShopifyToken?> GetTokenByShopAsync(string shopDomain)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<ShopifyToken>(
            "sp_Shopify_Auth_GetTokenByShop",
            new { ShopDomain = shopDomain },
            commandType: System.Data.CommandType.StoredProcedure
        );
    }

    public async Task SaveApiLogAsync(ShopifyApiLog log)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_Shopify_Auth_SaveApiLog",
            new
            {
                log.ShopDomain,
                log.ApiName,
                log.RequestBody,
                log.ResponseBody,
                log.IsSuccess,
                log.StatusCode,
                log.CreatedAt
            },
            commandType: System.Data.CommandType.StoredProcedure
        );
    }
}
