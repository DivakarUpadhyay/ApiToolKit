using ApiToolKit.Domain.Entities.Search;
using ApiToolKit.Domain.Interfaces.Repositories.Search;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Search;

public class KnowledgeGraphRepository : IKnowledgeGraphRepository
{
    private readonly DapperContext _context;

    public KnowledgeGraphRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveSearchResultAsync(KnowledgeGraphResult result)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_Search_SaveKnowledgeGraphResult",
            new
            {
                result.Query,
                result.Types,
                result.ResultsJson
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
