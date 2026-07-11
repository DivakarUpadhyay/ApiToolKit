using ApiToolKit.Domain.Entities.Search;

namespace ApiToolKit.Domain.Interfaces.Repositories.Search;

public interface IKnowledgeGraphRepository
{
    Task SaveSearchResultAsync(KnowledgeGraphResult result);
}
