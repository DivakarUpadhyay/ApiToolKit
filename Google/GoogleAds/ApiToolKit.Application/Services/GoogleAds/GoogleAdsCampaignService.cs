using ApiToolKit.Application.DTOs.GoogleAds.Campaign;
using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.GoogleAds;
using ApiToolKit.Domain.Interfaces.Repositories.GoogleAds;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.Services.GoogleAds;

public class GoogleAdsCampaignService : IGoogleAdsCampaignService
{
    private readonly IGoogleAdsAuthRepository _authRepo;
    private readonly IGoogleAdsCampaignRepository _campaignRepo;
    private readonly IGoogleAdsApiClient _apiClient;
    private const string BaseUrl = GoogleAdsApiVersion.BaseUrl;

    public GoogleAdsCampaignService(
        IGoogleAdsAuthRepository authRepo,
        IGoogleAdsCampaignRepository campaignRepo,
        IGoogleAdsApiClient apiClient)
    {
        _authRepo = authRepo;
        _campaignRepo = campaignRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<CampaignListResponse>> ListCampaignsAsync(ListCampaignsRequest request)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(request.CustomerId);
        if (token == null)
            return Result<CampaignListResponse>.Fail("No token found for customer. Please authenticate first.");

        var query = "SELECT campaign.resource_name, campaign.id, campaign.name, campaign.status, " +
                    "campaign.advertising_channel_type, campaign.bidding_strategy_type, " +
                    "campaign_budget.amount_micros, campaign.start_date, campaign.end_date, " +
                    "campaign.serving_status FROM campaign ORDER BY campaign.id";

        var response = await _apiClient.SearchAsync<JObject>(request.CustomerId, query, token.AccessToken);
        if (response == null)
            return Result<CampaignListResponse>.Fail("No response from Google Ads API.");

        var campaigns = new List<CampaignResponse>();
        var results = response["results"] as JArray ?? new JArray();
        foreach (var row in results)
        {
            var c = row["campaign"];
            if (c == null) continue;
            var campaign = new CampaignResponse
            {
                ResourceName = c["resourceName"]?.ToString() ?? string.Empty,
                Id = c["id"]?.Value<long>() ?? 0,
                Name = c["name"]?.ToString() ?? string.Empty,
                Status = c["status"]?.ToString() ?? string.Empty,
                AdvertisingChannelType = c["advertisingChannelType"]?.ToString() ?? string.Empty,
                BiddingStrategyType = c["biddingStrategyType"]?.ToString() ?? string.Empty,
                StartDate = c["startDate"]?.ToString() ?? string.Empty,
                EndDate = c["endDate"]?.ToString() ?? string.Empty,
                ServingStatus = c["servingStatus"]?.ToString() ?? string.Empty,
                BudgetAmountMicros = row["campaignBudget"]?["amountMicros"]?.Value<long>() ?? 0
            };
            campaigns.Add(campaign);

            await _campaignRepo.SaveCampaignAsync(new GoogleAdsCampaign
            {
                ResourceName = campaign.ResourceName,
                CustomerId = request.CustomerId,
                Name = campaign.Name,
                Status = campaign.Status,
                AdvertisingChannelType = campaign.AdvertisingChannelType,
                BiddingStrategyType = campaign.BiddingStrategyType,
                BudgetAmountMicros = campaign.BudgetAmountMicros,
                StartDate = campaign.StartDate,
                EndDate = campaign.EndDate,
                CreatedAt = DateTime.UtcNow
            });
        }

        return Result<CampaignListResponse>.Ok(new CampaignListResponse
        {
            Campaigns = campaigns,
            NextPageToken = response["nextPageToken"]?.ToString()
        });
    }

    public async Task<Result<CampaignResponse>> GetCampaignAsync(string customerId, string resourceName)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(customerId);
        if (token == null)
            return Result<CampaignResponse>.Fail("No token found for customer. Please authenticate first.");

        var encodedResourceName = Uri.EscapeDataString(resourceName);
        var url = $"{BaseUrl}/{resourceName}";
        var response = await _apiClient.GetAsync<JObject>(url, token.AccessToken);
        if (response == null)
            return Result<CampaignResponse>.Fail("Campaign not found.");

