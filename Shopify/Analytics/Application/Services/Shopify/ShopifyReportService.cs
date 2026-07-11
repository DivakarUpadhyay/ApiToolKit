using ApiToolKit.Application.DTOs.Shopify.Analytics;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Shopify;

public class ShopifyReportService : IShopifyReportService
{
    private readonly IShopifyAuthRepository _authRepo;
    private readonly IShopifyApiClient _apiClient;

    public ShopifyReportService(IShopifyAuthRepository authRepo, IShopifyApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ListReportsResponse>> ListReportsAsync(string shopDomain, int? limit = null, string? sinceId = null)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ListReportsResponse>.Fail($"No access token found for shop: {shopDomain}");

        var queryParams = new Dictionary<string, string>();
        if (limit.HasValue) queryParams["limit"] = limit.Value.ToString();
        if (!string.IsNullOrEmpty(sinceId)) queryParams["since_id"] = sinceId;

        var response = await _apiClient.GetAsync<ListReportsResponse>(
            shopDomain,
            "reports.json",
            queryParams.Count > 0 ? queryParams : null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "ListReports",
            RequestBody = JsonConvert.SerializeObject(queryParams),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ListReportsResponse>.Fail("No response from Shopify API.");

        return Result<ListReportsResponse>.Ok(response);
    }

    public async Task<Result<ReportResponse>> GetReportAsync(string shopDomain, string reportId)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ReportResponse>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<ReportResponse>(
            shopDomain,
            $"reports/{reportId}.json",
            null,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "GetReport",
            RequestBody = JsonConvert.SerializeObject(new { reportId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Report != null,
            StatusCode = response?.Report != null ? 200 : 404,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ReportResponse>.Fail("No response from Shopify API.");

        return Result<ReportResponse>.Ok(response);
    }

    public async Task<Result<int>> CountReportsAsync(string shopDomain)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<int>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.GetAsync<dynamic>(
            shopDomain,
            "reports/count.json",
            null,
            token.AccessToken);

        int count = 0;
        if (response != null)
            int.TryParse(response?.count?.ToString(), out count);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CountReports",
            RequestBody = null,
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response != null,
            StatusCode = response != null ? 200 : 500,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<int>.Fail("No response from Shopify API.");

        return Result<int>.Ok(count);
    }

    public async Task<Result<ReportResponse>> CreateReportAsync(string shopDomain, CreateReportRequest request)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ReportResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { report = request };

        var response = await _apiClient.PostAsync<ReportResponse>(
            shopDomain,
            "reports.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "CreateReport",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Report != null,
            StatusCode = response?.Report != null ? 201 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ReportResponse>.Fail("No response from Shopify API.");

        return Result<ReportResponse>.Ok(response);
    }

    public async Task<Result<ReportResponse>> UpdateReportAsync(string shopDomain, string reportId, UpdateReportRequest request)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<ReportResponse>.Fail($"No access token found for shop: {shopDomain}");

        var body = new { report = request };

        var response = await _apiClient.PutAsync<ReportResponse>(
            shopDomain,
            $"reports/{reportId}.json",
            body,
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "UpdateReport",
            RequestBody = JsonConvert.SerializeObject(body),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = response?.Report != null,
            StatusCode = response?.Report != null ? 200 : 422,
            CreatedAt = DateTime.UtcNow
        });

        if (response == null)
            return Result<ReportResponse>.Fail("No response from Shopify API.");

        return Result<ReportResponse>.Ok(response);
    }

    public async Task<Result<bool>> DeleteReportAsync(string shopDomain, string reportId)
    {
        var token = await _authRepo.GetTokenByShopAsync(shopDomain);
        if (token == null)
            return Result<bool>.Fail($"No access token found for shop: {shopDomain}");

        var response = await _apiClient.DeleteAsync<dynamic>(
            shopDomain,
            $"reports/{reportId}.json",
            token.AccessToken);

        await _authRepo.SaveApiLogAsync(new ShopifyApiLog
        {
            ShopDomain = shopDomain,
            ApiName = "DeleteReport",
            RequestBody = JsonConvert.SerializeObject(new { reportId }),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<bool>.Ok(true);
    }
}
