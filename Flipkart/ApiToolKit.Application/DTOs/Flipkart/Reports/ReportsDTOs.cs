using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Flipkart.Reports;

public class RequestReportRequest
{
    [Required] public string ReportTypeIdentifier { get; set; } = string.Empty;
    public string? CorrelationId { get; set; }
    public Dictionary<string, object>? Parameters { get; set; }
}

public class GetReportRequest
{
    [Required] public string ReportId { get; set; } = string.Empty;
}

public class ReportRequestResponse { public string? ReportId { get; set; } }

public class ReportDetailResponse
{
    public string? ReportId { get; set; }
    public string? Status { get; set; }
    public string? Location { get; set; }
    public object? Metadata { get; set; }
}
