using ApiToolKit.Application.DTOs.Facebook.Campaign;
using ApiToolKit.Application.DTOs.Facebook.AdSet;
using ApiToolKit.Application.DTOs.Facebook.Ad;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookCampaignService : IFacebookCampaignService
{
    private readonly IFacebookAuthRepository _authRepo;
    private readonly IFacebookCampaignRepository _campaignRepo;
    private readonly IFacebookApiClient _apiClient;

    public FacebookCampaignService(IFacebookAuthRepository authRepo, IFacebookCampaignRepository campaignRepo, IFacebookApiClient apiClient)
    {
        _authRepo = authRepo;
        _campaignRepo = campaignRepo;
        _apiClient = apiClient;
    }

    private static string NormalizeActId(string id) => id.StartsWith("act_", StringComparison.OrdinalIgnoreCase) ? id[4..] : id;

    public async Task<Result<CreateCampaignResponse>> CreateCampaignAsync(CreateCampaignRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<CreateCampaignResponse>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?>
        {
            ["name"] = request.Name,
            ["objective"] = request.Objective,
            ["status"] = request.Status,
            ["special_ad_categories"] = request.SpecialAdCategories ?? new List<string>(),
            ["is_adset_budget_sharing_enabled"] = false
        };
        if (request.DailyBudget != null) body["daily_budget"] = request.DailyBudget;
        if (request.LifetimeBudget != null) body["lifetime_budget"] = request.LifetimeBudget;
        if (request.StartTime != null) body["start_time"] = request.StartTime;
        if (request.StopTime != null) body["stop_time"] = request.StopTime;
        if (request.BudgetRebalanceFlag.HasValue) body["budget_rebalance_flag"] = request.BudgetRebalanceFlag.Value;

        var response = await _apiClient.PostAsync<dynamic>($"/act_{NormalizeActId(request.AccountId)}/campaigns", body, accessToken);
        if (response == null) return Result<CreateCampaignResponse>.Fail("No response from Facebook API.");

        string campaignId = response?.id?.ToString() ?? string.Empty;

        await _campaignRepo.SaveCampaignAsync(new FacebookCampaign
        {
            CampaignId = campaignId,
            AccountId = request.AccountId,
            Name = request.Name,
            Objective = request.Objective,
            Status = request.Status,
            DailyBudget = request.DailyBudget,
            LifetimeBudget = request.LifetimeBudget,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        await _campaignRepo.SaveApiLogAsync(new FacebookApiLog
        {
            ApiName = "CreateCampaign",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<CreateCampaignResponse>.Ok(new CreateCampaignResponse { Id = campaignId });
    }

    public async Task<Result<CampaignResponse>> GetCampaignAsync(string campaignId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<CampaignResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,objective,status,daily_budget,lifetime_budget,start_time,stop_time,budget_remaining,created_time,updated_time",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{campaignId}", queryParams);
        if (response == null) return Result<CampaignResponse>.Fail("No response from Facebook API.");

        var campaign = JsonConvert.DeserializeObject<CampaignResponse>(JsonConvert.SerializeObject(response)) ?? new CampaignResponse();
        return Result<CampaignResponse>.Ok(campaign);
    }

    public async Task<Result<GetCampaignsResponse>> GetCampaignsAsync(GetCampaignsRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetCampaignsResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = request.Fields,
            ["limit"] = request.Limit.ToString(),
            ["access_token"] = accessToken
        };
        if (request.Status != null) queryParams["effective_status"] = $"[\"{request.Status}\"]";
        if (request.After != null) queryParams["after"] = request.After;

        var response = await _apiClient.GetAsync<dynamic>($"/act_{NormalizeActId(request.AccountId)}/campaigns", queryParams);
        if (response == null) return Result<GetCampaignsResponse>.Fail("No response from Facebook API.");

        var campaigns = JsonConvert.DeserializeObject<List<CampaignResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<CampaignResponse>();

        return Result<GetCampaignsResponse>.Ok(new GetCampaignsResponse { Data = campaigns });
    }

    public async Task<Result<bool>> UpdateCampaignAsync(UpdateCampaignRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?>();
        if (request.Name != null) body["name"] = request.Name;
        if (request.Status != null) body["status"] = request.Status;
        if (request.DailyBudget != null) body["daily_budget"] = request.DailyBudget;
        if (request.LifetimeBudget != null) body["lifetime_budget"] = request.LifetimeBudget;

        var response = await _apiClient.PostAsync<dynamic>($"/{request.CampaignId}", body, accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> DeleteCampaignAsync(string campaignId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var response = await _apiClient.DeleteAsync<dynamic>($"/{campaignId}", accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        await _campaignRepo.DeleteCampaignAsync(campaignId);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<GetAdSetsResponse>> GetCampaignAdSetsAsync(string campaignId, string accessToken, int limit)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetAdSetsResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,campaign_id,status,daily_budget,lifetime_budget,start_time,end_time,optimization_goal,billing_event",
            ["limit"] = limit.ToString(),
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{campaignId}/adsets", queryParams);
        if (response == null) return Result<GetAdSetsResponse>.Fail("No response from Facebook API.");

        var adSets = JsonConvert.DeserializeObject<List<AdSetResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<AdSetResponse>();

        return Result<GetAdSetsResponse>.Ok(new GetAdSetsResponse { Data = adSets });
    }

    public async Task<Result<GetAdsResponse>> GetCampaignAdsAsync(string campaignId, string accessToken, int limit)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetAdsResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,adset_id,campaign_id,status,creative,created_time,updated_time",
            ["limit"] = limit.ToString(),
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{campaignId}/ads", queryParams);
        if (response == null) return Result<GetAdsResponse>.Fail("No response from Facebook API.");

        var ads = JsonConvert.DeserializeObject<List<AdResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<AdResponse>();

        return Result<GetAdsResponse>.Ok(new GetAdsResponse { Data = ads });
    }
}


