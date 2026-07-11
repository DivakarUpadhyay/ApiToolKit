using ApiToolKit.Domain.Entities.Search;
using ApiToolKit.Domain.Interfaces.Repositories.Search;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Search;

public class SearchConfigRepository : ISearchConfigRepository
{
    private readonly DapperContext _context;

    public SearchConfigRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<SearchConfig?> GetConfigAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<SearchConfig>(
            "sp_Search_GetActiveConfig",
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
