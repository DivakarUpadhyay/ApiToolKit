using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

using ApiToolKit.Application.Interfaces.Services.Shopee;

namespace ApiToolKit.Infrastructure.External.Shopee;

public class ShopeeApiClient : IShopeeApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _partnerId;
    private readonly string _partnerKey;
    private readonly string _baseUrl;

    public ShopeeApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _partnerId = configuration["Shopee:PartnerId"] ?? throw new InvalidOperationException("Shopee:PartnerId missing");
        _partnerKey = configuration["Shopee:PartnerKey"] ?? throw new InvalidOperationException("Shopee:PartnerKey missing");
        _baseUrl = configuration["Shopee:BaseUrl"] ?? "https://partner.test-stable.shopeemobile.com";
    }

    private long GetTimestamp() => DateTimeOffset.UtcNow.ToUnixTimeSeconds();

    private string GenerateSign(string path, long timestamp, string? accessToken = null, long shopId = 0)
    {
        var baseString = shopId > 0
            ? $"{_partnerId}{path}{timestamp}{accessToken}{shopId}"
            : $"{_partnerId}{path}{timestamp}";

        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_partnerKey));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(baseString));
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }

    public string BuildAuthUrl(string redirectUrl)
    {
        var path = "/api/v2/shop/auth_partner";
        var timestamp = GetTimestamp();
        var sign = GenerateSign(path, timestamp);
        return $"{_baseUrl}{path}?partner_id={_partnerId}&timestamp={timestamp}&sign={sign}&redirect={Uri.EscapeDataString(redirectUrl)}";
    }

    public async Task<T?> PostAsync<T>(string path, object body, string? accessToken = null, long shopId = 0)
    {
        var timestamp = GetTimestamp();
        var sign = GenerateSign(path, timestamp, accessToken, shopId);

        var queryParams = $"partner_id={_partnerId}&timestamp={timestamp}&sign={sign}";
        if (accessToken != null) queryParams += $"&access_token={accessToken}";
        if (shopId > 0) queryParams += $"&shop_id={shopId}";

        var url = $"{_baseUrl}{path}?{queryParams}";
        var json = JsonConvert.SerializeObject(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }

    public async Task<T?> GetAsync<T>(string path, Dictionary<string, string> queryString, string? accessToken = null, long shopId = 0)
    {
        var timestamp = GetTimestamp();
        var sign = GenerateSign(path, timestamp, accessToken, shopId);

        var sb = new StringBuilder($"partner_id={_partnerId}&timestamp={timestamp}&sign={sign}");
        if (accessToken != null) sb.Append($"&access_token={accessToken}");
        if (shopId > 0) sb.Append($"&shop_id={shopId}");
        foreach (var kv in queryString)
            sb.Append($"&{kv.Key}={Uri.EscapeDataString(kv.Value)}");

        var url = $"{_baseUrl}{path}?{sb}";
        var response = await _httpClient.GetAsync(url);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }

    public async Task<T?> PostMultipartAsync<T>(string path, MultipartFormDataContent content, string? accessToken = null, long shopId = 0)
    {
        var timestamp = GetTimestamp();
        var sign = GenerateSign(path, timestamp, accessToken, shopId);

        var queryParams = $"partner_id={_partnerId}&timestamp={timestamp}&sign={sign}";
        if (accessToken != null) queryParams += $"&access_token={accessToken}";
        if (shopId > 0) queryParams += $"&shop_id={shopId}";

        var url = $"{_baseUrl}{path}?{queryParams}";
        var response = await _httpClient.PostAsync(url, content);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }
}
