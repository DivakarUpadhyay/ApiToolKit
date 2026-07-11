using ApiToolKit.Application.DTOs.Facebook.Ad;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookAdService : IFacebookAdService
{
    private readonly IFacebookAuthRepository _authRepo;
    private readonly IFacebookAdRepository _adRepo;
    private readonly IFacebookApiClient _apiClient;

    public FacebookAdService(IFacebookAuthRepository authRepo, IFacebookAdRepository adRepo, IFacebookApiClient apiClient)
    {
        _authRepo = authRepo;
        _adRepo = adRepo;
        _apiClient = apiClient;
    }

    private static string NormalizeActId(string id) => id.StartsWith("act_", StringComparison.OrdinalIgnoreCase) ? id[4..] : id;

    public async Task<Result<CreateAdResponse>> CreateAdAsync(CreateAdRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<CreateAdResponse>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?>
        {
            ["name"] = request.Name,
            ["adset_id"] = request.AdSetId,
            ["status"] = request.Status
        };
        if (request.Creative != null)
        {
            if (request.Creative.CreativeId != null)
                body["creative"] = new { creative_id = request.Creative.CreativeId };
            else if (request.Creative.ObjectStorySpec != null)
                body["creative"] = new { object_story_spec = request.Creative.ObjectStorySpec };
        }

        var response = await _apiClient.PostAsync<dynamic>($"/act_{NormalizeActId(request.AccountId)}/ads", body, accessToken);
        if (response == null) return Result<CreateAdResponse>.Fail("No response from Facebook API.");

        string adId = response?.id?.ToString() ?? string.Empty;

        await _adRepo.SaveAdAsync(new FacebookAd
        {
            AdId = adId,
            AdSetId = request.AdSetId,
            AccountId = request.AccountId,
            Name = request.Name,
            Status = request.Status,
            CreativeId = request.Creative?.CreativeId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        await _adRepo.SaveApiLogAsync(new FacebookApiLog
        {
            ApiName = "CreateAd",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<CreateAdResponse>.Ok(new CreateAdResponse { Id = adId });
    }

    public async Task<Result<AdResponse>> GetAdAsync(string adId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<AdResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,adset_id,campaign_id,status,creative,created_time,updated_time,bid_amount,bid_type",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{adId}", queryParams);
        if (response == null) return Result<AdResponse>.Fail("No response from Facebook API.");

        var ad = JsonConvert.DeserializeObject<AdResponse>(JsonConvert.SerializeObject(response)) ?? new AdResponse();
        return Result<AdResponse>.Ok(ad);
    }

    public async Task<Result<GetAdsResponse>> GetAdsAsync(GetAdsRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetAdsResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = request.Fields,
            ["limit"] = request.Limit.ToString(),
            ["access_token"] = accessToken
        };
        if (request.Status != null) queryParams["effective_status"] = $"[\"{request.Status}\"]";
        if (request.After != null) queryParams["after"] = request.After;

        var response = await _apiClient.GetAsync<dynamic>($"/{request.AdSetId}/ads", queryParams);
        if (response == null) return Result<GetAdsResponse>.Fail("No response from Facebook API.");

        var ads = JsonConvert.DeserializeObject<List<AdResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<AdResponse>();

        return Result<GetAdsResponse>.Ok(new GetAdsResponse { Data = ads });
    }

    public async Task<Result<bool>> UpdateAdAsync(UpdateAdRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?>();
        if (request.Name != null) body["name"] = request.Name;
        if (request.Status != null) body["status"] = request.Status;

        var response = await _apiClient.PostAsync<dynamic>($"/{request.AdId}", body, accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> DeleteAdAsync(string adId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var response = await _apiClient.DeleteAsync<dynamic>($"/{adId}", accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        await _adRepo.DeleteAdAsync(adId);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<AdCreativeResponse>> CreateAdCreativeAsync(AdCreativeRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<AdCreativeResponse>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?>
        {
            ["name"] = request.Name
        };
        if (request.ObjectStorySpec != null) body["object_story_spec"] = request.ObjectStorySpec;

        var response = await _apiClient.PostAsync<dynamic>($"/act_{NormalizeActId(request.AccountId)}/adcreatives", body, accessToken);
        if (response == null) return Result<AdCreativeResponse>.Fail("No response from Facebook API.");

        return Result<AdCreativeResponse>.Ok(new AdCreativeResponse { Id = response?.id?.ToString() ?? string.Empty });
    }

    public async Task<Result<object>> GetAdPreviewAsync(string adId, string adFormat, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<object>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["ad_format"] = adFormat,
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<object>($"/{adId}/previews", queryParams);
        if (response == null) return Result<object>.Fail("No response from Facebook API.");

        return Result<object>.Ok(response);
    }

    public async Task<Result<object>> GetAdCreativesAsync(string accountId, string accessToken, int limit)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<object>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,object_story_spec,effective_object_story_id,status",
            ["limit"] = limit.ToString(),
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<object>($"/act_{NormalizeActId(accountId)}/adcreatives", queryParams);
        if (response == null) return Result<object>.Fail("No response from Facebook API.");

        return Result<object>.Ok(response);
    }
}


