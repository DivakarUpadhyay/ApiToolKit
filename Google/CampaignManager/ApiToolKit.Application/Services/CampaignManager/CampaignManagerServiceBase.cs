using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.CampaignManager;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.Services.CampaignManager;

/// <summary>
/// Shared call pipeline: resolve a valid OAuth token for the account, call Google, log the call, map the result.
/// </summary>
public abstract class CampaignManagerServiceBase
{
    private const int MaxLoggedResponseLength = 4000;
    private readonly ICampaignManagerApiClient _apiClient;
    private readonly ICampaignManagerAuthService _authService;
    private readonly ICampaignManagerAuthRepository _authRepo;

    protected CampaignManagerServiceBase(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
    {
        _apiClient = apiClient;
        _authService = authService;
        _authRepo = authRepo;
    }

    protected async Task<Result<object>> SendAsync(string accountKey, string apiName, HttpMethod method, string url,
        List<KeyValuePair<string, string?>>? query = null, object? body = null, bool raw = false)
    {
        var accessToken = await _authService.GetValidAccessTokenAsync(accountKey);
        if (accessToken == null)
            return Result<object>.Fail("No valid token for this account key. Connect the Google account first with GET /api/campaignmanager/auth/url.");

        var response = await _apiClient.SendAsync(method, url, accessToken, query, body, raw);
        var responseText = response.Body?.ToString(Formatting.None);

        await _authRepo.SaveApiLogAsync(new CampaignManagerApiLog
        {
            AccountKey = accountKey,
            ApiName = apiName,
            HttpMethod = method.Method,
            Url = url,
            RequestBody = body == null ? null : JsonConvert.SerializeObject(body),
            ResponseBody = responseText?.Length > MaxLoggedResponseLength ? responseText[..MaxLoggedResponseLength] : responseText,
            IsSuccess = response.IsSuccess,
            StatusCode = response.StatusCode
        });

        if (!response.IsSuccess)
            return Result<object>.Fail(response.Body?["error"]?["message"]?.ToString() ?? $"Google API returned HTTP {response.StatusCode}.", response.StatusCode);

        return Result<object>.Ok(response.Body ?? new JObject());
    }

    protected static string Esc(string value) => Uri.EscapeDataString(value);

    protected static void Add(List<KeyValuePair<string, string?>> query, string key, string? value)
    {
        if (!string.IsNullOrEmpty(value)) query.Add(new KeyValuePair<string, string?>(key, value));
    }

    protected static void AddMany(List<KeyValuePair<string, string?>> query, string key, IEnumerable<string>? values)
    {
        if (values == null) return;
        foreach (var value in values) Add(query, key, value);
    }

    /// <summary>Returns year (0), month (1) or day (2) of a yyyy-MM-dd / yyyy-MM value without leading zeros.</summary>
    protected static string? DatePart(string? value, int index) =>
        value?.Split('-').ElementAtOrDefault(index)?.TrimStart('0');
}
