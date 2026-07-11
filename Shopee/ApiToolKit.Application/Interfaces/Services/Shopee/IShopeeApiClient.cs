namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeeApiClient
{
    string BuildAuthUrl(string redirectUrl);
    Task<T?> PostAsync<T>(string path, object body, string? accessToken = null, long shopId = 0);
    Task<T?> GetAsync<T>(string path, Dictionary<string, string> queryString, string? accessToken = null, long shopId = 0);
    Task<T?> PostMultipartAsync<T>(string path, MultipartFormDataContent content, string? accessToken = null, long shopId = 0);
}
