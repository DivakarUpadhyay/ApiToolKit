using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.GoogleAds.Report;

public class RunReportRequest
{
    [Required] public string CustomerId { get; set; } = string.Empty;
    [Required] public string Query { get; set; } = string.Empty;
    public int PageSize { get; set; } = 1000;
    public string? PageToken { get; set; }
}

public class ReportRow
{
    public Dictionary<string, object> Fields { get; set; } = new();
}

public class ReportResponse
{
    public List<ReportRow> Rows { get; set; } = new();
    public string? NextPageToken { get; set; }
    public long TotalResultsCount { get; set; }
}
