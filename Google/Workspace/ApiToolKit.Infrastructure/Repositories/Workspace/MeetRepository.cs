using ApiToolKit.Domain.Entities.Workspace.Meet;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Workspace;

public class MeetRepository : IMeetRepository
{
    private readonly DapperContext _context;

    public MeetRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveSpaceAsync(MeetSpace space)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_Workspace_Meet_SaveSpace",
            new
            {
                space.SpaceName,
                space.UserId,
                space.MeetingCode,
                space.MeetingUri,
                space.Title,
                space.ActiveConference,
                space.Config
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<List<MeetSpace>> GetSpacesByUserIdAsync(string userId)
    {
        using var conn = _context.CreateConnection();
        var result = await conn.QueryAsync<MeetSpace>(
            "sp_Workspace_Meet_GetSpacesByUserId",
            new { UserId = userId },
            commandType: System.Data.CommandType.StoredProcedure);
        return result.ToList();
    }
}
