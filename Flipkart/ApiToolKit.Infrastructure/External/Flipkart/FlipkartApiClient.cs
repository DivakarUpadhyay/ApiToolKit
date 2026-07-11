using ApiToolKit.Application.Interfaces.Services.Flipkart;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace ApiToolKit.Infrastructure.External.Flipkart;

public class FlipkartApiClient : IFlipkartApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public FlipkartApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseUrl = configuration["Flipkart:BaseUrl"] ?? "https://api.flipkart.net/sellers";
        var accessToken = configuration["Flipkart:AccessToken"] ?? string.Empty;
        if (!string.IsNullOrEmpty(accessToken))
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
    }

    public async Task<T?> GetAsync<T>(string path, string? queryString = null)
    {
        try
        {
            var url = string.IsNullOrEmpty(queryString) ? $"{_baseUrl}{path}" : $"{_baseUrl}{path}?{queryString}";
            var response = await _httpClient.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
        catch
        {
            return default;
        }
    }

    public async Task<T?> PostAsync<T>(string path, object? body = null)
    {
        try
        {
            var url = $"{_baseUrl}{path}";
            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseString);
        }
        catch
        {
            return default;
        }
    }

    public async Task<byte[]?> GetBytesAsync(string path)
    {
        try
        {
            var url = $"{_baseUrl}{path}";
            var response = await _httpClient.GetAsync(url);
            return await response.Content.ReadAsByteArrayAsync();
        }
        catch
        {
            return default;
        }
    }
}
