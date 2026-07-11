using ApiToolKit.Application.DTOs.GoogleAds.BiddingStrategy;
using ApiToolKit.Application.DTOs.GoogleAds.Campaign;
using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.GoogleAds;
using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.Services.GoogleAds;

public class GoogleAdsBiddingStrategyService : IGoogleAdsBiddingStrategyService
{
    private readonly IGoogleAdsAuthRepository _authRepo;
    private readonly IGoogleAdsApiClient _apiClient;

    public GoogleAdsBiddingStrategyService(IGoogleAdsAuthRepository authRepo, IGoogleAdsApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<List<BiddingStrategyResponse>>> ListBiddingStrategiesAsync(ListBiddingStrategiesRequest request)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(request.CustomerId);
        if (token == null)
            return Result<List<BiddingStrategyResponse>>.Fail("No token found for customer. Please authenticate first.");

        var query = "SELECT bidding_strategy.resource_name, bidding_strategy.id, bidding_strategy.name, " +
                    "bidding_strategy.type, bidding_strategy.status, bidding_strategy.campaign_count, " +
                    "bidding_strategy.non_removed_campaign_count FROM bidding_strategy";

        var response = await _apiClient.SearchAsync<JObject>(request.CustomerId, query, token.AccessToken);
        if (response == null)
            return Result<List<BiddingStrategyResponse>>.Fail("No response from Google Ads API.");

        var strategies = new List<BiddingStrategyResponse>();
        var results = response["results"] as JArray ?? new JArray();
        foreach (var row in results)
        {
            var bs = row["biddingStrategy"];
            if (bs == null) continue;
            strategies.Add(new BiddingStrategyResponse
            {
                ResourceName = bs["resourceName"]?.ToString() ?? string.Empty,
                Id = bs["id"]?.Value<long>() ?? 0,
                Name = bs["name"]?.ToString() ?? string.Empty,
                Type = bs["type"]?.ToString() ?? string.Empty,
                Status = bs["status"]?.ToString() ?? string.Empty,
                CampaignCount = bs["campaignCount"]?.Value<int>() ?? 0,
                NonRemovedCampaignCount = bs["nonRemovedCampaignCount"]?.Value<int>() ?? 0
            });
        }

        return Result<List<BiddingStrategyResponse>>.Ok(strategies);
    }

    public async Task<Result<MutateOperationResponse>> CreateBiddingStrategyAsync(CreateBiddingStrategyRequest request)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(request.CustomerId);
        if (token == null)
            return Result<MutateOperationResponse>.Fail("No token found for customer. Please authenticate first.");

        object biddingScheme = request.Type switch
        {
            "TARGET_CPA" => new { targetCpa = new { targetCpaMicros = request.TargetCpaMicros ?? 1000000L } },
            "TARGET_ROAS" => new { targetRoas = new { targetRoas = request.TargetRoas ?? 1.0 } },
            "MAXIMIZE_CONVERSIONS" => new { maximizeConversions = new object() },
            _ => new { maximizeConversions = new object() }
        };

        var operations = new object[]
        {
            new
            {
                create = new
                {
                    name = request.Name,
                    @type = $"portfolioBiddingStrategy",
                    scheme = biddingScheme
                }
            }
        };

        var response = await _apiClient.MutateAsync<JObject>(request.CustomerId, "biddingStrategies", new { operations }, token.AccessToken);
        if (response == null)
            return Result<MutateOperationResponse>.Fail("No response from Google Ads API.");

        var resourceName = response["results"]?[0]?["resourceName"]?.ToString() ?? string.Empty;
        return Result<MutateOperationResponse>.Ok(new MutateOperationResponse { ResourceName = resourceName, Index = 0 });
    }
}
