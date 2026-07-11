using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;

namespace Google.ApiTests.Helpers;

public class ApiTestClient : IDisposable
{
    private readonly HttpClient _client;
    public string BaseUrl { get; }

    public ApiTestClient(string baseUrl)
    {
        BaseUrl = baseUrl;
        _client = new HttpClient { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(30) };
    }

    public async Task<HttpResponseMessage> GetAsync(string path, Dictionary<string, string>? queryParams = null)
    {
        var url = path;
        if (queryParams != null)
            url += "?" + string.Join("&", queryParams.Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}"));
        return await _client.GetAsync(url);
    }

    public async Task<HttpResponseMessage> PostAsync(string path, object body)
    {
        var json = JsonConvert.SerializeObject(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        return await _client.PostAsync(path, content);
    }

    public async Task<bool> IsSwaggerAvailableAsync()
    {
        try
        {
            var response = await _client.GetAsync("/swagger/v1/swagger.json");
            return response.IsSuccessStatusCode;
        }
        catch { return false; }
    }

    public async Task<string?> GetSwaggerJsonAsync()
    {
        try
        {
            var response = await _client.GetAsync("/swagger/v1/swagger.json");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadAsStringAsync();
        }
        catch { return null; }
    }

    public void Dispose() => _client.Dispose();
}
