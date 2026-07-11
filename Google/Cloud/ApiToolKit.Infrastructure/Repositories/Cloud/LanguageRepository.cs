using ApiToolKit.Domain.Entities.Cloud;
using ApiToolKit.Domain.Interfaces.Repositories.Cloud;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Cloud;

public class LanguageRepository : ILanguageRepository
{
    private readonly DapperContext _context;

    public LanguageRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveAnalysisAsync(LanguageAnalysis analysis)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_Cloud_SaveLanguageAnalysis",
            new
            {
                analysis.RequestId,
                analysis.ProjectId,
                analysis.Text,
                analysis.AnalysisType,
                analysis.ResultData,
                analysis.LanguageCode,
                analysis.Confidence,
                analysis.CreatedAt
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<LanguageAnalysis>> GetByProjectIdAsync(string projectId)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<LanguageAnalysis>(
            "sp_Cloud_GetLanguageAnalysesByProjectId",
            new { ProjectId = projectId },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
