namespace ApiToolKit.Domain.Entities.Analytics;

public class AnalyticsSavedReport
{
    public long Id { get; set; }
    public string PropertyId { get; set; } = string.Empty;
    public string ReportName { get; set; } = string.Empty;
    public string? Query { get; set; }
    public string? ResponseJson { get; set; }
    public int RowCount { get; set; }
    public DateTime CreatedAt { get; set; }
}
