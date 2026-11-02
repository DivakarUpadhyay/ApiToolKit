namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IWhatsAppApiClient
{
    Task<T?> GetAsync<T>(string path, string accessToken, Dictionary<string, string>? queryParams = null);
    Task<T?> PostAsync<T>(string path, string accessToken, object? body = null, Dictionary<string, string>? queryParams = null);
    Task<T?> DeleteAsync<T>(string path, string accessToken, Dictionary<string, string>? queryParams = null, object? body = null);
    Task<T?> PostMultipartAsync<T>(string path, string accessToken, MultipartFormDataContent content);

    /// <summary>Downloads binary content from an absolute URL (e.g. a WhatsApp media URL) using Bearer auth.</summary>
    Task<(byte[] Content, string ContentType)> DownloadAsync(string absoluteUrl, string accessToken);
}

/// <summary>Thrown when the Graph API returns an <c>error</c> object.</summary>
public class WhatsAppApiException : Exception
{
    public int? Code { get; }
    public int StatusCode { get; }

    public WhatsAppApiException(string message, int? code, int statusCode) : base(message)
    {
        Code = code;
        StatusCode = statusCode;
    }
}
