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
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ShopifyConfig>(
            "sp_Shopify_Auth_GetConfig",
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<ShopifyToken?> GetTokenByShopAsync(string shopDomain)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ShopifyToken>(
            "sp_Shopify_Auth_GetTokenByShop",
            new { ShopDomain = shopDomain },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveTokenAsync(ShopifyToken token)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Shopify_Auth_SaveToken",
            new { token.ShopDomain, token.AccessToken, token.Scope, token.TokenType, token.CreatedAt, token.UpdatedAt },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> UpdateTokenAsync(ShopifyToken token)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Shopify_Auth_UpdateToken",
            new { token.ShopDomain, token.AccessToken, token.Scope, token.UpdatedAt },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveApiLogAsync(ShopifyApiLog log)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Shopify_Auth_SaveApiLog",
            new { log.ShopDomain, log.ApiName, log.RequestBody, log.ResponseBody, log.IsSuccess, log.StatusCode, log.CreatedAt },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
