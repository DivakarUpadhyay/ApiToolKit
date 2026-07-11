using ApiToolKit.Domain.Entities.Cloud;
using ApiToolKit.Domain.Interfaces.Repositories.Cloud;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Cloud;

public class SpeechRepository : ISpeechRepository
{
    private readonly DapperContext _context;

    public SpeechRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveRecognitionAsync(SpeechRecognition recognition)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_Cloud_SaveSpeechRecognition",
            new
            {
                recognition.RequestId,
                recognition.ProjectId,
                recognition.AudioSource,
                recognition.LanguageCode,
                recognition.Transcript,
                recognition.Confidence,
                recognition.WordCount,
                recognition.Duration,
                recognition.CreatedAt
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<SpeechRecognition>> GetByProjectIdAsync(string projectId)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<SpeechRecognition>(
            "sp_Cloud_GetSpeechRecognitionsByProjectId",
            new { ProjectId = projectId },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
