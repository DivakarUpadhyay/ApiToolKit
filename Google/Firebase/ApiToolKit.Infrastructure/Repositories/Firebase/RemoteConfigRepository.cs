using ApiToolKit.Domain.Entities.Firebase;
using ApiToolKit.Domain.Interfaces.Repositories.Firebase;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Firebase;

public class RemoteConfigRepository : IRemoteConfigRepository
{
    private readonly DapperContext _context;

    public RemoteConfigRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveTemplateAsync(RemoteConfigTemplate template)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Firebase_SaveRemoteConfig",
            new
            {
                template.ProjectId,
                template.ETag,
                template.Version,
                template.UpdateTime,
                template.UpdateOrigin,
                template.UpdateType,
                template.UpdateUser,
                template.Description,
                template.Parameters,
                template.Conditions
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<RemoteConfigTemplate?> GetLatestByProjectIdAsync(string projectId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RemoteConfigTemplate>(
            "sp_Firebase_GetLatestRemoteConfig",
            new { ProjectId = projectId },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
