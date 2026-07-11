using ApiToolKit.Application.DTOs.GoogleAds.Customer;
using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.GoogleAds;
using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.Services.GoogleAds;

public class GoogleAdsCustomerService : IGoogleAdsCustomerService
{
    private readonly IGoogleAdsAuthRepository _authRepo;
    private readonly IGoogleAdsApiClient _apiClient;
    private const string BaseUrl = "https://googleads.googleapis.com/v18";

    public GoogleAdsCustomerService(IGoogleAdsAuthRepository authRepo, IGoogleAdsApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<CustomerResponse>> GetCustomerAsync(GetCustomerRequest request)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(request.CustomerId);
        if (token == null)
            return Result<CustomerResponse>.Fail("No token found for customer. Please authenticate first.");

        var url = $"{BaseUrl}/customers/{request.CustomerId}";
        var response = await _apiClient.GetAsync<JObject>(url, token.AccessToken);
        if (response == null)
            return Result<CustomerResponse>.Fail("Customer not found.");

        var customer = new CustomerResponse
        {
            ResourceName = response["resourceName"]?.ToString() ?? string.Empty,
            Id = response["id"]?.Value<long>() ?? 0,
            DescriptiveName = response["descriptiveName"]?.ToString() ?? string.Empty,
            CurrencyCode = response["currencyCode"]?.ToString() ?? string.Empty,
            TimeZone = response["timeZone"]?.ToString() ?? string.Empty,
            Status = response["status"]?.ToString() ?? string.Empty,
            TestAccount = response["testAccount"]?.Value<bool>() ?? false
        };

        return Result<CustomerResponse>.Ok(customer);
    }

    public async Task<Result<ListAccessibleCustomersResponse>> ListAccessibleCustomersAsync(string accessToken)
    {
        var url = $"{BaseUrl}/customers:listAccessibleCustomers";
        var response = await _apiClient.GetAsync<JObject>(url, accessToken);
        if (response == null)
            return Result<ListAccessibleCustomersResponse>.Fail("No response from Google Ads API.");

        var resourceNames = response["resourceNames"]?
            .Select(r => r.ToString())
            .ToList() ?? new List<string>();

        return Result<ListAccessibleCustomersResponse>.Ok(new ListAccessibleCustomersResponse
        {
            ResourceNames = resourceNames
        });
    }
}
