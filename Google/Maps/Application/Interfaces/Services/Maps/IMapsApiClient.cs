namespace ApiToolKit.Application.Interfaces.Services.Maps;

public interface IMapsApiClient
{
    Task<T?> GetAsync<T>(string url, Dictionary<string, string> queryParams);
}
