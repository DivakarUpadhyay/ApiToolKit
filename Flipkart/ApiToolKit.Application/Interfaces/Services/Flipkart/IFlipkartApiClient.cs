namespace ApiToolKit.Application.Interfaces.Services.Flipkart;

public interface IFlipkartApiClient
{
    Task<T?> GetAsync<T>(string path, string? queryString = null);
    Task<T?> PostAsync<T>(string path, object? body = null);
    Task<byte[]?> GetBytesAsync(string path);
}