        var campaign = new CampaignResponse
        {
            ResourceName = response["resourceName"]?.ToString() ?? string.Empty,
            Id = response["id"]?.Value<long>() ?? 0,
            Name = response["name"]?.ToString() ?? string.Empty,
            Status = response["status"]?.ToString() ?? string.Empty,
            AdvertisingChannelType = response["advertisingChannelType"]?.ToString() ?? string.Empty,
            BiddingStrategyType = response["biddingStrategyType"]?.ToString() ?? string.Empty,
            StartDate = response["startDate"]?.ToString() ?? string.Empty,
            EndDate = response["endDate"]?.ToString() ?? string.Empty,
            ServingStatus = response["servingStatus"]?.ToString() ?? string.Empty
        };

        return Result<CampaignResponse>.Ok(campaign);
    }

    public async Task<Result<MutateOperationResponse>> CreateCampaignAsync(CreateCampaignRequest request)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(request.CustomerId);
        if (token == null)
            return Result<MutateOperationResponse>.Fail("No token found for customer. Please authenticate first.");

        var budgetOperation = new
        {
            campaignBudgetOperation = new
            {
                create = new
                {
                    name = $"Budget for {request.Name}",
                    amountMicros = request.BudgetAmountMicros,
                    deliveryMethod = "STANDARD"
                }
            }
        };

        var budgetUrl = $"{BaseUrl}/customers/{request.CustomerId}/campaignBudgets:mutate";
        var budgetResponse = await _apiClient.PostAsync<JObject>(budgetUrl, new { operations = new[] { budgetOperation } }, token.AccessToken, request.CustomerId);

        var budgetResourceName = budgetResponse?["results"]?[0]?["resourceName"]?.ToString() ?? string.Empty;

        var operations = new object[]
        {
            new
            {
                create = new
                {
                    name = request.Name,
                    advertisingChannelType = request.AdvertisingChannelType,
                    status = "PAUSED",
                    campaignBudget = budgetResourceName,
                    startDate = string.IsNullOrEmpty(request.StartDate) ? DateTime.UtcNow.ToString("yyyyMMdd") : request.StartDate,
                    endDate = request.EndDate,
                    targetCpa = request.BiddingStrategyType == "TARGET_CPA" ? new object() : null,
                    // Required by Google for every new campaign (EU political ads self-declaration).
                    containsEuPoliticalAdvertising = request.ContainsEuPoliticalAdvertising
                }
            }
        };

        var response = await _apiClient.MutateAsync<JObject>(request.CustomerId, "campaigns", new { operations }, token.AccessToken);
        if (response == null)
            return Result<MutateOperationResponse>.Fail("No response from Google Ads API.");

        var resourceName = response["results"]?[0]?["resourceName"]?.ToString() ?? string.Empty;
        return Result<MutateOperationResponse>.Ok(new MutateOperationResponse { ResourceName = resourceName, Index = 0 });
    }

    public async Task<Result<MutateOperationResponse>> UpdateCampaignAsync(UpdateCampaignRequest request)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(request.CustomerId);
        if (token == null)
            return Result<MutateOperationResponse>.Fail("No token found for customer. Please authenticate first.");

        var updateFields = new Dictionary<string, object> { ["resourceName"] = request.ResourceName };
        var updateMask = new List<string>();

        if (!string.IsNullOrEmpty(request.Name)) { updateFields["name"] = request.Name; updateMask.Add("name"); }
        if (!string.IsNullOrEmpty(request.Status)) { updateFields["status"] = request.Status; updateMask.Add("status"); }

        var operations = new object[]
        {
            new
            {
                update = updateFields,
                updateMask = string.Join(",", updateMask)
            }
        };

        var response = await _apiClient.MutateAsync<JObject>(request.CustomerId, "campaigns", new { operations }, token.AccessToken);
        if (response == null)
            return Result<MutateOperationResponse>.Fail("No response from Google Ads API.");

        var resourceName = response["results"]?[0]?["resourceName"]?.ToString() ?? string.Empty;
        return Result<MutateOperationResponse>.Ok(new MutateOperationResponse { ResourceName = resourceName, Index = 0 });
    }

    public async Task<Result<bool>> RemoveCampaignAsync(string customerId, string resourceName)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(customerId);
        if (token == null)
            return Result<bool>.Fail("No token found for customer. Please authenticate first.");

        var operations = new object[] { new { remove = resourceName } };
        var response = await _apiClient.MutateAsync<JObject>(customerId, "campaigns", new { operations }, token.AccessToken);
        if (response == null)
            return Result<bool>.Fail("No response from Google Ads API.");

        return Result<bool>.Ok(true);
    }
}
