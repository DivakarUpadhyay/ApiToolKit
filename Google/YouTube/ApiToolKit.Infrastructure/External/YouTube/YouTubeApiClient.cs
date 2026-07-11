using ApiToolKit.Application.DTOs.YouTube.Auth;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace ApiToolKit.Infrastructure.External.YouTube;

public class YouTubeApiClient : IYouTubeApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _redirectUri;
    private readonly string _apiKey;
    private const string DataApiBaseUrl = "https://www.googleapis.com/youtube/v3/";
    private const string AnalyticsApiBaseUrl = "https://youtubeanalytics.googleapis.com/v2/";
    private const string TokenEndpoint = "https://oauth2.googleapis.com/token";
    private const string AuthEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";

    public YouTubeApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _clientId = configuration["Google:YouTube:ClientId"] ?? throw new InvalidOperationException("Google:YouTube:ClientId missing");
        _clientSecret = configuration["Google:YouTube:ClientSecret"] ?? throw new InvalidOperationException("Google:YouTube:ClientSecret missing");
        _redirectUri = configuration["Google:YouTube:RedirectUri"] ?? throw new InvalidOperationException("Google:YouTube:RedirectUri missing");
        _apiKey = configuration["Google:YouTube:ApiKey"] ?? string.Empty;
    }

    public string BuildAuthorizationUrl(string[] scopes)
    {
        var scope = Uri.EscapeDataString(string.Join(" ", scopes));
        var redirectUri = Uri.EscapeDataString(_redirectUri);
        return $"{AuthEndpoint}?client_id={_clientId}&redirect_uri={redirectUri}&response_type=code&scope={scope}&access_type=offline&prompt=consent";
    }

    public async Task<YouTubeTokenResponse?> ExchangeCodeAsync(string code, string redirectUri)
    {
        var body = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["code"] = code,
            ["client_id"] = _clientId,
            ["client_secret"] = _clientSecret,
            ["redirect_uri"] = redirectUri,
            ["grant_type"] = "authorization_code"
        });

        var response = await _httpClient.PostAsync(TokenEndpoint, body);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<YouTubeTokenResponse>(responseString);
    }

    public async Task<YouTubeTokenResponse?> RefreshTokenAsync(string refreshToken)
    {
        var body = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["refresh_token"] = refreshToken,
            ["client_id"] = _clientId,
            ["client_secret"] = _clientSecret,
            ["grant_type"] = "refresh_token"
        });

        var response = await _httpClient.PostAsync(TokenEndpoint, body);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<YouTubeTokenResponse>(responseString);
    }

    public async Task<T?> GetAsync<T>(string url, string? accessToken, Dictionary<string, string>? queryParams = null)
    {
        var baseUrl = url.StartsWith("reports") ? AnalyticsApiBaseUrl : DataApiBaseUrl;
        var fullUrl = BuildUrl(baseUrl + url, queryParams, accessToken);

        var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
        if (!string.IsNullOrEmpty(accessToken))
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }

    public async Task<T?> PostAsync<T>(string url, object body, string accessToken)
    {
        var baseUrl = url.StartsWith("reports") ? AnalyticsApiBaseUrl : DataApiBaseUrl;
        var fullUrl = baseUrl + url;

        var json = JsonConvert.SerializeObject(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Post, fullUrl) { Content = content };
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }

    public async Task<T?> PutAsync<T>(string url, object body, string accessToken)
    {
        var baseUrl = DataApiBaseUrl;
        var fullUrl = baseUrl + url;

        var json = JsonConvert.SerializeObject(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Put, fullUrl) { Content = content };
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }

    public async Task<bool> DeleteAsync(string url, string accessToken)
    {
        var fullUrl = DataApiBaseUrl + url;

        var request = new HttpRequestMessage(HttpMethod.Delete, fullUrl);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.SendAsync(request);
        return response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NoContent;
    }

    public async Task<T?> UploadVideoAsync<T>(object metadata, Stream videoStream, string mimeType, string accessToken)
    {
        const string uploadUrl = "https://www.googleapis.com/upload/youtube/v3/videos?uploadType=multipart&part=snippet,status";

        var boundary = $"---boundary_{Guid.NewGuid():N}";
        var multipart = new MultipartContent("related", boundary);

        var metaJson = JsonConvert.SerializeObject(metadata, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        var metaPart = new StringContent(metaJson, Encoding.UTF8, "application/json");
        multipart.Add(metaPart);

        var videoPart = new StreamContent(videoStream);
        videoPart.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mimeType);
        multipart.Add(videoPart);

        var request = new HttpRequestMessage(HttpMethod.Post, uploadUrl) { Content = multipart };
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseString);
    }

    private string BuildUrl(string baseUrl, Dictionary<string, string>? queryParams, string? accessToken)
    {
        var sb = new StringBuilder(baseUrl);
        var hasQuery = baseUrl.Contains('?');

        if (queryParams != null && queryParams.Count > 0)
        {
            foreach (var kv in queryParams)
            {
                sb.Append(hasQuery ? '&' : '?');
                sb.Append($"{kv.Key}={Uri.EscapeDataString(kv.Value)}");
                hasQuery = true;
            }
        }

        return sb.ToString();
    }
}
