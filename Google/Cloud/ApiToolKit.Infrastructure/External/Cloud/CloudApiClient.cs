using ApiToolKit.Application.DTOs.Cloud.Auth;
using ApiToolKit.Application.Interfaces.Services.Cloud;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace ApiToolKit.Infrastructure.External.Cloud;

public class CloudApiClient : ICloudApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _redirectUri;
    private readonly string _projectId;

    private const string AuthBaseUrl = "https://accounts.google.com/o/oauth2/v2/auth";
    private const string TokenUrl = "https://oauth2.googleapis.com/token";

    public CloudApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _clientId = configuration["Google:Cloud:ClientId"] ?? throw new InvalidOperationException("Google:Cloud:ClientId missing");
        _clientSecret = configuration["Google:Cloud:ClientSecret"] ?? throw new InvalidOperationException("Google:Cloud:ClientSecret missing");
        _redirectUri = configuration["Google:Cloud:RedirectUri"] ?? throw new InvalidOperationException("Google:Cloud:RedirectUri missing");
        _projectId = configuration["Google:Cloud:ProjectId"] ?? throw new InvalidOperationException("Google:Cloud:ProjectId missing");
    }

    public string BuildAuthorizationUrl(string[] scopes)
    {
        var scopeStr = Uri.EscapeDataString(string.Join(" ", scopes));
        var redirectUri = Uri.EscapeDataString(_redirectUri);
        return $"{AuthBaseUrl}?client_id={_clientId}&redirect_uri={redirectUri}&response_type=code&scope={scopeStr}&access_type=offline&prompt=consent";
    }

    public async Task<CloudTokenResponse?> ExchangeCodeAsync(string code, string redirectUri)
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
        var response = await _httpClient.PostAsync(TokenUrl, content);
        var responseString = await response.Content.ReadAsStringAsync();

        var rawToken = JsonConvert.DeserializeObject<dynamic>(responseString);
        if (rawToken == null) return null;

        return new CloudTokenResponse
        {
            AccessToken = rawToken.access_token?.ToString() ?? string.Empty,
            RefreshToken = rawToken.refresh_token?.ToString(),
            ExpiresIn = int.TryParse(rawToken.expires_in?.ToString(), out int expCode) ? expCode : 3600,
            TokenType = rawToken.token_type?.ToString() ?? "Bearer",
            Scope = rawToken.scope?.ToString()
        };
    }

    public async Task<CloudTokenResponse?> RefreshTokenAsync(string refreshToken)
    {
        var formData = new Dictionary<string, string>
        {
            ["refresh_token"] = refreshToken,
            ["client_id"] = _clientId,
            ["client_secret"] = _clientSecret,
            ["grant_type"] = "refresh_token"
        };

        var content = new FormUrlEncodedContent(formData);
        var response = await _httpClient.PostAsync(TokenUrl, content);
        var responseString = await response.Content.ReadAsStringAsync();

        var rawToken = JsonConvert.DeserializeObject<dynamic>(responseString);
        if (rawToken == null) return null;

        return new CloudTokenResponse
        {
            AccessToken = rawToken.access_token?.ToString() ?? string.Empty,
            RefreshToken = rawToken.refresh_token?.ToString() ?? refreshToken,
            ExpiresIn = int.TryParse(rawToken.expires_in?.ToString(), out int expRefresh) ? expRefresh : 3600,
            TokenType = rawToken.token_type?.ToString() ?? "Bearer",
            Scope = rawToken.scope?.ToString()
        };
    }

    public async Task<T?> GetAsync<T>(string url, string accessToken, Dictionary<string, string>? queryParams = null)
    {
        var fullUrl = url;
        if (queryParams != null && queryParams.Count > 0)
        {
            var queryString = string.Join("&", queryParams.Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value)}"));
            fullUrl = url.Contains('?') ? $"{url}&{queryString}" : $"{url}?{queryString}";
        }

        using var requestMessage = new HttpRequestMessage(HttpMethod.Get, fullUrl);
        requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.SendAsync(requestMessage);
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

        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
        requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        requestMessage.Content = content;

        var response = await _httpClient.SendAsync(requestMessage);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }
}
