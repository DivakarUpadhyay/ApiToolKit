namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IWhatsAppApiClient
{
    Task<T?> GetAsync<T>(string path, string accessToken, Dictionary<string, string>? queryParams = null);
    Task<T?> PostAsync<T>(string path, string accessToken, object? body = null);
    Task<T?> DeleteAsync<T>(string path, string accessToken);
    Task<T?> PostMultipartAsync<T>(string path, string accessToken, MultipartFormDataContent content);
}
