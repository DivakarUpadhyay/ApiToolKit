using System.ComponentModel.DataAnnotations;
using ApiToolKit.Application.DTOs.Analytics.Reports;

namespace ApiToolKit.Application.DTOs.Analytics.FunnelReport;

public class FunnelStep
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public FilterExpression FilterExpression { get; set; } = new();
    public bool IsDirectlyFollowedBy { get; set; } = false;
}

public class FunnelDefinition
{
    public List<FunnelStep> Steps { get; set; } = new();
}

public class FunnelNextAction
{
    public FilterExpression? DrillDownFilter { get; set; }
}

public class RunFunnelReportRequest
{
    [Required]
    public string PropertyId { get; set; } = string.Empty;
    [Required]
    public List<DateRange> DateRanges { get; set; } = new();
    public FunnelDefinition? Funnel { get; set; }
    public Dimension? FunnelBreakdown { get; set; }
    public FunnelNextAction? FunnelNextAction { get; set; }
    public object? Segments { get; set; }
    public long Limit { get; set; } = 10000;
}

public class FunnelSubReport
{
    public List<DimensionHeader> DimensionHeaders { get; set; } = new();
    public List<MetricHeader> MetricHeaders { get; set; } = new();
    public List<Row> Rows { get; set; } = new();
    public List<Row> Totals { get; set; } = new();
    public List<Row> Maximums { get; set; } = new();
    public List<Row> Minimums { get; set; } = new();
}

public class RunFunnelReportResponse
{
    public FunnelSubReport? FunnelTable { get; set; }
    public FunnelSubReport? FunnelVisualization { get; set; }
    public object? PropertyQuota { get; set; }
    public string? Kind { get; set; }
}
