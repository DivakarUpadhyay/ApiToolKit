namespace ApiToolKit.Application.Interfaces.Services.Search;

public interface ISearchApiClient
{
    Task<T?> GetAsync<T>(string url, Dictionary<string, string> queryParams);
}
