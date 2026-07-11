using ApiToolKit.Application.DTOs.Facebook.AdAccount;
using ApiToolKit.Application.DTOs.Facebook.Campaign;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookAdAccountService : IFacebookAdAccountService
{
    private readonly IFacebookAuthRepository _authRepo;
    private readonly IFacebookAdAccountRepository _adAccountRepo;
    private readonly IFacebookApiClient _apiClient;

    public FacebookAdAccountService(IFacebookAuthRepository authRepo, IFacebookAdAccountRepository adAccountRepo, IFacebookApiClient apiClient)
    {
        _authRepo = authRepo;
        _adAccountRepo = adAccountRepo;
        _apiClient = apiClient;
    }

    private static string NormalizeAccountId(string accountId) =>
        accountId.StartsWith("act_", StringComparison.OrdinalIgnoreCase) ? accountId[4..] : accountId;

    public async Task<Result<AdAccountResponse>> GetAdAccountAsync(string accountId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<AdAccountResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,account_id,name,currency,balance,account_status,timezone_name,business,disable_reason,funding_source",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/act_{NormalizeAccountId(accountId)}", queryParams);
        if (response == null) return Result<AdAccountResponse>.Fail("No response from Facebook API.");

        var account = JsonConvert.DeserializeObject<AdAccountResponse>(JsonConvert.SerializeObject(response)) ?? new AdAccountResponse();

        await _adAccountRepo.SaveAdAccountAsync(new FacebookAdAccount
        {
            AccountId = accountId,
            Name = account.Name,
            Currency = account.Currency,
            TimezoneName = account.TimezoneName,
            Balance = account.Balance,
            Status = account.Status,
            BusinessId = account.BusinessId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        await _adAccountRepo.SaveApiLogAsync(new FacebookApiLog
        {
            ApiName = "GetAdAccount",
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<AdAccountResponse>.Ok(account);
    }

    public async Task<Result<List<AdAccountResponse>>> GetAdAccountsByUserAsync(GetAdAccountsRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<List<AdAccountResponse>>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = request.Fields,
            ["limit"] = request.Limit.ToString(),
            ["access_token"] = accessToken
        };
        if (request.After != null) queryParams["after"] = request.After;

        var response = await _apiClient.GetAsync<dynamic>($"/{request.UserId}/adaccounts", queryParams);
        if (response == null) return Result<List<AdAccountResponse>>.Fail("No response from Facebook API.");

        var accounts = JsonConvert.DeserializeObject<List<AdAccountResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<AdAccountResponse>();

        return Result<List<AdAccountResponse>>.Ok(accounts);
    }

    public async Task<Result<AdAccountBillingResponse>> GetAdAccountBillingAsync(string accountId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<AdAccountBillingResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "balance,currency,min_campaign_group_spend_cap",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/act_{NormalizeAccountId(accountId)}", queryParams);
        if (response == null) return Result<AdAccountBillingResponse>.Fail("No response from Facebook API.");

        return Result<AdAccountBillingResponse>.Ok(new AdAccountBillingResponse
        {
            AccountId = accountId,
            Balance = response?.balance?.ToString() ?? string.Empty,
            Currency = response?.currency?.ToString() ?? string.Empty,
            MinimumBudget = response?.min_campaign_group_spend_cap?.ToString()
        });
    }

    public async Task<Result<List<AdAccountUserResponse>>> GetAdAccountUsersAsync(AdAccountUsersRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<List<AdAccountUserResponse>>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,role,permissions",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/act_{NormalizeAccountId(request.AccountId)}/users", queryParams);
        if (response == null) return Result<List<AdAccountUserResponse>>.Fail("No response from Facebook API.");

        var users = JsonConvert.DeserializeObject<List<AdAccountUserResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<AdAccountUserResponse>();

        return Result<List<AdAccountUserResponse>>.Ok(users);
    }

    public async Task<Result<GetCampaignsResponse>> GetAdAccountCampaignsAsync(GetCampaignsRequest request, string accessToken)
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

        var response = await _apiClient.GetAsync<dynamic>($"/act_{NormalizeAccountId(request.AccountId)}/campaigns", queryParams);
        if (response == null) return Result<GetCampaignsResponse>.Fail("No response from Facebook API.");

        var campaigns = JsonConvert.DeserializeObject<List<CampaignResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<CampaignResponse>();

        return Result<GetCampaignsResponse>.Ok(new GetCampaignsResponse { Data = campaigns });
    }

    public async Task<Result<object>> GetAdAccountCreativesAsync(string accountId, string accessToken, int limit)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<object>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,object_story_spec,effective_object_story_id,status",
            ["limit"] = limit.ToString(),
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<object>($"/act_{NormalizeAccountId(accountId)}/adcreatives", queryParams);
        if (response == null) return Result<object>.Fail("No response from Facebook API.");

        return Result<object>.Ok(response);
    }
}

