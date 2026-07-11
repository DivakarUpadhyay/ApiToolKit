using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace ApiToolKit.Infrastructure.External.Shopify;

public class ShopifyApiClient : IShopifyApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiVersion;

    public ShopifyApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiVersion = configuration["Shopify:ApiVersion"] ?? "2024-10";
    }

    private string BuildUrl(string shopDomain, string endpoint)
        => $"https://{shopDomain}/admin/api/{_apiVersion}/{endpoint}";

    private void SetAuthHeader(string? accessToken)
    {
        _httpClient.DefaultRequestHeaders.Remove("X-Shopify-Access-Token");
        if (!string.IsNullOrEmpty(accessToken))
            _httpClient.DefaultRequestHeaders.Add("X-Shopify-Access-Token", accessToken);
    }

    public string BuildInstallUrl(string shopDomain, string apiKey, string scopes, string redirectUri, string state)
        => $"https://{shopDomain}/admin/oauth/authorize?client_id={apiKey}&scope={Uri.EscapeDataString(scopes)}&redirect_uri={Uri.EscapeDataString(redirectUri)}&state={state}";

    public async Task<T?> ExchangeCodeAsync<T>(string shopDomain, string apiKey, string apiSecret, string code)
    {
        var url = $"https://{shopDomain}/admin/oauth/access_token";
        var body = new { client_id = apiKey, client_secret = apiSecret, code };
        var json = JsonConvert.SerializeObject(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }

    public async Task<T?> GetAsync<T>(string shopDomain, string endpoint, Dictionary<string, string>? queryParams = null, string? accessToken = null)
    {
        SetAuthHeader(accessToken);
        var url = BuildUrl(shopDomain, endpoint);
        if (queryParams?.Count > 0)
        {
            var qs = string.Join("&", queryParams.Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value)}"));
            url += $"?{qs}";
        }
        var response = await _httpClient.GetAsync(url);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }

    public async Task<T?> PostAsync<T>(string shopDomain, string endpoint, object? body = null, string? accessToken = null)
    {
        SetAuthHeader(accessToken);
        var url = BuildUrl(shopDomain, endpoint);
        var json = body != null ? JsonConvert.SerializeObject(body) : "{}";
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }

    public async Task<T?> PutAsync<T>(string shopDomain, string endpoint, object? body = null, string? accessToken = null)
    {
        SetAuthHeader(accessToken);
        var url = BuildUrl(shopDomain, endpoint);
        var json = body != null ? JsonConvert.SerializeObject(body) : "{}";
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Put, url) { Content = content };
        var response = await _httpClient.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }

    public async Task<T?> DeleteAsync<T>(string shopDomain, string endpoint, string? accessToken = null)
    {
        SetAuthHeader(accessToken);
        var url = BuildUrl(shopDomain, endpoint);
        var response = await _httpClient.DeleteAsync(url);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }
}
