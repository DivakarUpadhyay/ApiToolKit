namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface IWorkspaceApiClient
{
    string BuildAuthorizationUrl(string[] scopes, string state = "");
    Task<T?> GetAsync<T>(string url, string accessToken);
    Task<T?> PostAsync<T>(string url, object? body, string accessToken);
    Task<T?> PatchAsync<T>(string url, object? body, string accessToken);
    Task<T?> PutAsync<T>(string url, object? body, string accessToken);
    Task DeleteAsync(string url, string accessToken);
    Task<T?> ExchangeCodeAsync<T>(string code, string redirectUri);
    Task<T?> RefreshTokenAsync<T>(string refreshToken);
}
