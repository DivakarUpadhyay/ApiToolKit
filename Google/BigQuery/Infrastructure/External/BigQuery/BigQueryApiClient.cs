using ApiToolKit.Application.DTOs.BigQuery.Auth;
using ApiToolKit.Application.Interfaces.Services.BigQuery;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace ApiToolKit.Infrastructure.External.BigQuery;

public class BigQueryApiClient : IBigQueryApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _redirectUri;
    private const string OAuthScope = "https://www.googleapis.com/auth/bigquery";
    private const string TokenEndpoint = "https://oauth2.googleapis.com/token";
    private const string AuthEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";

    public BigQueryApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _clientId = configuration["Google:BigQuery:ClientId"]
            ?? throw new InvalidOperationException("Google:BigQuery:ClientId missing in configuration.");
        _clientSecret = configuration["Google:BigQuery:ClientSecret"]
            ?? throw new InvalidOperationException("Google:BigQuery:ClientSecret missing in configuration.");
        _redirectUri = configuration["Google:BigQuery:RedirectUri"]
            ?? throw new InvalidOperationException("Google:BigQuery:RedirectUri missing in configuration.");
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

    public async Task<BigQueryTokenResponse> ExchangeCodeAsync(string code, string redirectUri)
    {
        var formParams = new Dictionary<string, string>
        {
            ["code"] = code,
            ["client_id"] = _clientId,
            ["client_secret"] = _clientSecret,
            ["redirect_uri"] = redirectUri,
            ["grant_type"] = "authorization_code"
        };

        var content = new FormUrlEncodedContent(formParams);
        var response = await _httpClient.PostAsync(TokenEndpoint, content);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<BigQueryTokenResponse>(responseString) ?? new BigQueryTokenResponse();
    }

    public async Task<BigQueryTokenResponse> RefreshTokenAsync(string refreshToken)
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
        return JsonConvert.DeserializeObject<BigQueryTokenResponse>(responseString) ?? new BigQueryTokenResponse();
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

    public async Task<T?> PatchAsync<T>(string url, object body, string accessToken)
    {
        var json = JsonConvert.SerializeObject(body, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var request = new HttpRequestMessage(HttpMethod.Patch, url);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        request.Content = content;

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
