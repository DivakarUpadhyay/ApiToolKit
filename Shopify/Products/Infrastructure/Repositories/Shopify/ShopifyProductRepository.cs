using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Shopify;

public class ShopifyProductRepository : IShopifyProductRepository
{
    private readonly DapperContext _context;

    public ShopifyProductRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> UpsertProductAsync(ShopifyProduct product)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Shopify_Products_Upsert",
            new
            {
                product.ShopDomain,
                product.ShopifyProductId,
                product.Title,
                product.BodyHtml,
                product.Vendor,
                product.ProductType,
                product.Status,
                product.Tags,
                product.PublishedAt,
                product.CreatedAt,
                product.UpdatedAt
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveApiLogAsync(ShopifyApiLog log)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
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
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
