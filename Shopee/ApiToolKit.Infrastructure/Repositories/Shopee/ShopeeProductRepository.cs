using ApiToolKit.Domain.Interfaces.Repositories.Shopee;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Shopee;

public class ShopeeProductRepository : IShopeeProductRepository
{
    private readonly DapperContext _context;

    public ShopeeProductRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveProductAsync(long shopId, long itemId, string itemName, string description, long categoryId, string condition, string status, decimal originalPrice, int stock, string images, string variations, string attributes)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync("sp_Shopee_SaveProduct",
            new { ShopId = shopId, ItemId = itemId, ItemName = itemName, Description = description, CategoryId = categoryId, Condition = condition, Status = status, OriginalPrice = originalPrice, Stock = stock, Images = images, Variations = variations, Attributes = attributes },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<dynamic>> GetProductsByShopAsync(long shopId, int pageNo, int pageSize)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryAsync("sp_Shopee_GetProductsByShop",
            new { ShopId = shopId, PageNo = pageNo, PageSize = pageSize },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<dynamic?> GetProductByItemIdAsync(long shopId, long itemId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync("sp_Shopee_GetProductByItemId",
            new { ShopId = shopId, ItemId = itemId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> UpdateProductPriceAsync(long shopId, long itemId, long modelId, decimal price)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync("sp_Shopee_UpdateProductPrice",
            new { ShopId = shopId, ItemId = itemId, ModelId = modelId, Price = price },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> UpdateProductStockAsync(long shopId, long itemId, long modelId, int stock)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync("sp_Shopee_UpdateProductStock",
            new { ShopId = shopId, ItemId = itemId, ModelId = modelId, Stock = stock },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> DeleteProductAsync(long shopId, long itemId)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync("sp_Shopee_DeleteProduct",
            new { ShopId = shopId, ItemId = itemId },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
