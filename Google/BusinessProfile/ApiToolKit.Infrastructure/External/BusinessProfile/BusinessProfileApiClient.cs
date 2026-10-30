using ApiToolKit.Application.DTOs.BusinessProfile.Auth;
using ApiToolKit.Application.DTOs.BusinessProfile.Common;
using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace ApiToolKit.Infrastructure.External.BusinessProfile;

public class BusinessProfileApiClient : IBusinessProfileApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _redirectUri;
    private const string AuthEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
    private const string TokenEndpoint = "https://oauth2.googleapis.com/token";
    private const string OAuthScopes =
        "https://www.googleapis.com/auth/business.manage";

    private static readonly JsonSerializerSettings BodySettings = new() { NullValueHandling = NullValueHandling.Ignore };

    public BusinessProfileApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _clientId = configuration["Google:BusinessProfile:ClientId"]
            ?? throw new InvalidOperationException("Google:BusinessProfile:ClientId missing in configuration.");
        _clientSecret = configuration["Google:BusinessProfile:ClientSecret"]
            ?? throw new InvalidOperationException("Google:BusinessProfile:ClientSecret missing in configuration.");
        _redirectUri = configuration["Google:BusinessProfile:RedirectUri"]
            ?? throw new InvalidOperationException("Google:BusinessProfile:RedirectUri missing in configuration.");
    }

    public string BuildAuthorizationUrl(string state)
    {
        var sb = new StringBuilder(AuthEndpoint);
        sb.Append("?client_id=").Append(Uri.EscapeDataString(_clientId));
        sb.Append("&redirect_uri=").Append(Uri.EscapeDataString(_redirectUri));
        sb.Append("&response_type=code");
        sb.Append("&scope=").Append(Uri.EscapeDataString(OAuthScopes));
        sb.Append("&access_type=offline");
        sb.Append("&prompt=consent");
        sb.Append("&state=").Append(Uri.EscapeDataString(state));
        return sb.ToString();
    }

    public Task<BusinessProfileTokenResponse> ExchangeCodeAsync(string code) =>
        PostTokenAsync(new Dictionary<string, string>
        {
            ["code"] = code,
            ["client_id"] = _clientId,
            ["client_secret"] = _clientSecret,
            ["redirect_uri"] = _redirectUri,
            ["grant_type"] = "authorization_code"
        });

    public Task<BusinessProfileTokenResponse> RefreshTokenAsync(string refreshToken) =>
        PostTokenAsync(new Dictionary<string, string>
        {
            ["refresh_token"] = refreshToken,
            ["client_id"] = _clientId,
            ["client_secret"] = _clientSecret,
            ["grant_type"] = "refresh_token"
        });

    public async Task<BusinessProfileApiResponse> SendAsync(HttpMethod method, string url, string accessToken,
        IEnumerable<KeyValuePair<string, string?>>? query = null, object? body = null, bool raw = false)
    {
        var requestUrl = url;
        var queryString = string.Join("&", (query ?? Enumerable.Empty<KeyValuePair<string, string?>>())
            .Where(kv => !string.IsNullOrEmpty(kv.Value))
            .Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value!)}"));
        if (queryString.Length > 0)
            requestUrl += (requestUrl.Contains('?') ? "&" : "?") + queryString;

        using var request = new HttpRequestMessage(method, requestUrl);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        if (body != null)
            request.Content = new StringContent(JsonConvert.SerializeObject(body, BodySettings), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();

        return new BusinessProfileApiResponse
        {
            StatusCode = (int)response.StatusCode,
            IsSuccess = response.IsSuccessStatusCode,
            Body = raw ? new JValue(responseString) : ParseBody(responseString)
        };
    }

    private async Task<BusinessProfileTokenResponse> PostTokenAsync(Dictionary<string, string> formParams)
    {
        var response = await _httpClient.PostAsync(TokenEndpoint, new FormUrlEncodedContent(formParams));
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<BusinessProfileTokenResponse>(responseString) ?? new BusinessProfileTokenResponse();
    }

    private static JToken ParseBody(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return new JObject();
        try { return JToken.Parse(text); }
        catch (JsonReaderException) { return new JValue(text); }
    }
}
