using ApiToolKit.Application.DTOs.YouTube.Analytics;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using ApiToolKit.Domain.Common;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.YouTube;

public class YouTubeAnalyticsService : IYouTubeAnalyticsService
{
    private readonly IYouTubeApiClient _apiClient;

    public YouTubeAnalyticsService(IYouTubeApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<Result<AnalyticsReportResponse>> RunReportAsync(RunAnalyticsReportRequest request, string accessToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["ids"] = request.Ids,
            ["startDate"] = request.StartDate,
            ["endDate"] = request.EndDate,
            ["metrics"] = request.Metrics,
            ["maxResults"] = request.MaxResults.ToString()
        };

        if (!string.IsNullOrEmpty(request.Dimensions))
            queryParams["dimensions"] = request.Dimensions;
        if (!string.IsNullOrEmpty(request.Filters))
            queryParams["filters"] = request.Filters;
        if (!string.IsNullOrEmpty(request.Sort))
            queryParams["sort"] = request.Sort;

        var response = await _apiClient.GetAsync<dynamic>("reports", accessToken, queryParams);
        if (response == null) return Result<AnalyticsReportResponse>.Fail("No response from YouTube Analytics API.");

        var columnHeaders = new List<AnalyticsRowHeader>();
        var headers = response?.columnHeaders;
        if (headers != null)
        {
            foreach (var h in headers)
            {
                columnHeaders.Add(new AnalyticsRowHeader
                {
                    ColumnType = (string?)h?.columnType ?? string.Empty,
                    DataType = (string?)h?.dataType ?? string.Empty,
                    Name = (string?)h?.name ?? string.Empty
                });
            }
        }

        var rows = new List<AnalyticsRow>();
        var rawRows = response?.rows;
        if (rawRows != null)
        {
            foreach (var row in rawRows)
            {
                var values = new List<string>();
                foreach (var val in row)
                    values.Add(val?.ToString() ?? string.Empty);
                rows.Add(new AnalyticsRow { Values = values });
            }
        }

        return Result<AnalyticsReportResponse>.Ok(new AnalyticsReportResponse
        {
            Kind = (string?)response?.kind ?? string.Empty,
            ColumnHeaders = columnHeaders,
            Rows = rows
        });
    }
}
