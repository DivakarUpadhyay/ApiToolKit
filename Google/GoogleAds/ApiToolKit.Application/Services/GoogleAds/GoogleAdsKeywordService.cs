using ApiToolKit.Application.DTOs.GoogleAds.Campaign;
using ApiToolKit.Application.DTOs.GoogleAds.Keyword;
using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.GoogleAds;
using ApiToolKit.Domain.Interfaces.Repositories.GoogleAds;
using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.Services.GoogleAds;

public class GoogleAdsKeywordService : IGoogleAdsKeywordService
{
    private readonly IGoogleAdsAuthRepository _authRepo;
    private readonly IGoogleAdsKeywordRepository _keywordRepo;
    private readonly IGoogleAdsApiClient _apiClient;

    public GoogleAdsKeywordService(
        IGoogleAdsAuthRepository authRepo,
        IGoogleAdsKeywordRepository keywordRepo,
        IGoogleAdsApiClient apiClient)
    {
        _authRepo = authRepo;
        _keywordRepo = keywordRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<KeywordListResponse>> ListKeywordsAsync(ListKeywordsRequest request)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(request.CustomerId);
        if (token == null)
            return Result<KeywordListResponse>.Fail("No token found for customer. Please authenticate first.");

        var query = "SELECT ad_group_criterion.resource_name, ad_group_criterion.criterion_id, " +
                    "ad_group_criterion.ad_group, ad_group_criterion.keyword.text, " +
                    "ad_group_criterion.keyword.match_type, ad_group_criterion.status, " +
                    "ad_group_criterion.cpc_bid_micros, ad_group_criterion.quality_info.quality_score " +
                    "FROM ad_group_criterion WHERE ad_group_criterion.type = 'KEYWORD'";

        if (!string.IsNullOrEmpty(request.AdGroupResourceName))
            query += $" AND ad_group_criterion.ad_group = '{request.AdGroupResourceName}'";

        var response = await _apiClient.SearchAsync<JObject>(request.CustomerId, query, token.AccessToken);
        if (response == null)
            return Result<KeywordListResponse>.Fail("No response from Google Ads API.");

        var keywords = new List<KeywordResponse>();
        var results = response["results"] as JArray ?? new JArray();
        foreach (var row in results)
        {
            var criterion = row["adGroupCriterion"];
            if (criterion == null) continue;
            var keyword = new KeywordResponse
            {
                ResourceName = criterion["resourceName"]?.ToString() ?? string.Empty,
                Id = criterion["criterionId"]?.Value<long>() ?? 0,
                AdGroupResourceName = criterion["adGroup"]?.ToString() ?? string.Empty,
                Text = criterion["keyword"]?["text"]?.ToString() ?? string.Empty,
                MatchType = criterion["keyword"]?["matchType"]?.ToString() ?? string.Empty,
                Status = criterion["status"]?.ToString() ?? string.Empty,
                CpcBidMicros = criterion["cpcBidMicros"]?.Value<long>() ?? 0,
                QualityScore = criterion["qualityInfo"]?["qualityScore"]?.Value<int?>()
            };
            keywords.Add(keyword);

            await _keywordRepo.SaveKeywordAsync(new GoogleAdsKeyword
            {
                ResourceName = keyword.ResourceName,
                CustomerId = request.CustomerId,
                AdGroupResourceName = keyword.AdGroupResourceName,
                Text = keyword.Text,
                MatchType = keyword.MatchType,
                Status = keyword.Status,
                CpcBidMicros = keyword.CpcBidMicros,
                CreatedAt = DateTime.UtcNow
            });
        }

        return Result<KeywordListResponse>.Ok(new KeywordListResponse
        {
            Keywords = keywords,
            NextPageToken = response["nextPageToken"]?.ToString()
        });
    }

    public async Task<Result<MutateOperationResponse>> CreateKeywordAsync(CreateKeywordRequest request)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(request.CustomerId);
        if (token == null)
            return Result<MutateOperationResponse>.Fail("No token found for customer. Please authenticate first.");

        var operations = new object[]
        {
            new
            {
                create = new
                {
                    adGroup = request.AdGroupResourceName,
                    status = "ENABLED",
                    keyword = new
                    {
                        text = request.Text,
                        matchType = request.MatchType
                    },
                    cpcBidMicros = request.CpcBidMicros
                }
            }
        };

        var response = await _apiClient.MutateAsync<JObject>(request.CustomerId, "adGroupCriteria", new { operations }, token.AccessToken);
        if (response == null)
            return Result<MutateOperationResponse>.Fail("No response from Google Ads API.");

        var resourceName = response["results"]?[0]?["resourceName"]?.ToString() ?? string.Empty;
        return Result<MutateOperationResponse>.Ok(new MutateOperationResponse { ResourceName = resourceName, Index = 0 });
    }

    public async Task<Result<MutateOperationResponse>> UpdateKeywordAsync(UpdateKeywordRequest request)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(request.CustomerId);
        if (token == null)
            return Result<MutateOperationResponse>.Fail("No token found for customer. Please authenticate first.");

        var updateFields = new Dictionary<string, object> { ["resourceName"] = request.ResourceName };
        var updateMask = new List<string>();

        if (!string.IsNullOrEmpty(request.Status)) { updateFields["status"] = request.Status; updateMask.Add("status"); }
        if (request.CpcBidMicros.HasValue) { updateFields["cpcBidMicros"] = request.CpcBidMicros.Value; updateMask.Add("cpc_bid_micros"); }

        var operations = new object[]
        {
            new
            {
                update = updateFields,
                updateMask = string.Join(",", updateMask)
            }
        };

        var response = await _apiClient.MutateAsync<JObject>(request.CustomerId, "adGroupCriteria", new { operations }, token.AccessToken);
        if (response == null)
            return Result<MutateOperationResponse>.Fail("No response from Google Ads API.");

        var resourceName = response["results"]?[0]?["resourceName"]?.ToString() ?? string.Empty;
        return Result<MutateOperationResponse>.Ok(new MutateOperationResponse { ResourceName = resourceName, Index = 0 });
    }

    public async Task<Result<bool>> RemoveKeywordAsync(string customerId, string resourceName)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(customerId);
        if (token == null)
            return Result<bool>.Fail("No token found for customer. Please authenticate first.");

        var operations = new object[] { new { remove = resourceName } };
        var response = await _apiClient.MutateAsync<JObject>(customerId, "adGroupCriteria", new { operations }, token.AccessToken);
        if (response == null)
            return Result<bool>.Fail("No response from Google Ads API.");

        return Result<bool>.Ok(true);
    }
}
