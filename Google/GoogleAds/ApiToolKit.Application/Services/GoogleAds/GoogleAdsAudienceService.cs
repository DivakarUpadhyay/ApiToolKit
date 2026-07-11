using ApiToolKit.Application.DTOs.GoogleAds.Audience;
using ApiToolKit.Application.DTOs.GoogleAds.Campaign;
using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.GoogleAds;
using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.Services.GoogleAds;

public class GoogleAdsAudienceService : IGoogleAdsAudienceService
{
    private readonly IGoogleAdsAuthRepository _authRepo;
    private readonly IGoogleAdsApiClient _apiClient;

    public GoogleAdsAudienceService(IGoogleAdsAuthRepository authRepo, IGoogleAdsApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<List<AudienceResponse>>> ListAudiencesAsync(ListAudiencesRequest request)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(request.CustomerId);
        if (token == null)
            return Result<List<AudienceResponse>>.Fail("No token found for customer. Please authenticate first.");

        var query = "SELECT audience.resource_name, audience.id, audience.name, " +
                    "audience.description, audience.status FROM audience";

        var response = await _apiClient.SearchAsync<JObject>(request.CustomerId, query, token.AccessToken);
        if (response == null)
            return Result<List<AudienceResponse>>.Fail("No response from Google Ads API.");

        var audiences = new List<AudienceResponse>();
        var results = response["results"] as JArray ?? new JArray();
        foreach (var row in results)
        {
            var a = row["audience"];
            if (a == null) continue;
            audiences.Add(new AudienceResponse
            {
                ResourceName = a["resourceName"]?.ToString() ?? string.Empty,
                Id = a["id"]?.Value<long>() ?? 0,
                Name = a["name"]?.ToString() ?? string.Empty,
                Description = a["description"]?.ToString() ?? string.Empty,
                Status = a["status"]?.ToString() ?? string.Empty
            });
        }

        return Result<List<AudienceResponse>>.Ok(audiences);
    }

    public async Task<Result<MutateOperationResponse>> CreateAudienceAsync(CreateAudienceRequest request)
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
                    description = request.Description
                }
            }
        };

        var response = await _apiClient.MutateAsync<JObject>(request.CustomerId, "audiences", new { operations }, token.AccessToken);
        if (response == null)
            return Result<MutateOperationResponse>.Fail("No response from Google Ads API.");

        var resourceName = response["results"]?[0]?["resourceName"]?.ToString() ?? string.Empty;
        return Result<MutateOperationResponse>.Ok(new MutateOperationResponse { ResourceName = resourceName, Index = 0 });
    }
}
