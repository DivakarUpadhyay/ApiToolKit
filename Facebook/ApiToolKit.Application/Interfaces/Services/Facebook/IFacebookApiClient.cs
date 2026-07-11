namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookApiClient
{
    Task<T?> GetAsync<T>(string path, Dictionary<string, string>? queryParams = null, string? accessToken = null);
    Task<T?> PostAsync<T>(string path, object? body = null, string? accessToken = null);
    Task<T?> PostFormAsync<T>(string path, Dictionary<string, string> formData, string? accessToken = null);
    Task<T?> DeleteAsync<T>(string path, string? accessToken = null);
    Task<T?> PostMultipartAsync<T>(string path, MultipartFormDataContent content, string? accessToken = null);
}
