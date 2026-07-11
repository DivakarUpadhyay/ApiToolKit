using ApiToolKit.Application.DTOs.GoogleAds.Report;
using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.GoogleAds;
using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.Services.GoogleAds;

public class GoogleAdsReportService : IGoogleAdsReportService
{
    private readonly IGoogleAdsAuthRepository _authRepo;
    private readonly IGoogleAdsApiClient _apiClient;

    public GoogleAdsReportService(IGoogleAdsAuthRepository authRepo, IGoogleAdsApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<ReportResponse>> RunReportAsync(RunReportRequest request)
    {
        var token = await _authRepo.GetTokenByCustomerIdAsync(request.CustomerId);
        if (token == null)
            return Result<ReportResponse>.Fail("No token found for customer. Please authenticate first.");

        var response = await _apiClient.SearchAsync<JObject>(request.CustomerId, request.Query, token.AccessToken);
        if (response == null)
            return Result<ReportResponse>.Fail("No response from Google Ads API.");

        var rows = new List<ReportRow>();
        var results = response["results"] as JArray ?? new JArray();
        foreach (var row in results)
        {
            var fields = new Dictionary<string, object>();
            if (row is JObject rowObj)
            {
                foreach (var prop in rowObj.Properties())
                {
                    fields[prop.Name] = prop.Value.ToObject<object>() ?? new object();
                }
            }
            rows.Add(new ReportRow { Fields = fields });
        }

        return Result<ReportResponse>.Ok(new ReportResponse
        {
            Rows = rows,
            NextPageToken = response["nextPageToken"]?.ToString(),
            TotalResultsCount = response["totalResultsCount"]?.Value<long>() ?? rows.Count
        });
    }
}
