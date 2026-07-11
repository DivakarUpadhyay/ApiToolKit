using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ApiToolKit.Infrastructure.External.Facebook;

public class ThreadsApiClient : IThreadsApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public ThreadsApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseUrl = configuration["Threads:BaseUrl"] ?? "https://graph.threads.net/v1.0";
    }

    private void SetBearer(string accessToken) =>
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

    private string BuildUrl(string path, Dictionary<string, string>? queryParams)
    {
        var url = $"{_baseUrl}{path}";
        if (queryParams == null || queryParams.Count == 0) return url;
        var sb = new StringBuilder(url + "?");
        foreach (var kv in queryParams) sb.Append($"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value)}&");
        return sb.ToString().TrimEnd('&');
    }

    public async Task<T?> GetAsync<T>(string path, string accessToken, Dictionary<string, string>? queryParams = null)
    {
        SetBearer(accessToken);
        var response = await _httpClient.GetAsync(BuildUrl(path, queryParams));
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }

    public async Task<T?> PostAsync<T>(string path, string accessToken, object? body = null)
    {
        SetBearer(accessToken);
        var json = body != null ? JsonConvert.SerializeObject(body) : "{}";
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_baseUrl}{path}", content);
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }

    public async Task<T?> DeleteAsync<T>(string path, string accessToken)
    {
        SetBearer(accessToken);
        var response = await _httpClient.DeleteAsync($"{_baseUrl}{path}");
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }
}
