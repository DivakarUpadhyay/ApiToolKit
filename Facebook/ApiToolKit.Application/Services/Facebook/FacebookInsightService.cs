using ApiToolKit.Application.DTOs.Facebook.Insight;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookInsightService : IFacebookInsightService
{
    private readonly IFacebookAuthRepository _authRepo;
    private readonly IFacebookInsightRepository _insightRepo;
    private readonly IFacebookApiClient _apiClient;

    public FacebookInsightService(IFacebookAuthRepository authRepo, IFacebookInsightRepository insightRepo, IFacebookApiClient apiClient)
    {
        _authRepo = authRepo;
        _insightRepo = insightRepo;
        _apiClient = apiClient;
    }

    private static string NormalizeActId(string id) => id.StartsWith("act_", StringComparison.OrdinalIgnoreCase) ? id[4..] : id;

    public async Task<Result<GetPageInsightsResponse>> GetPageInsightsAsync(GetPageInsightsRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetPageInsightsResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["metric"] = string.Join(",", request.Metric),
            ["period"] = request.Period,
            ["access_token"] = accessToken
        };
        if (request.Since.HasValue) queryParams["since"] = new DateTimeOffset(request.Since.Value).ToUnixTimeSeconds().ToString();
        if (request.Until.HasValue) queryParams["until"] = new DateTimeOffset(request.Until.Value).ToUnixTimeSeconds().ToString();

        var response = await _apiClient.GetAsync<dynamic>($"/{request.PageId}/insights", queryParams);
        if (response == null) return Result<GetPageInsightsResponse>.Fail("No response from Facebook API.");

        var insights = JsonConvert.DeserializeObject<List<PageInsightResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<PageInsightResponse>();

        await _insightRepo.SaveApiLogAsync(new FacebookApiLog
        {
            PageId = request.PageId,
            ApiName = "GetPageInsights",
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<GetPageInsightsResponse>.Ok(new GetPageInsightsResponse { Data = insights });
    }

    public async Task<Result<GetPageInsightsResponse>> GetVideoInsightsAsync(VideoInsightRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetPageInsightsResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["metric"] = request.Metric,
            ["period"] = request.Period,
            ["access_token"] = accessToken
        };
        if (request.Since.HasValue) queryParams["since"] = new DateTimeOffset(request.Since.Value).ToUnixTimeSeconds().ToString();
        if (request.Until.HasValue) queryParams["until"] = new DateTimeOffset(request.Until.Value).ToUnixTimeSeconds().ToString();

        var response = await _apiClient.GetAsync<dynamic>($"/{request.VideoId}/video_insights", queryParams);
        if (response == null) return Result<GetPageInsightsResponse>.Fail("No response from Facebook API.");

        var insights = JsonConvert.DeserializeObject<List<PageInsightResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<PageInsightResponse>();

        return Result<GetPageInsightsResponse>.Ok(new GetPageInsightsResponse { Data = insights });
    }

    public async Task<Result<GetAdInsightsResponse>> GetAdInsightsAsync(GetAdInsightsRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetAdInsightsResponse>.Fail("Facebook configuration not found.");

        var fields = request.Fields.Count > 0
            ? string.Join(",", request.Fields)
            : "account_id,campaign_id,adset_id,ad_id,impressions,reach,clicks,spend,cpc,cpm,ctr,cpp,frequency,unique_clicks,actions,date_start,date_stop";

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = fields,
            ["level"] = request.Level,
            ["limit"] = request.Limit.ToString(),
            ["access_token"] = accessToken
        };
        if (request.DatePreset != null) queryParams["date_preset"] = request.DatePreset;
        if (request.Since != null) queryParams["time_range[since]"] = request.Since;
        if (request.Until != null) queryParams["time_range[until]"] = request.Until;
        if (request.After != null) queryParams["after"] = request.After;
        if (request.Breakdowns?.Count > 0) queryParams["breakdowns"] = string.Join(",", request.Breakdowns);

        var objectId = request.AdId ?? request.AdSetId ?? request.CampaignId ?? $"act_{NormalizeActId(request.AccountId)}";
        var response = await _apiClient.GetAsync<dynamic>($"/{objectId}/insights", queryParams);
        if (response == null) return Result<GetAdInsightsResponse>.Fail("No response from Facebook API.");

        var insights = JsonConvert.DeserializeObject<List<AdInsightResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<AdInsightResponse>();

        await _insightRepo.SaveApiLogAsync(new FacebookApiLog
        {
            ApiName = "GetAdInsights",
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<GetAdInsightsResponse>.Ok(new GetAdInsightsResponse { Data = insights });
    }

    public async Task<Result<GetAdInsightsResponse>> GetCampaignInsightsAsync(string campaignId, string accessToken, string? datePreset, int limit)
    {
        return await GetAdInsightsAsync(new GetAdInsightsRequest
        {
            AccountId = string.Empty,
            CampaignId = campaignId,
            Level = "campaign",
            DatePreset = datePreset,
            Limit = limit
        }, accessToken);
    }

    public async Task<Result<GetAdInsightsResponse>> GetAdSetInsightsAsync(string adSetId, string accessToken, string? datePreset, int limit)
    {
        return await GetAdInsightsAsync(new GetAdInsightsRequest
        {
            AccountId = string.Empty,
            AdSetId = adSetId,
            Level = "adset",
            DatePreset = datePreset,
            Limit = limit
        }, accessToken);
    }

    public async Task<Result<GetAdInsightsResponse>> GetAccountInsightsAsync(string accountId, string accessToken, string? datePreset, int limit)
    {
        return await GetAdInsightsAsync(new GetAdInsightsRequest
        {
            AccountId = accountId,
            Level = "account",
            DatePreset = datePreset,
            Limit = limit
        }, accessToken);
    }
}


