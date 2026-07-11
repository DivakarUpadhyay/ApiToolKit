using ApiToolKit.Application.Interfaces.Services.Maps;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace ApiToolKit.Infrastructure.External.Maps;

public class MapsApiClient : IMapsApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public MapsApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["Google:Maps:ApiKey"]
            ?? throw new InvalidOperationException("Google:Maps:ApiKey missing in configuration.");
    }

    public async Task<T?> GetAsync<T>(string url, Dictionary<string, string> queryParams)
    {
        var sb = new StringBuilder(url);
        sb.Append(url.Contains('?') ? '&' : '?');

        foreach (var kv in queryParams)
            sb.Append($"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value)}&");

        sb.Append($"key={Uri.EscapeDataString(_apiKey)}");

        var requestUrl = sb.ToString();
        var response = await _httpClient.GetAsync(requestUrl);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }
}
