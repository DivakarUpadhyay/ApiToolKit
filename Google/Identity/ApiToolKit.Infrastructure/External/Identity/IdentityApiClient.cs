using ApiToolKit.Application.DTOs.Identity.OAuth;
using ApiToolKit.Application.Interfaces.Services.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using System.Web;

namespace ApiToolKit.Infrastructure.External.Identity;

public class IdentityApiClient : IIdentityApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _defaultRedirectUri;
    private readonly string _apiKey;
    private readonly string _projectId;

    private const string OAuthBaseUrl = "https://accounts.google.com/o/oauth2/v2/auth";
    private const string TokenUrl = "https://oauth2.googleapis.com/token";
    private const string RevokeUrl = "https://oauth2.googleapis.com/revoke";

    public IdentityApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _clientId = configuration["Google:Identity:ClientId"] ?? throw new InvalidOperationException("Google:Identity:ClientId missing");
        _clientSecret = configuration["Google:Identity:ClientSecret"] ?? throw new InvalidOperationException("Google:Identity:ClientSecret missing");
        _defaultRedirectUri = configuration["Google:Identity:RedirectUri"] ?? string.Empty;
        _apiKey = configuration["Google:Identity:ApiKey"] ?? throw new InvalidOperationException("Google:Identity:ApiKey missing");
        _projectId = configuration["Google:Identity:ProjectId"] ?? throw new InvalidOperationException("Google:Identity:ProjectId missing");
    }

    public string BuildAuthorizationUrl(List<string> scopes, string state)
    {
        var scopeString = Uri.EscapeDataString(string.Join(" ", scopes));
        var redirectUri = Uri.EscapeDataString(_defaultRedirectUri);
        return $"{OAuthBaseUrl}?client_id={_clientId}&redirect_uri={redirectUri}&response_type=code&scope={scopeString}&state={state}&access_type=offline&prompt=consent";
    }

    public async Task<OAuthTokenResponse?> ExchangeCodeAsync(string code, string redirectUri)
    {
        var body = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["code"] = code,
            ["client_id"] = _clientId,
            ["client_secret"] = _clientSecret,
            ["redirect_uri"] = string.IsNullOrEmpty(redirectUri) ? _defaultRedirectUri : redirectUri,
            ["grant_type"] = "authorization_code"
        });

        var response = await _httpClient.PostAsync(TokenUrl, body);
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<OAuthTokenResponse>(content);
    }

    public async Task<OAuthTokenResponse?> RefreshTokenAsync(string refreshToken)
    {
        var body = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["refresh_token"] = refreshToken,
            ["client_id"] = _clientId,
            ["client_secret"] = _clientSecret,
            ["grant_type"] = "refresh_token"
        });

        var response = await _httpClient.PostAsync(TokenUrl, body);
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<OAuthTokenResponse>(content);
    }

    public async Task<bool> RevokeTokenAsync(string token)
    {
        var body = new FormUrlEncodedContent(new Dictionary<string, string> { ["token"] = token });
        var response = await _httpClient.PostAsync(RevokeUrl, body);
        return response.IsSuccessStatusCode;
    }

    public async Task<T?> GetAsync<T>(string url, string? accessToken = null, Dictionary<string, string>? queryParams = null)
    {
        var builder = new UriBuilder(url);
        if (queryParams != null && queryParams.Count > 0)
        {
            var query = HttpUtility.ParseQueryString(builder.Query);
            foreach (var kv in queryParams)
                query[kv.Key] = kv.Value;
            builder.Query = query.ToString();
        }

        var request = new HttpRequestMessage(HttpMethod.Get, builder.ToString());
        if (!string.IsNullOrEmpty(accessToken))
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(content);
    }

    public async Task<T?> PostAsync<T>(string url, object body, string? accessToken = null)
    {
        var json = JsonConvert.SerializeObject(body, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
        if (!string.IsNullOrEmpty(accessToken))
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseContent);
    }

    public async Task<T?> PatchAsync<T>(string url, object body, string accessToken)
    {
        var json = JsonConvert.SerializeObject(body, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Patch, url) { Content = content };
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseContent);
    }

    public async Task<bool> DeleteAsync(string url, string accessToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, url);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        var response = await _httpClient.SendAsync(request);
        return response.IsSuccessStatusCode;
    }
}
