using ApiToolKit.Application.DTOs.GoogleAds.Budget;
using ApiToolKit.Application.DTOs.GoogleAds.Campaign;
using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.GoogleAds;
using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.Services.GoogleAds;

public class GoogleAdsBudgetService : IGoogleAdsBudgetService
{
    private readonly IGoogleAdsAuthRepository _authRepo;
    private readonly IGoogleAdsApiClient _apiClient;

    public GoogleAdsBudgetService(IGoogleAdsAuthRepository authRepo, IGoogleAdsApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<List<BudgetResponse>>> ListBudgetsAsync(ListBudgetsRequest request)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(request.CustomerId);
        if (token == null)
            return Result<List<BudgetResponse>>.Fail("No token found for customer. Please authenticate first.");

        var query = "SELECT campaign_budget.resource_name, campaign_budget.id, campaign_budget.name, " +
                    "campaign_budget.amount_micros, campaign_budget.status, campaign_budget.period, " +
                    "campaign_budget.explicitly_shared FROM campaign_budget";

        var response = await _apiClient.SearchAsync<JObject>(request.CustomerId, query, token.AccessToken);
        if (response == null)
            return Result<List<BudgetResponse>>.Fail("No response from Google Ads API.");

        var budgets = new List<BudgetResponse>();
        var results = response["results"] as JArray ?? new JArray();
        foreach (var row in results)
        {
            var b = row["campaignBudget"];
            if (b == null) continue;
            budgets.Add(new BudgetResponse
            {
                ResourceName = b["resourceName"]?.ToString() ?? string.Empty,
                Id = b["id"]?.Value<long>() ?? 0,
                Name = b["name"]?.ToString() ?? string.Empty,
                AmountMicros = b["amountMicros"]?.Value<long>() ?? 0,
                Status = b["status"]?.ToString() ?? string.Empty,
                Period = b["period"]?.ToString() ?? string.Empty,
                ExplicitlyShared = b["explicitlyShared"]?.Value<bool>() ?? false
            });
        }

        return Result<List<BudgetResponse>>.Ok(budgets);
    }

    public async Task<Result<MutateOperationResponse>> CreateBudgetAsync(CreateBudgetRequest request)
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
                    amountMicros = request.AmountMicros,
                    period = request.Period,
                    explicitlyShared = request.ExplicitlyShared,
                    deliveryMethod = "STANDARD"
                }
            }
        };

        var response = await _apiClient.MutateAsync<JObject>(request.CustomerId, "campaignBudgets", new { operations }, token.AccessToken);
        if (response == null)
            return Result<MutateOperationResponse>.Fail("No response from Google Ads API.");

        var resourceName = response["results"]?[0]?["resourceName"]?.ToString() ?? string.Empty;
        return Result<MutateOperationResponse>.Ok(new MutateOperationResponse { ResourceName = resourceName, Index = 0 });
    }
}
