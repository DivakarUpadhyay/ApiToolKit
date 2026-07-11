namespace ApiToolKit.Domain.Entities.Analytics;

public class AnalyticsApiLog
{
    public long Id { get; set; }
    public string PropertyId { get; set; } = string.Empty;
    public string ReportType { get; set; } = string.Empty;
    public string? RequestBody { get; set; }
    public string? ResponseBody { get; set; }
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public DateTime CreatedAt { get; set; }
}
