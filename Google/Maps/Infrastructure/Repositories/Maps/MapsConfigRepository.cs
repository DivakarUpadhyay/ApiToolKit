using ApiToolKit.Domain.Entities.Maps;
using ApiToolKit.Domain.Interfaces.Repositories.Maps;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Maps;

public class MapsConfigRepository : IMapsConfigRepository
{
    private readonly DapperContext _context;

    public MapsConfigRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<MapsConfig?> GetConfigAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<MapsConfig>(
            "sp_Maps_GetActiveConfig",
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
