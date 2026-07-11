using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace ApiToolKit.Infrastructure.External.Workspace;

public class WorkspaceApiClient : IWorkspaceApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _redirectUri;

    public WorkspaceApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _clientId = configuration["Google:Workspace:ClientId"] ?? throw new InvalidOperationException("Google:Workspace:ClientId missing");
        _clientSecret = configuration["Google:Workspace:ClientSecret"] ?? throw new InvalidOperationException("Google:Workspace:ClientSecret missing");
        _redirectUri = configuration["Google:Workspace:RedirectUri"] ?? throw new InvalidOperationException("Google:Workspace:RedirectUri missing");
    }

    public string BuildAuthorizationUrl(string[] scopes, string state = "")
    {
        var scopeStr = Uri.EscapeDataString(string.Join(" ", scopes));
        var sb = new StringBuilder("https://accounts.google.com/o/oauth2/v2/auth");
        sb.Append($"?client_id={Uri.EscapeDataString(_clientId)}");
        sb.Append($"&redirect_uri={Uri.EscapeDataString(_redirectUri)}");
        sb.Append("&response_type=code");
        sb.Append($"&scope={scopeStr}");
        sb.Append("&access_type=offline");
        sb.Append("&prompt=consent");
        if (!string.IsNullOrEmpty(state)) sb.Append($"&state={Uri.EscapeDataString(state)}");
        return sb.ToString();
    }

    public async Task<T?> ExchangeCodeAsync<T>(string code, string redirectUri)
    {
        var body = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["code"] = code,
            ["client_id"] = _clientId,
            ["client_secret"] = _clientSecret,
            ["redirect_uri"] = redirectUri,
            ["grant_type"] = "authorization_code"
        });

        var response = await _httpClient.PostAsync("https://oauth2.googleapis.com/token", body);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }

    public async Task<T?> RefreshTokenAsync<T>(string refreshToken)
    {
        var body = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["refresh_token"] = refreshToken,
            ["client_id"] = _clientId,
            ["client_secret"] = _clientSecret,
            ["grant_type"] = "refresh_token"
        });

        var response = await _httpClient.PostAsync("https://oauth2.googleapis.com/token", body);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }

    public async Task<T?> GetAsync<T>(string url, string accessToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }

    public async Task<T?> PostAsync<T>(string url, object? body, string accessToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        if (body != null)
        {
            var json = JsonConvert.SerializeObject(body);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        var response = await _httpClient.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }

    public async Task<T?> PatchAsync<T>(string url, object? body, string accessToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Patch, url);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        if (body != null)
        {
            var json = JsonConvert.SerializeObject(body);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        var response = await _httpClient.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }

    public async Task<T?> PutAsync<T>(string url, object? body, string accessToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Put, url);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        if (body != null)
        {
            var json = JsonConvert.SerializeObject(body);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        var response = await _httpClient.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }

    public async Task DeleteAsync(string url, string accessToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, url);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        await _httpClient.SendAsync(request);
    }
}
