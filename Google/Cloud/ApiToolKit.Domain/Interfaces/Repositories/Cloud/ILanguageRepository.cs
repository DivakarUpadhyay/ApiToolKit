using ApiToolKit.Domain.Entities.Cloud;

namespace ApiToolKit.Domain.Interfaces.Repositories.Cloud;

public interface ILanguageRepository
{
    Task SaveAnalysisAsync(LanguageAnalysis analysis);
    Task<IEnumerable<LanguageAnalysis>> GetByProjectIdAsync(string projectId);
}
