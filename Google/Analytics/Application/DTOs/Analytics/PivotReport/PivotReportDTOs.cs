using System.ComponentModel.DataAnnotations;
using ApiToolKit.Application.DTOs.Analytics.Reports;

namespace ApiToolKit.Application.DTOs.Analytics.PivotReport;

public class PivotDimension
{
    public string DimensionColumn { get; set; } = string.Empty;
    public long Offset { get; set; }
    public long Limit { get; set; }
}

public class RunPivotReportRequest
{
    [Required]
    public string PropertyId { get; set; } = string.Empty;
    [Required]
    public List<DateRange> DateRanges { get; set; } = new();
    [Required]
    public List<PivotDimension> Pivots { get; set; } = new();
    [Required]
    public List<Dimension> Dimensions { get; set; } = new();
    [Required]
    public List<Metric> Metrics { get; set; } = new();
    public FilterExpression? DimensionFilter { get; set; }
    public FilterExpression? MetricFilter { get; set; }
}

public class PivotDimensionHeader
{
    public List<DimensionValue> DimensionValues { get; set; } = new();
}

public class PivotHeader
{
    public List<PivotDimensionHeader> PivotDimensionHeaders { get; set; } = new();
    public int RowCount { get; set; }
}

public class RunPivotReportResponse
{
    public List<PivotHeader> PivotHeaders { get; set; } = new();
    public List<DimensionHeader> DimensionHeaders { get; set; } = new();
    public List<MetricHeader> MetricHeaders { get; set; } = new();
    public List<Row> Rows { get; set; } = new();
    public List<Row> Aggregates { get; set; } = new();
    public object? Metadata { get; set; }
    public object? PropertyQuota { get; set; }
}
