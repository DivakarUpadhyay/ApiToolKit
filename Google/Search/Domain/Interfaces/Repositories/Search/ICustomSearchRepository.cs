using ApiToolKit.Domain.Entities.Search;

namespace ApiToolKit.Domain.Interfaces.Repositories.Search;

public interface ICustomSearchRepository
{
    Task SaveSearchResultAsync(CustomSearchResult result);
}
