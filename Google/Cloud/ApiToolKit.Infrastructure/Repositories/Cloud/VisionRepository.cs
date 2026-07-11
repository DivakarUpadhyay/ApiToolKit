using ApiToolKit.Domain.Entities.Cloud;
using ApiToolKit.Domain.Interfaces.Repositories.Cloud;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Cloud;

public class VisionRepository : IVisionRepository
{
    private readonly DapperContext _context;

    public VisionRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveAnnotationAsync(VisionAnnotation annotation)
    {
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(
            "sp_Cloud_SaveVisionAnnotation",
            new
            {
                annotation.RequestId,
                annotation.ProjectId,
                annotation.ImageSource,
                annotation.AnnotationType,
                annotation.AnnotationData,
                annotation.Confidence,
                annotation.CreatedAt
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<VisionAnnotation>> GetAnnotationsByProjectIdAsync(string projectId)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<VisionAnnotation>(
            "sp_Cloud_GetVisionAnnotationsByProjectId",
            new { ProjectId = projectId },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
