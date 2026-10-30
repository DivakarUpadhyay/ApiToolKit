using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.GoogleAds;
using ApiToolKit.Domain.Interfaces.Repositories.GoogleAds;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace ApiToolKit.Application.Services.GoogleAds;

public class GoogleAdsRestService : IGoogleAdsRestService
{
    private const int MaxLoggedResponseLength = 4000;
    private readonly IGoogleAdsAuthRepository _authRepo;
    private readonly IGoogleAdsApiClient _apiClient;

    public GoogleAdsRestService(IGoogleAdsAuthRepository authRepo, IGoogleAdsApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<JsonNode>> SendAsync(string customerId, string apiName, HttpMethod method, string path,
        IEnumerable<KeyValuePair<string, string?>>? query = null, JsonNode? body = null)
    {
        var accessToken = await GetValidAccessTokenAsync(customerId);
        if (accessToken == null)
            return Result<JsonNode>.Fail("No token found for customer. Please authenticate first.");

        // Top-level planning methods look like v26:listPlannableLocations (no slash before the colon).
        var url = path.StartsWith(':') ? GoogleAdsApiVersion.BaseUrl + path : $"{GoogleAdsApiVersion.BaseUrl}/{path}";
        var queryString = string.Join("&", (query ?? Enumerable.Empty<KeyValuePair<string, string?>>())
            .Where(kv => !string.IsNullOrEmpty(kv.Value))
            .Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value!)}"));
        if (queryString.Length > 0)
            url += "?" + queryString;

        var requestJson = body?.ToJsonString();
        var (statusCode, responseText) = await _apiClient.SendRawAsync(method, url, accessToken, requestJson, null);
        var isSuccess = statusCode is >= 200 and < 300;

        await _authRepo.SaveApiLogAsync(new GoogleAdsApiLog
        {
            CustomerId = customerId,
            ApiName = apiName,
            RequestBody = requestJson ?? string.Empty,
            ResponseBody = responseText.Length > MaxLoggedResponseLength ? responseText[..MaxLoggedResponseLength] : responseText,
            IsSuccess = isSuccess,
            StatusCode = statusCode,
            CreatedAt = DateTime.UtcNow
        });

        JsonNode? parsed;
        try { parsed = string.IsNullOrWhiteSpace(responseText) ? new JsonObject() : JsonNode.Parse(responseText); }
        catch (JsonException) { parsed = JsonValue.Create(responseText); }

        if (!isSuccess)
        {
            // searchStream returns an array; errors come back as an object.
            var message = (parsed as JsonObject)?["error"]?["message"]?.ToString()
                          ?? ((parsed as JsonArray)?.FirstOrDefault() as JsonObject)?["error"]?["message"]?.ToString();
            return Result<JsonNode>.Fail(message ?? $"Google Ads API returned HTTP {statusCode}.", statusCode);
        }

        return Result<JsonNode>.Ok(parsed ?? new JsonObject());
    }

    private async Task<string?> GetValidAccessTokenAsync(string customerId)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(customerId);
        if (token == null) return null;
        if (token.ExpiresAt > DateTime.UtcNow.AddMinutes(1) || string.IsNullOrEmpty(token.RefreshToken)) return token.AccessToken;

        var refreshed = await _apiClient.RefreshTokenAsync(token.RefreshToken);
        if (refreshed == null || string.IsNullOrEmpty(refreshed.AccessToken)) return token.AccessToken;

        token.AccessToken = refreshed.AccessToken;
        token.ExpiresAt = DateTime.UtcNow.AddSeconds(refreshed.ExpiresIn);
        token.UpdatedAt = DateTime.UtcNow;
        await _authRepo.UpdateTokenAsync(token);
        return token.AccessToken;
    }
}
