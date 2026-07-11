using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Shopify;

public class ShopifyStorefrontRepository : IShopifyStorefrontRepository
{
    private readonly DapperContext _context;
    public ShopifyStorefrontRepository(DapperContext context) { _context = context; }

    public async Task<ShopifyStorefrontConfig?> GetConfigAsync(string shopDomain)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ShopifyStorefrontConfig>(
            "sp_Shopify_Storefront_GetConfig",
            new { ShopDomain = shopDomain },
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
