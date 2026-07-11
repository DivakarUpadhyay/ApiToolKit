using ApiToolKit.Domain.Entities.Cloud;

namespace ApiToolKit.Domain.Interfaces.Repositories.Cloud;

public interface ITranslationRepository
{
    Task SaveTranslationAsync(TranslationResult translation);
    Task<IEnumerable<TranslationResult>> GetByProjectIdAsync(string projectId);
}
