using System.ComponentModel.DataAnnotations;
using ApiToolKit.Application.DTOs.Analytics.Reports;

namespace ApiToolKit.Application.DTOs.Analytics.Realtime;

public class RunRealtimeReportRequest
{
    [Required]
    public string PropertyId { get; set; } = string.Empty;
    public List<Dimension> Dimensions { get; set; } = new();
    [Required]
    public List<Metric> Metrics { get; set; } = new();
    public FilterExpression? DimensionFilter { get; set; }
    public FilterExpression? MetricFilter { get; set; }
    public long Limit { get; set; } = 10000;
    public List<string> MetricAggregations { get; set; } = new();
    public List<OrderBy> OrderBys { get; set; } = new();
    public bool ReturnPropertyQuota { get; set; } = false;
}

public class RunRealtimeReportResponse
{
    public List<DimensionHeader> DimensionHeaders { get; set; } = new();
    public List<MetricHeader> MetricHeaders { get; set; } = new();
    public List<Row> Rows { get; set; } = new();
    public List<Row> Totals { get; set; } = new();
    public List<Row> Maximums { get; set; } = new();
    public List<Row> Minimums { get; set; } = new();
    public int RowCount { get; set; }
    public object? PropertyQuota { get; set; }
    public string? Kind { get; set; }
}
