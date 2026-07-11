using ApiToolKit.Application.DTOs.GoogleAds.AdGroup;
using ApiToolKit.Application.DTOs.GoogleAds.Campaign;
using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.GoogleAds;
using ApiToolKit.Domain.Interfaces.Repositories.GoogleAds;
using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.Services.GoogleAds;

public class GoogleAdsAdGroupService : IGoogleAdsAdGroupService
{
    private readonly IGoogleAdsAuthRepository _authRepo;
    private readonly IGoogleAdsAdGroupRepository _adGroupRepo;
    private readonly IGoogleAdsApiClient _apiClient;
    private const string BaseUrl = "https://googleads.googleapis.com/v18";

    public GoogleAdsAdGroupService(
        IGoogleAdsAuthRepository authRepo,
        IGoogleAdsAdGroupRepository adGroupRepo,
        IGoogleAdsApiClient apiClient)
    {
        _authRepo = authRepo;
        _adGroupRepo = adGroupRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<AdGroupListResponse>> ListAdGroupsAsync(ListAdGroupsRequest request)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(request.CustomerId);
        if (token == null)
            return Result<AdGroupListResponse>.Fail("No token found for customer. Please authenticate first.");

        var query = "SELECT ad_group.resource_name, ad_group.id, ad_group.name, ad_group.status, " +
                    "ad_group.campaign, ad_group.type, ad_group.cpc_bid_micros FROM ad_group";

        if (!string.IsNullOrEmpty(request.CampaignResourceName))
            query += $" WHERE ad_group.campaign = '{request.CampaignResourceName}'";

        var response = await _apiClient.SearchAsync<JObject>(request.CustomerId, query, token.AccessToken);
        if (response == null)
            return Result<AdGroupListResponse>.Fail("No response from Google Ads API.");

        var adGroups = new List<AdGroupResponse>();
        var results = response["results"] as JArray ?? new JArray();
        foreach (var row in results)
        {
            var ag = row["adGroup"];
            if (ag == null) continue;
            var adGroup = new AdGroupResponse
            {
                ResourceName = ag["resourceName"]?.ToString() ?? string.Empty,
                Id = ag["id"]?.Value<long>() ?? 0,
                Name = ag["name"]?.ToString() ?? string.Empty,
                Status = ag["status"]?.ToString() ?? string.Empty,
                CampaignResourceName = ag["campaign"]?.ToString() ?? string.Empty,
                Type = ag["type"]?.ToString() ?? string.Empty,
                CpcBidMicros = ag["cpcBidMicros"]?.Value<long>() ?? 0
            };
            adGroups.Add(adGroup);

            await _adGroupRepo.SaveAdGroupAsync(new GoogleAdsAdGroup
            {
                ResourceName = adGroup.ResourceName,
                CustomerId = request.CustomerId,
                CampaignResourceName = adGroup.CampaignResourceName,
                Name = adGroup.Name,
                Status = adGroup.Status,
                Type = adGroup.Type,
                CpcBidMicros = adGroup.CpcBidMicros,
                CreatedAt = DateTime.UtcNow
            });
        }

        return Result<AdGroupListResponse>.Ok(new AdGroupListResponse
        {
            AdGroups = adGroups,
            NextPageToken = response["nextPageToken"]?.ToString()
        });
    }

    public async Task<Result<AdGroupResponse>> GetAdGroupAsync(string customerId, string resourceName)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(customerId);
        if (token == null)
            return Result<AdGroupResponse>.Fail("No token found for customer. Please authenticate first.");

        var url = $"{BaseUrl}/{resourceName}";
        var response = await _apiClient.GetAsync<JObject>(url, token.AccessToken);
        if (response == null)
            return Result<AdGroupResponse>.Fail("Ad group not found.");

        var adGroup = new AdGroupResponse
        {
            ResourceName = response["resourceName"]?.ToString() ?? string.Empty,
            Id = response["id"]?.Value<long>() ?? 0,
            Name = response["name"]?.ToString() ?? string.Empty,
            Status = response["status"]?.ToString() ?? string.Empty,
            CampaignResourceName = response["campaign"]?.ToString() ?? string.Empty,
            Type = response["type"]?.ToString() ?? string.Empty,
            CpcBidMicros = response["cpcBidMicros"]?.Value<long>() ?? 0
        };

        return Result<AdGroupResponse>.Ok(adGroup);
    }

    public async Task<Result<MutateOperationResponse>> CreateAdGroupAsync(CreateAdGroupRequest request)
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
                    name = request.Name,
                    campaign = request.CampaignResourceName,
                    type = request.Type,
                    status = "ENABLED",
                    cpcBidMicros = request.CpcBidMicros
                }
            }
        };

        var response = await _apiClient.MutateAsync<JObject>(request.CustomerId, "adGroups", new { operations }, token.AccessToken);
        if (response == null)
            return Result<MutateOperationResponse>.Fail("No response from Google Ads API.");

        var resourceName = response["results"]?[0]?["resourceName"]?.ToString() ?? string.Empty;
        return Result<MutateOperationResponse>.Ok(new MutateOperationResponse { ResourceName = resourceName, Index = 0 });
    }

    public async Task<Result<MutateOperationResponse>> UpdateAdGroupAsync(UpdateAdGroupRequest request)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(request.CustomerId);
        if (token == null)
            return Result<MutateOperationResponse>.Fail("No token found for customer. Please authenticate first.");

        var updateFields = new Dictionary<string, object> { ["resourceName"] = request.ResourceName };
        var updateMask = new List<string>();

        if (!string.IsNullOrEmpty(request.Name)) { updateFields["name"] = request.Name; updateMask.Add("name"); }
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

        var response = await _apiClient.MutateAsync<JObject>(request.CustomerId, "adGroups", new { operations }, token.AccessToken);
        if (response == null)
            return Result<MutateOperationResponse>.Fail("No response from Google Ads API.");

        var resourceName = response["results"]?[0]?["resourceName"]?.ToString() ?? string.Empty;
        return Result<MutateOperationResponse>.Ok(new MutateOperationResponse { ResourceName = resourceName, Index = 0 });
    }

    public async Task<Result<bool>> RemoveAdGroupAsync(string customerId, string resourceName)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(customerId);
        if (token == null)
            return Result<bool>.Fail("No token found for customer. Please authenticate first.");

        var operations = new object[] { new { remove = resourceName } };
        var response = await _apiClient.MutateAsync<JObject>(customerId, "adGroups", new { operations }, token.AccessToken);
        if (response == null)
            return Result<bool>.Fail("No response from Google Ads API.");

        return Result<bool>.Ok(true);
    }
}
