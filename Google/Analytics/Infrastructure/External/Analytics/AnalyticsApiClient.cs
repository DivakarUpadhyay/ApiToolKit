using ApiToolKit.Application.DTOs.Analytics.Auth;
using ApiToolKit.Application.Interfaces.Services.Analytics;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace ApiToolKit.Infrastructure.External.Analytics;

public class AnalyticsApiClient : IAnalyticsApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _redirectUri;
    private const string OAuthScope = "https://www.googleapis.com/auth/analytics.readonly";
    private const string TokenEndpoint = "https://oauth2.googleapis.com/token";
    private const string AuthEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";

    public AnalyticsApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _clientId = configuration["Google:Analytics:ClientId"]
            ?? throw new InvalidOperationException("Google:Analytics:ClientId missing in configuration.");
        _clientSecret = configuration["Google:Analytics:ClientSecret"]
            ?? throw new InvalidOperationException("Google:Analytics:ClientSecret missing in configuration.");
        _redirectUri = configuration["Google:Analytics:RedirectUri"]
            ?? throw new InvalidOperationException("Google:Analytics:RedirectUri missing in configuration.");
    }

    public string BuildAuthorizationUrl(string state = "")
    {
        var sb = new StringBuilder(AuthEndpoint);
        sb.Append("?client_id=").Append(Uri.EscapeDataString(_clientId));
        sb.Append("&redirect_uri=").Append(Uri.EscapeDataString(_redirectUri));
        sb.Append("&response_type=code");
        sb.Append("&scope=").Append(Uri.EscapeDataString(OAuthScope));
        sb.Append("&access_type=offline");
        sb.Append("&prompt=consent");
        if (!string.IsNullOrEmpty(state))
            sb.Append("&state=").Append(Uri.EscapeDataString(state));
        return sb.ToString();
    }

    public async Task<AnalyticsTokenResponse> ExchangeCodeAsync(string code)
    {
        var formParams = new Dictionary<string, string>
        {
            ["code"] = code,
            ["client_id"] = _clientId,
            ["client_secret"] = _clientSecret,
            ["redirect_uri"] = _redirectUri,
            ["grant_type"] = "authorization_code"
        };

        var content = new FormUrlEncodedContent(formParams);
        var response = await _httpClient.PostAsync(TokenEndpoint, content);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<AnalyticsTokenResponse>(responseString) ?? new AnalyticsTokenResponse();
    }

    public async Task<AnalyticsTokenResponse> RefreshTokenAsync(string refreshToken)
    {
        var formParams = new Dictionary<string, string>
        {
            ["refresh_token"] = refreshToken,
            ["client_id"] = _clientId,
            ["client_secret"] = _clientSecret,
            ["grant_type"] = "refresh_token"
        };

        var content = new FormUrlEncodedContent(formParams);
        var response = await _httpClient.PostAsync(TokenEndpoint, content);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<AnalyticsTokenResponse>(responseString) ?? new AnalyticsTokenResponse();
    }

    public async Task<T?> GetAsync<T>(string url, string accessToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }

    public async Task<T?> PostAsync<T>(string url, object body, string accessToken)
    {
        var json = JsonConvert.SerializeObject(body, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        request.Content = content;

        var response = await _httpClient.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }

    public async Task<(int StatusCode, string Body)> SendRawAsync(HttpMethod method, string url, string? jsonBody, string accessToken)
    {
        using var request = new HttpRequestMessage(method, url);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        if (jsonBody != null)
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        return ((int)response.StatusCode, await response.Content.ReadAsStringAsync());
    }
}
