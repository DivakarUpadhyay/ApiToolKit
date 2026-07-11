using ApiToolKit.Application.Interfaces.Services.Search;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace ApiToolKit.Infrastructure.External.Search;

public class SearchApiClient : ISearchApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public SearchApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["Google:Search:ApiKey"]
            ?? throw new InvalidOperationException("Google:Search:ApiKey missing in configuration.");
    }

    public async Task<T?> GetAsync<T>(string url, Dictionary<string, string> queryParams)
    {
        var sb = new StringBuilder(url);
        sb.Append('?');
        sb.Append("key=").Append(Uri.EscapeDataString(_apiKey));

        foreach (var kv in queryParams)
        {
            sb.Append('&');
            sb.Append(Uri.EscapeDataString(kv.Key));
            sb.Append('=');
            sb.Append(Uri.EscapeDataString(kv.Value));
        }

        var response = await _httpClient.GetAsync(sb.ToString());
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }
}
