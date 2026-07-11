using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace ApiToolKit.Infrastructure.External.Shopify;

public class ShopifyApiClient : IShopifyApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IShopifyAuthRepository _authRepository;
    private readonly string _apiVersion;

    public ShopifyApiClient(HttpClient httpClient, IShopifyAuthRepository authRepository, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _authRepository = authRepository;
        _apiVersion = configuration["Shopify:ApiVersion"] ?? "2024-10";
    }

    private async Task<string> BuildUrlAsync(string shopDomain, string endpoint)
    {
        return $"https://{shopDomain}/admin/api/{_apiVersion}/{endpoint}";
    }

    private string BuildUrlWithParams(string baseUrl, Dictionary<string, string>? queryParams)
    {
        if (queryParams == null || queryParams.Count == 0)
            return baseUrl;

        var query = string.Join("&", queryParams.Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value)}"));
        return $"{baseUrl}?{query}";
    }

    private async Task<string> GetAccessTokenAsync(string shopDomain)
    {
        var token = await _authRepository.GetTokenByShopAsync(shopDomain);
        return token?.AccessToken ?? throw new InvalidOperationException($"No access token found for shop: {shopDomain}");
    }

    public async Task<string> GetAsync(string shopDomain, string endpoint, Dictionary<string, string>? queryParams = null)
    {
        var accessToken = await GetAccessTokenAsync(shopDomain);
        var baseUrl = await BuildUrlAsync(shopDomain, endpoint);
        var url = BuildUrlWithParams(baseUrl, queryParams);

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("X-Shopify-Access-Token", accessToken);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> PostAsync(string shopDomain, string endpoint, object? body = null)
    {
        var accessToken = await GetAccessTokenAsync(shopDomain);
        var url = await BuildUrlAsync(shopDomain, endpoint);

        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Add("X-Shopify-Access-Token", accessToken);

        if (body != null)
        {
            var json = JsonConvert.SerializeObject(body);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }
        else
        {
            request.Content = new StringContent("{}", Encoding.UTF8, "application/json");
        }

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> PutAsync(string shopDomain, string endpoint, object? body = null)
    {
        var accessToken = await GetAccessTokenAsync(shopDomain);
        var url = await BuildUrlAsync(shopDomain, endpoint);

        var request = new HttpRequestMessage(HttpMethod.Put, url);
        request.Headers.Add("X-Shopify-Access-Token", accessToken);

        if (body != null)
        {
            var json = JsonConvert.SerializeObject(body);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> DeleteAsync(string shopDomain, string endpoint)
    {
        var accessToken = await GetAccessTokenAsync(shopDomain);
        var url = await BuildUrlAsync(shopDomain, endpoint);

        var request = new HttpRequestMessage(HttpMethod.Delete, url);
        request.Headers.Add("X-Shopify-Access-Token", accessToken);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
