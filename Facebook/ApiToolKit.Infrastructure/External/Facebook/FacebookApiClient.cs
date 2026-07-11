using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace ApiToolKit.Infrastructure.External.Facebook;

public class FacebookApiClient : IFacebookApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public FacebookApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseUrl = configuration["Facebook:BaseUrl"] ?? "https://graph.facebook.com/v21.0";
        _ = configuration["Facebook:AppId"] ?? throw new InvalidOperationException("Facebook:AppId missing");
        _ = configuration["Facebook:AppSecret"] ?? throw new InvalidOperationException("Facebook:AppSecret missing");
    }

    public async Task<T?> GetAsync<T>(string path, Dictionary<string, string>? queryParams = null, string? accessToken = null)
    {
        var hasQuery = path.Contains('?');
        var sb = new StringBuilder($"{_baseUrl}{path}");
        var sep = hasQuery ? '&' : '?';
        if (queryParams != null)
            foreach (var kv in queryParams)
            {
                sb.Append($"{sep}{kv.Key}={Uri.EscapeDataString(kv.Value)}");
                sep = '&';
            }
        if (!string.IsNullOrEmpty(accessToken))
            sb.Append($"{sep}access_token={Uri.EscapeDataString(accessToken)}");

        var response = await _httpClient.GetAsync(sb.ToString());
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<T>(content);
        if (result is Newtonsoft.Json.Linq.JObject jObj && jObj["error"] != null)
            throw new Exception(jObj["error"]?["message"]?.ToString() ?? "Facebook API error");
        return result;
    }

    public async Task<T?> PostAsync<T>(string path, object? body = null, string? accessToken = null)
    {
        var url = $"{_baseUrl}{path}";
        if (!string.IsNullOrEmpty(accessToken))
            url += $"?access_token={accessToken}";

        var json = body != null ? JsonConvert.SerializeObject(body) : "{}";
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<T>(responseString);
        if (result is Newtonsoft.Json.Linq.JObject jObj && jObj["error"] != null)
            throw new Exception(jObj["error"]?["message"]?.ToString() ?? "Facebook API error");
        return result;
    }

    public async Task<T?> PostFormAsync<T>(string path, Dictionary<string, string> formData, string? accessToken = null)
    {
        var url = $"{_baseUrl}{path}";
        if (!string.IsNullOrEmpty(accessToken))
            formData["access_token"] = accessToken;

        var content = new FormUrlEncodedContent(formData);
        var response = await _httpClient.PostAsync(url, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<T>(responseString);
        if (result is Newtonsoft.Json.Linq.JObject jObj && jObj["error"] != null)
            throw new Exception(jObj["error"]?["message"]?.ToString() ?? "Facebook API error");
        return result;
    }

    public async Task<T?> DeleteAsync<T>(string path, string? accessToken = null)
    {
        var url = $"{_baseUrl}{path}";
        if (!string.IsNullOrEmpty(accessToken))
            url += $"?access_token={accessToken}";

        var response = await _httpClient.DeleteAsync(url);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<T>(responseString);
        if (result is Newtonsoft.Json.Linq.JObject jObj && jObj["error"] != null)
            throw new Exception(jObj["error"]?["message"]?.ToString() ?? "Facebook API error");
        return result;
    }

    public async Task<T?> PostMultipartAsync<T>(string path, MultipartFormDataContent content, string? accessToken = null)
    {
        var url = $"{_baseUrl}{path}";
        if (!string.IsNullOrEmpty(accessToken))
            url += $"?access_token={accessToken}";

        var response = await _httpClient.PostAsync(url, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<T>(responseString);
        if (result is Newtonsoft.Json.Linq.JObject jObj && jObj["error"] != null)
            throw new Exception(jObj["error"]?["message"]?.ToString() ?? "Facebook API error");
        return result;
    }
}
