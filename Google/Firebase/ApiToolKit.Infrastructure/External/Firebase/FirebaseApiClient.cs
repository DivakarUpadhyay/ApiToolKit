using ApiToolKit.Application.DTOs.Firebase.Auth;
using ApiToolKit.Application.Interfaces.Services.Firebase;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using System.Web;

namespace ApiToolKit.Infrastructure.External.Firebase;

public class FirebaseApiClient : IFirebaseApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _defaultRedirectUri;
    private readonly string _projectId;
    private readonly string _apiKey;

    private const string OAuthBaseUrl = "https://accounts.google.com/o/oauth2/v2/auth";
    private const string TokenUrl = "https://oauth2.googleapis.com/token";

    // Firebase API scopes for OAuth
    private const string FirebaseScopes =
        "https://www.googleapis.com/auth/firebase " +
        "https://www.googleapis.com/auth/cloud-platform " +
        "https://www.googleapis.com/auth/firebase.messaging " +
        "https://www.googleapis.com/auth/firebase.remoteconfig";

    public FirebaseApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _clientId = configuration["Google:Firebase:ClientId"] ?? throw new InvalidOperationException("Google:Firebase:ClientId missing");
        _clientSecret = configuration["Google:Firebase:ClientSecret"] ?? throw new InvalidOperationException("Google:Firebase:ClientSecret missing");
        _defaultRedirectUri = configuration["Google:Firebase:RedirectUri"] ?? string.Empty;
        _projectId = configuration["Google:Firebase:ProjectId"] ?? throw new InvalidOperationException("Google:Firebase:ProjectId missing");
        _apiKey = configuration["Google:Firebase:ApiKey"] ?? throw new InvalidOperationException("Google:Firebase:ApiKey missing");
    }

    public string BuildAuthorizationUrl()
    {
        var scopeEncoded = Uri.EscapeDataString(FirebaseScopes);
        var redirectUri = Uri.EscapeDataString(_defaultRedirectUri);
        var state = Guid.NewGuid().ToString("N");
        return $"{OAuthBaseUrl}?client_id={_clientId}&redirect_uri={redirectUri}&response_type=code&scope={scopeEncoded}&state={state}&access_type=offline&prompt=consent";
    }

    public async Task<FirebaseTokenResponse?> ExchangeCodeAsync(string code, string redirectUri)
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
        return JsonConvert.DeserializeObject<FirebaseTokenResponse>(content);
    }

    public async Task<FirebaseTokenResponse?> RefreshTokenAsync(string refreshToken)
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
        return JsonConvert.DeserializeObject<FirebaseTokenResponse>(content);
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

    public async Task<T?> PatchAsync<T>(string url, object body, string accessToken, Dictionary<string, string>? headers = null)
    {
        var json = JsonConvert.SerializeObject(body, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Patch, url) { Content = content };
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        if (headers != null)
        {
            foreach (var header in headers)
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

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
