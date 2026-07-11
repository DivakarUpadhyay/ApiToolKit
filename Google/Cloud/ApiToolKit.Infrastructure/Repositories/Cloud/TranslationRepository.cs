using ApiToolKit.Domain.Entities.Cloud;
using ApiToolKit.Domain.Interfaces.Repositories.Cloud;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Cloud;

public class TranslationRepository : ITranslationRepository
{
    private readonly DapperContext _context;

    public TranslationRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveTranslationAsync(TranslationResult translation)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_Cloud_SaveTranslationResult",
            new
            {
                translation.RequestId,
                translation.ProjectId,
                translation.SourceText,
                translation.TranslatedText,
                translation.SourceLanguage,
                translation.TargetLanguage,
                translation.GlossaryId,
                translation.Model,
                translation.CreatedAt
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<TranslationResult>> GetByProjectIdAsync(string projectId)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<TranslationResult>(
            "sp_Cloud_GetTranslationResultsByProjectId",
            new { ProjectId = projectId },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
