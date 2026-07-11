using ApiToolKit.Application.DTOs.GoogleAds.Campaign;
using ApiToolKit.Application.DTOs.GoogleAds.Conversion;
using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.GoogleAds;
using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.Services.GoogleAds;

public class GoogleAdsConversionService : IGoogleAdsConversionService
{
    private readonly IGoogleAdsAuthRepository _authRepo;
    private readonly IGoogleAdsApiClient _apiClient;

    public GoogleAdsConversionService(IGoogleAdsAuthRepository authRepo, IGoogleAdsApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<List<ConversionActionResponse>>> ListConversionActionsAsync(ListConversionActionsRequest request)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(request.CustomerId);
        if (token == null)
            return Result<List<ConversionActionResponse>>.Fail("No token found for customer. Please authenticate first.");

        var query = "SELECT conversion_action.resource_name, conversion_action.id, conversion_action.name, " +
                    "conversion_action.type, conversion_action.status, conversion_action.category, " +
                    "conversion_action.counting_type, conversion_action.value_settings FROM conversion_action";

        var response = await _apiClient.SearchAsync<JObject>(request.CustomerId, query, token.AccessToken);
        if (response == null)
            return Result<List<ConversionActionResponse>>.Fail("No response from Google Ads API.");

        var conversions = new List<ConversionActionResponse>();
        var results = response["results"] as JArray ?? new JArray();
        foreach (var row in results)
        {
            var ca = row["conversionAction"];
            if (ca == null) continue;
            conversions.Add(new ConversionActionResponse
            {
                ResourceName = ca["resourceName"]?.ToString() ?? string.Empty,
                Id = ca["id"]?.Value<long>() ?? 0,
                Name = ca["name"]?.ToString() ?? string.Empty,
                Type = ca["type"]?.ToString() ?? string.Empty,
                Status = ca["status"]?.ToString() ?? string.Empty,
                Category = ca["category"]?.ToString() ?? string.Empty,
                CountingType = ca["countingType"]?.ToString() ?? string.Empty,
                ValueSettings = ca["valueSettings"]?.ToObject<object>()
            });
        }

        return Result<List<ConversionActionResponse>>.Ok(conversions);
    }

    public async Task<Result<MutateOperationResponse>> CreateConversionActionAsync(CreateConversionActionRequest request)
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
                    type = request.Type,
                    category = request.Category,
                    countingType = request.CountingType,
                    status = "ENABLED"
                }
            }
        };

        var response = await _apiClient.MutateAsync<JObject>(request.CustomerId, "conversionActions", new { operations }, token.AccessToken);
        if (response == null)
            return Result<MutateOperationResponse>.Fail("No response from Google Ads API.");

        var resourceName = response["results"]?[0]?["resourceName"]?.ToString() ?? string.Empty;
        return Result<MutateOperationResponse>.Ok(new MutateOperationResponse { ResourceName = resourceName, Index = 0 });
    }
}
