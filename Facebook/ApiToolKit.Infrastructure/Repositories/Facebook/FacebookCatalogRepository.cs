using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Facebook;

public class FacebookCatalogRepository : IFacebookCatalogRepository
{
    private readonly DapperContext _context;

    public FacebookCatalogRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveCatalogAsync(FacebookCatalog catalog)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Catalog_Save",
            new
            {
                catalog.CatalogId,
                catalog.BusinessId,
                CatalogName = catalog.Name,
                catalog.VerticalType,
                catalog.ProductCount
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<FacebookCatalog?> GetCatalogByIdAsync(string catalogId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FacebookCatalog>(
            "sp_Facebook_Catalog_GetById",
            new { CatalogId = catalogId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<FacebookCatalog>> GetCatalogsByBusinessIdAsync(string businessId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryAsync<FacebookCatalog>(
            "sp_Facebook_Catalog_GetByBusinessId",
            new { BusinessId = businessId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> DeleteCatalogAsync(string catalogId)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Catalog_Delete",
            new { CatalogId = catalogId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveApiLogAsync(FacebookApiLog log)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Catalog_SaveApiLog",
            new
            {
                log.UserId,
                log.PageId,
                log.ApiName,
                log.RequestBody,
                log.ResponseBody,
                log.IsSuccess,
                log.StatusCode
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
