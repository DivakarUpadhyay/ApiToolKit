using ApiToolKit.Domain.Entities.SearchConsole;

namespace ApiToolKit.Domain.Interfaces.Repositories.SearchConsole;

public interface ISearchConsoleAuthRepository
{
    Task<SearchConsoleToken?> GetTokenAsync(string accountKey);
    Task SaveTokenAsync(SearchConsoleToken token);
    Task SaveApiLogAsync(SearchConsoleApiLog log);
}
