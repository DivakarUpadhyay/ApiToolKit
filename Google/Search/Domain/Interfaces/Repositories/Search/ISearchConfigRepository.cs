using ApiToolKit.Domain.Entities.Search;

namespace ApiToolKit.Domain.Interfaces.Repositories.Search;

public interface ISearchConfigRepository
{
    Task<SearchConfig?> GetConfigAsync();
}
