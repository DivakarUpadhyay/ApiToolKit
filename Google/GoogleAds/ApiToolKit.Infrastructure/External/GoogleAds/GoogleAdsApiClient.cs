using ApiToolKit.Application.DTOs.GoogleAds.Auth;
using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace ApiToolKit.Infrastructure.External.GoogleAds;

public class GoogleAdsApiClient : IGoogleAdsApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _redirectUri;
    private readonly string _developerToken;

    public GoogleAdsApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _clientId = configuration["Google:Ads:ClientId"] ?? throw new InvalidOperationException("Google:Ads:ClientId missing");
        _clientSecret = configuration["Google:Ads:ClientSecret"] ?? throw new InvalidOperationException("Google:Ads:ClientSecret missing");
        _developerToken = configuration["Google:Ads:DeveloperToken"] ?? throw new InvalidOperationException("Google:Ads:DeveloperToken missing");
        _redirectUri = configuration["Google:Ads:RedirectUri"] ?? throw new InvalidOperationException("Google:Ads:RedirectUri missing");
    }

    public string BuildAuthorizationUrl(string[] scopes)
    {
        var scope = Uri.EscapeDataString(string.Join(" ", scopes));
        var redirectUri = Uri.EscapeDataString(_redirectUri);
        return $"https://accounts.google.com/o/oauth2/v2/auth" +
               $"?client_id={_clientId}" +
               $"&redirect_uri={redirectUri}" +
               $"&response_type=code" +
               $"&scope={scope}" +
               $"&access_type=offline" +
               $"&prompt=consent";
    }

    public async Task<GoogleTokenResponse?> ExchangeCodeAsync(string code, string redirectUri)
    {
        var formData = new Dictionary<string, string>
        {
            ["code"] = code,
            ["client_id"] = _clientId,
            ["client_secret"] = _clientSecret,
            ["redirect_uri"] = redirectUri,
            ["grant_type"] = "authorization_code"
        };

        var content = new FormUrlEncodedContent(formData);
        var response = await _httpClient.PostAsync("https://oauth2.googleapis.com/token", content);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<GoogleTokenResponse>(responseString);
    }

    public async Task<GoogleTokenResponse?> RefreshTokenAsync(string refreshToken)
    {
        var formData = new Dictionary<string, string>
        {
            ["refresh_token"] = refreshToken,
            ["client_id"] = _clientId,
            ["client_secret"] = _clientSecret,
            ["grant_type"] = "refresh_token"
        };

        var content = new FormUrlEncodedContent(formData);
        var response = await _httpClient.PostAsync("https://oauth2.googleapis.com/token", content);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<GoogleTokenResponse>(responseString);
    }

    public async Task<T?> GetAsync<T>(string url, string accessToken, Dictionary<string, string>? queryParams = null)
    {
        var requestUrl = url;
        if (queryParams != null && queryParams.Count > 0)
        {
            var queryString = string.Join("&", queryParams.Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}"));
            requestUrl = $"{url}?{queryString}";
        }

        var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
        request.Headers.Add("Authorization", $"Bearer {accessToken}");
        request.Headers.Add("developer-token", _developerToken);

        var response = await _httpClient.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }

    public async Task<T?> PostAsync<T>(string url, object body, string accessToken, string? customerId = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Add("Authorization", $"Bearer {accessToken}");
        request.Headers.Add("developer-token", _developerToken);

        if (!string.IsNullOrEmpty(customerId))
            request.Headers.Add("login-customer-id", customerId);

        var json = JsonConvert.SerializeObject(body);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }

    public async Task<T?> SearchAsync<T>(string customerId, string query, string accessToken)
    {
        var url = $"https://googleads.googleapis.com/v18/customers/{customerId}/googleAds:search";
        var body = new { query, pageSize = 1000 };

        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Add("Authorization", $"Bearer {accessToken}");
        request.Headers.Add("developer-token", _developerToken);
        request.Headers.Add("login-customer-id", customerId);

        var json = JsonConvert.SerializeObject(body);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }

    public async Task<T?> MutateAsync<T>(string customerId, string resource, object operations, string accessToken)
    {
        var url = $"https://googleads.googleapis.com/v18/customers/{customerId}/{resource}:mutate";

        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Add("Authorization", $"Bearer {accessToken}");
        request.Headers.Add("developer-token", _developerToken);
        request.Headers.Add("login-customer-id", customerId);

        var json = JsonConvert.SerializeObject(operations);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }
}
