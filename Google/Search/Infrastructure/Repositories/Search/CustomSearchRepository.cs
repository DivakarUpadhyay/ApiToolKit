using ApiToolKit.Domain.Entities.Search;
using ApiToolKit.Domain.Interfaces.Repositories.Search;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Search;

public class CustomSearchRepository : ICustomSearchRepository
{
    private readonly DapperContext _context;

    public CustomSearchRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveSearchResultAsync(CustomSearchResult result)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_Search_SaveCustomSearchResult",
            new
            {
                result.Query,
                result.SearchEngineId,
                result.TotalResults,
                result.SearchType,
                result.ResultsJson
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
