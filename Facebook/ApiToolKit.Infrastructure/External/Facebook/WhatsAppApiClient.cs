using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace ApiToolKit.Infrastructure.External.Facebook;

public class WhatsAppApiClient : IWhatsAppApiClient
{
    public const string DefaultBaseUrl = "https://graph.facebook.com/v26.0";

    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public WhatsAppApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseUrl = (configuration["WhatsApp:BaseUrl"] ?? DefaultBaseUrl).TrimEnd('/');
    }

    // Empty token = unauthenticated call (e.g. the Embedded Signup code exchange, which uses client_secret).
    private void SetBearer(string accessToken) =>
        _httpClient.DefaultRequestHeaders.Authorization =
            string.IsNullOrEmpty(accessToken) ? null : new AuthenticationHeaderValue("Bearer", accessToken);

    private string BuildUrl(string path, Dictionary<string, string>? queryParams)
    {
        var url = $"{_baseUrl}{path}";
        if (queryParams == null || queryParams.Count == 0) return url;
        var sb = new StringBuilder(url + (url.Contains('?') ? "&" : "?"));
        foreach (var kv in queryParams)
            sb.Append($"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value)}&");
        return sb.ToString().TrimEnd('&');
    }

    private static StringContent JsonContent(object? body) =>
        new(body != null ? JsonConvert.SerializeObject(body, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }) : "{}",
            Encoding.UTF8, "application/json");

    // Meta returns { "error": { "message", "code", ... } } on failure; surface it instead of
    // deserializing it into an empty DTO and reporting success.
    private static async Task<T?> ReadAsync<T>(HttpResponseMessage response)
    {
        var text = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(text))
        {
            if (!response.IsSuccessStatusCode)
                throw new WhatsAppApiException($"WhatsApp API returned HTTP {(int)response.StatusCode}.", null, (int)response.StatusCode);
            return default;
        }

        JToken? token = null;
        try { token = JToken.Parse(text); } catch (JsonReaderException) { }

        if (token is JObject obj && obj["error"] is JObject err)
        {
            var message = err["error_user_msg"]?.ToString() ?? err["message"]?.ToString() ?? "WhatsApp API error";
            var details = err["error_data"]?["details"]?.ToString();
            if (!string.IsNullOrEmpty(details)) message += $" ({details})";
            throw new WhatsAppApiException(message, err["code"]?.Value<int?>(), (int)response.StatusCode);
        }

        if (!response.IsSuccessStatusCode)
            throw new WhatsAppApiException($"WhatsApp API returned HTTP {(int)response.StatusCode}: {text}", null, (int)response.StatusCode);

        return token != null ? token.ToObject<T>() : JsonConvert.DeserializeObject<T>(text);
    }

    public async Task<T?> GetAsync<T>(string path, string accessToken, Dictionary<string, string>? queryParams = null)
    {
        SetBearer(accessToken);
        return await ReadAsync<T>(await _httpClient.GetAsync(BuildUrl(path, queryParams)));
    }

    public async Task<T?> PostAsync<T>(string path, string accessToken, object? body = null, Dictionary<string, string>? queryParams = null)
    {
        SetBearer(accessToken);
        return await ReadAsync<T>(await _httpClient.PostAsync(BuildUrl(path, queryParams), JsonContent(body)));
    }

    public async Task<T?> DeleteAsync<T>(string path, string accessToken, Dictionary<string, string>? queryParams = null, object? body = null)
    {
        SetBearer(accessToken);
        using var request = new HttpRequestMessage(HttpMethod.Delete, BuildUrl(path, queryParams));
        if (body != null) request.Content = JsonContent(body);
        return await ReadAsync<T>(await _httpClient.SendAsync(request));
    }

    public async Task<T?> PostMultipartAsync<T>(string path, string accessToken, MultipartFormDataContent content)
    {
        SetBearer(accessToken);
        return await ReadAsync<T>(await _httpClient.PostAsync($"{_baseUrl}{path}", content));
    }

    public async Task<(byte[] Content, string ContentType)> DownloadAsync(string absoluteUrl, string accessToken)
    {
        SetBearer(accessToken);
        var response = await _httpClient.GetAsync(absoluteUrl);
        if (!response.IsSuccessStatusCode)
            throw new WhatsAppApiException($"Media download failed with HTTP {(int)response.StatusCode}.", null, (int)response.StatusCode);
        var bytes = await response.Content.ReadAsByteArrayAsync();
        var contentType = response.Content.Headers.ContentType?.MediaType ?? "application/octet-stream";
        return (bytes, contentType);
    }
}
