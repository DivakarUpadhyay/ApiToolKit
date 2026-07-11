using ApiToolKit.Application.DTOs.Facebook.AdSet;
using ApiToolKit.Application.DTOs.Facebook.Ad;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookAdSetService : IFacebookAdSetService
{
    private readonly IFacebookAuthRepository _authRepo;
    private readonly IFacebookAdSetRepository _adSetRepo;
    private readonly IFacebookApiClient _apiClient;

    public FacebookAdSetService(IFacebookAuthRepository authRepo, IFacebookAdSetRepository adSetRepo, IFacebookApiClient apiClient)
    {
        _authRepo = authRepo;
        _adSetRepo = adSetRepo;
        _apiClient = apiClient;
    }

    private static string NormalizeActId(string id) => id.StartsWith("act_", StringComparison.OrdinalIgnoreCase) ? id[4..] : id;

    public async Task<Result<CreateAdSetResponse>> CreateAdSetAsync(CreateAdSetRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<CreateAdSetResponse>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?>
        {
            ["name"] = request.Name,
            ["campaign_id"] = request.CampaignId,
            ["status"] = request.Status,
            ["optimization_goal"] = request.OptimizationGoal,
            ["billing_event"] = request.BillingEvent
        };
        if (request.DailyBudget != null) body["daily_budget"] = request.DailyBudget;
        if (request.LifetimeBudget != null) body["lifetime_budget"] = request.LifetimeBudget;
        if (request.BidAmount != null) body["bid_amount"] = request.BidAmount;
        if (request.StartTime != null) body["start_time"] = request.StartTime;
        if (request.EndTime != null) body["end_time"] = request.EndTime;
        if (request.Targeting != null) body["targeting"] = request.Targeting;
        if (request.PromotedObject != null) body["promoted_object"] = request.PromotedObject;

        var response = await _apiClient.PostAsync<dynamic>($"/act_{NormalizeActId(request.AccountId)}/adsets", body, accessToken);
        if (response == null) return Result<CreateAdSetResponse>.Fail("No response from Facebook API.");

        string adSetId = response?.id?.ToString() ?? string.Empty;

        await _adSetRepo.SaveAdSetAsync(new FacebookAdSet
        {
            AdSetId = adSetId,
            CampaignId = request.CampaignId,
            AccountId = request.AccountId,
            Name = request.Name,
            Status = request.Status,
            DailyBudget = request.DailyBudget,
            LifetimeBudget = request.LifetimeBudget,
            BillingEvent = request.BillingEvent,
            OptimizationGoal = request.OptimizationGoal,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        await _adSetRepo.SaveApiLogAsync(new FacebookApiLog
        {
            ApiName = "CreateAdSet",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<CreateAdSetResponse>.Ok(new CreateAdSetResponse { Id = adSetId });
    }

    public async Task<Result<AdSetResponse>> GetAdSetAsync(string adSetId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<AdSetResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,campaign_id,status,daily_budget,lifetime_budget,start_time,end_time,optimization_goal,billing_event,bid_amount,created_time,updated_time",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{adSetId}", queryParams);
        if (response == null) return Result<AdSetResponse>.Fail("No response from Facebook API.");

        var adSet = JsonConvert.DeserializeObject<AdSetResponse>(JsonConvert.SerializeObject(response)) ?? new AdSetResponse();
        return Result<AdSetResponse>.Ok(adSet);
    }

    public async Task<Result<GetAdSetsResponse>> GetAdSetsAsync(GetAdSetsRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetAdSetsResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = request.Fields,
            ["limit"] = request.Limit.ToString(),
            ["access_token"] = accessToken
        };
        if (request.Status != null) queryParams["effective_status"] = $"[\"{request.Status}\"]";
        if (request.After != null) queryParams["after"] = request.After;

        var response = await _apiClient.GetAsync<dynamic>($"/{request.CampaignId}/adsets", queryParams);
        if (response == null) return Result<GetAdSetsResponse>.Fail("No response from Facebook API.");

        var adSets = JsonConvert.DeserializeObject<List<AdSetResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<AdSetResponse>();

        return Result<GetAdSetsResponse>.Ok(new GetAdSetsResponse { Data = adSets });
    }

    public async Task<Result<bool>> UpdateAdSetAsync(UpdateAdSetRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?>();
        if (request.Name != null) body["name"] = request.Name;
        if (request.Status != null) body["status"] = request.Status;
        if (request.DailyBudget != null) body["daily_budget"] = request.DailyBudget;
        if (request.BidAmount != null) body["bid_amount"] = request.BidAmount;
        if (request.EndTime != null) body["end_time"] = request.EndTime;

        var response = await _apiClient.PostAsync<dynamic>($"/{request.AdSetId}", body, accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> DeleteAdSetAsync(string adSetId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var response = await _apiClient.DeleteAsync<dynamic>($"/{adSetId}", accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        await _adSetRepo.DeleteAdSetAsync(adSetId);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<GetAdsResponse>> GetAdSetAdsAsync(string adSetId, string accessToken, int limit)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetAdsResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,adset_id,campaign_id,status,creative,created_time,updated_time",
            ["limit"] = limit.ToString(),
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{adSetId}/ads", queryParams);
        if (response == null) return Result<GetAdsResponse>.Fail("No response from Facebook API.");

        var ads = JsonConvert.DeserializeObject<List<AdResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<AdResponse>();

        return Result<GetAdsResponse>.Ok(new GetAdsResponse { Data = ads });
    }
}


