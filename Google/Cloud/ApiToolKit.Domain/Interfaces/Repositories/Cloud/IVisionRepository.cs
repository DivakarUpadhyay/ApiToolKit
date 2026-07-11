using ApiToolKit.Domain.Entities.Cloud;

namespace ApiToolKit.Domain.Interfaces.Repositories.Cloud;

public interface IVisionRepository
{
    Task SaveAnnotationAsync(VisionAnnotation annotation);
    Task<IEnumerable<VisionAnnotation>> GetAnnotationsByProjectIdAsync(string projectId);
}
