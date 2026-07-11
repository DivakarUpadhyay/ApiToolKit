using ApiToolKit.Domain.Entities.Workspace.Slides;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Workspace;

public class PresentationRepository : IPresentationRepository
{
    private readonly DapperContext _context;

    public PresentationRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SavePresentationAsync(PresentationInfo presentation)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_Workspace_Slides_SavePresentation",
            new
            {
                presentation.PresentationId,
                presentation.UserId,
                presentation.Title,
                presentation.PresentationUrl,
                presentation.RevisionId
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<List<PresentationInfo>> GetPresentationsByUserIdAsync(string userId)
    {
        using var conn = _context.CreateConnection();
        var result = await conn.QueryAsync<PresentationInfo>(
            "sp_Workspace_Slides_GetPresentationsByUserId",
            new { UserId = userId },
            commandType: System.Data.CommandType.StoredProcedure);
        return result.ToList();
    }
}
