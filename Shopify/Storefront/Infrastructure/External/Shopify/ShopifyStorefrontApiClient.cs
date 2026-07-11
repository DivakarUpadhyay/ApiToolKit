using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace ApiToolKit.Infrastructure.External.Shopify;

public class ShopifyStorefrontApiClient : IShopifyStorefrontApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiVersion;

    public ShopifyStorefrontApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiVersion = configuration["Shopify:ApiVersion"] ?? "2024-10";
    }

    private string BuildUrl(string shopDomain)
        => $"https://{shopDomain}/api/{_apiVersion}/graphql.json";

    private void SetAuthHeader(string? storefrontAccessToken)
    {
        _httpClient.DefaultRequestHeaders.Remove("X-Shopify-Storefront-Access-Token");
        if (!string.IsNullOrEmpty(storefrontAccessToken))
            _httpClient.DefaultRequestHeaders.Add("X-Shopify-Storefront-Access-Token", storefrontAccessToken);
    }

    public async Task<T?> QueryAsync<T>(string shopDomain, string query, object? variables = null, string? storefrontAccessToken = null)
    {
        SetAuthHeader(storefrontAccessToken);
        var url = BuildUrl(shopDomain);
        var payload = new { query, variables };
        var json = JsonConvert.SerializeObject(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }

    public async Task<T?> MutateAsync<T>(string shopDomain, string mutation, object? variables = null, string? storefrontAccessToken = null)
    {
        SetAuthHeader(storefrontAccessToken);
        var url = BuildUrl(shopDomain);
        var payload = new { query = mutation, variables };
        var json = JsonConvert.SerializeObject(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }
}
