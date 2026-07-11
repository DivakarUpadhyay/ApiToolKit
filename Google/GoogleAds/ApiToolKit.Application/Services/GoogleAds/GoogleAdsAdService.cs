using ApiToolKit.Application.DTOs.GoogleAds.Ad;
using ApiToolKit.Application.DTOs.GoogleAds.Campaign;
using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.GoogleAds;
using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.Services.GoogleAds;

public class GoogleAdsAdService : IGoogleAdsAdService
{
    private readonly IGoogleAdsAuthRepository _authRepo;
    private readonly IGoogleAdsApiClient _apiClient;

    public GoogleAdsAdService(IGoogleAdsAuthRepository authRepo, IGoogleAdsApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<AdListResponse>> ListAdsAsync(ListAdsRequest request)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(request.CustomerId);
        if (token == null)
            return Result<AdListResponse>.Fail("No token found for customer. Please authenticate first.");

        var query = "SELECT ad_group_ad.resource_name, ad_group_ad.ad.id, ad_group_ad.ad_group, " +
                    "ad_group_ad.ad.type, ad_group_ad.status, " +
                    "ad_group_ad.ad.responsive_search_ad.headlines, " +
                    "ad_group_ad.ad.responsive_search_ad.descriptions, " +
                    "ad_group_ad.ad.final_urls FROM ad_group_ad";

        if (!string.IsNullOrEmpty(request.AdGroupResourceName))
            query += $" WHERE ad_group_ad.ad_group = '{request.AdGroupResourceName}'";

        var response = await _apiClient.SearchAsync<JObject>(request.CustomerId, query, token.AccessToken);
        if (response == null)
            return Result<AdListResponse>.Fail("No response from Google Ads API.");

        var ads = new List<AdResponse>();
        var results = response["results"] as JArray ?? new JArray();
        foreach (var row in results)
        {
            var adGroupAd = row["adGroupAd"];
            if (adGroupAd == null) continue;
            var ad = adGroupAd["ad"];

            var headlines = new List<string>();
            var descriptions = new List<string>();
            var finalUrls = new List<string>();

            var rsa = ad?["responsiveSearchAd"];
            if (rsa != null)
            {
                headlines = rsa["headlines"]?.Select(h => h["text"]?.ToString() ?? string.Empty).ToList() ?? new List<string>();
                descriptions = rsa["descriptions"]?.Select(d => d["text"]?.ToString() ?? string.Empty).ToList() ?? new List<string>();
            }

            if (ad?["finalUrls"] is JArray urls)
                finalUrls = urls.Select(u => u.ToString()).ToList();

            ads.Add(new AdResponse
            {
                ResourceName = adGroupAd["resourceName"]?.ToString() ?? string.Empty,
                Id = ad?["id"]?.Value<long>() ?? 0,
                AdGroupResourceName = adGroupAd["adGroup"]?.ToString() ?? string.Empty,
                Type = ad?["type"]?.ToString() ?? string.Empty,
                Status = adGroupAd["status"]?.ToString() ?? string.Empty,
                Headlines = headlines,
                Descriptions = descriptions,
                FinalUrls = finalUrls
            });
        }

        return Result<AdListResponse>.Ok(new AdListResponse
        {
            Ads = ads,
            NextPageToken = response["nextPageToken"]?.ToString()
        });
    }

    public async Task<Result<MutateOperationResponse>> CreateResponsiveSearchAdAsync(CreateResponsiveSearchAdRequest request)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(request.CustomerId);
        if (token == null)
            return Result<MutateOperationResponse>.Fail("No token found for customer. Please authenticate first.");

        var headlines = request.Headlines.Select(h => new { text = h }).ToArray();
        var descriptions = request.Descriptions.Select(d => new { text = d }).ToArray();

        var operations = new object[]
        {
            new
            {
                create = new
                {
                    adGroup = request.AdGroupResourceName,
                    status = "ENABLED",
                    ad = new
                    {
                        finalUrls = request.FinalUrls,
                        responsiveSearchAd = new
                        {
                            headlines,
                            descriptions
                        }
                    }
                }
            }
        };

        var response = await _apiClient.MutateAsync<JObject>(request.CustomerId, "adGroupAds", new { operations }, token.AccessToken);
        if (response == null)
            return Result<MutateOperationResponse>.Fail("No response from Google Ads API.");

        var resourceName = response["results"]?[0]?["resourceName"]?.ToString() ?? string.Empty;
        return Result<MutateOperationResponse>.Ok(new MutateOperationResponse { ResourceName = resourceName, Index = 0 });
    }

    public async Task<Result<MutateOperationResponse>> UpdateAdAsync(UpdateAdRequest request)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(request.CustomerId);
        if (token == null)
            return Result<MutateOperationResponse>.Fail("No token found for customer. Please authenticate first.");

        var updateFields = new Dictionary<string, object> { ["resourceName"] = request.AdGroupAdResourceName };
        var updateMask = new List<string>();

        if (!string.IsNullOrEmpty(request.Status)) { updateFields["status"] = request.Status; updateMask.Add("status"); }

        var operations = new object[]
        {
            new
            {
                update = updateFields,
                updateMask = string.Join(",", updateMask)
            }
        };

        var response = await _apiClient.MutateAsync<JObject>(request.CustomerId, "adGroupAds", new { operations }, token.AccessToken);
        if (response == null)
            return Result<MutateOperationResponse>.Fail("No response from Google Ads API.");

        var resourceName = response["results"]?[0]?["resourceName"]?.ToString() ?? string.Empty;
        return Result<MutateOperationResponse>.Ok(new MutateOperationResponse { ResourceName = resourceName, Index = 0 });
    }

    public async Task<Result<bool>> RemoveAdAsync(string customerId, string resourceName)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(customerId);
        if (token == null)
            return Result<bool>.Fail("No token found for customer. Please authenticate first.");

        var operations = new object[] { new { remove = resourceName } };
        var response = await _apiClient.MutateAsync<JObject>(customerId, "adGroupAds", new { operations }, token.AccessToken);
        if (response == null)
            return Result<bool>.Fail("No response from Google Ads API.");

        return Result<bool>.Ok(true);
    }
}
