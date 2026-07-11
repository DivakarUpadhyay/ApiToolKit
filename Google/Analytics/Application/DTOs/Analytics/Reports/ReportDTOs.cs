using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Analytics.Reports;

public class Dimension
{
    public string Name { get; set; } = string.Empty;
}

public class Metric
{
    public string Name { get; set; } = string.Empty;
}

public class DateRange
{
    [Required]
    public string StartDate { get; set; } = string.Empty;
    [Required]
    public string EndDate { get; set; } = string.Empty;
    public string? Name { get; set; }
}

public class StringFilter
{
    public string MatchType { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public bool CaseSensitive { get; set; }
}

public class NumericFilter
{
    public string Operation { get; set; } = string.Empty;
    public object? Value { get; set; }
}

public class BetweenFilter
{
    public object? FromValue { get; set; }
    public object? ToValue { get; set; }
}

public class InListFilter
{
    public List<string> Values { get; set; } = new();
    public bool CaseSensitive { get; set; }
}

public class Filter
{
    public string FieldName { get; set; } = string.Empty;
    public StringFilter? StringFilter { get; set; }
    public NumericFilter? NumericFilter { get; set; }
    public BetweenFilter? BetweenFilter { get; set; }
    public InListFilter? InListFilter { get; set; }
}

public class FilterExpression
{
    public Filter? Filter { get; set; }
    public FilterExpressionList? AndGroup { get; set; }
    public FilterExpressionList? OrGroup { get; set; }
    public FilterExpression? NotExpression { get; set; }
}

public class FilterExpressionList
{
    public List<FilterExpression> Expressions { get; set; } = new();
}

public class OrderBy
{
    public DimensionOrderBy? Dimension { get; set; }
    public MetricOrderBy? Metric { get; set; }
    public PivotOrderBy? Pivot { get; set; }
    public bool Desc { get; set; } = false;
}

public class DimensionOrderBy
{
    public string DimensionName { get; set; } = string.Empty;
    public string? OrderType { get; set; }
}

public class MetricOrderBy
{
    public string MetricName { get; set; } = string.Empty;
}

public class PivotOrderBy
{
    public int? PivotSelectionCount { get; set; }
    public object? Subtotals { get; set; }
}

public class RunReportRequest
{
    [Required]
    public string PropertyId { get; set; } = string.Empty;
    [Required]
    public List<DateRange> DateRanges { get; set; } = new();
    [Required]
    public List<Dimension> Dimensions { get; set; } = new();
    [Required]
    public List<Metric> Metrics { get; set; } = new();
    public FilterExpression? DimensionFilter { get; set; }
    public FilterExpression? MetricFilter { get; set; }
    public long Offset { get; set; } = 0;
    public long Limit { get; set; } = 10000;
    public List<string> MetricAggregations { get; set; } = new();
    public List<OrderBy> OrderBys { get; set; } = new();
    public string? CurrencyCode { get; set; }
    public object? CohortSpec { get; set; }
    public bool KeepEmptyRows { get; set; } = false;
    public bool ReturnPropertyQuota { get; set; } = false;
}

public class DimensionHeader
{
    public string Name { get; set; } = string.Empty;
}

public class MetricHeader
{
    public string Name { get; set; } = string.Empty;
    public string? Type { get; set; }
}

public class DimensionValue
{
    public string Value { get; set; } = string.Empty;
}

public class MetricValue
{
    public string Value { get; set; } = string.Empty;
}

public class Row
{
    public List<DimensionValue> DimensionValues { get; set; } = new();
    public List<MetricValue> MetricValues { get; set; } = new();
}

public class RunReportResponse
{
    public List<DimensionHeader> DimensionHeaders { get; set; } = new();
    public List<MetricHeader> MetricHeaders { get; set; } = new();
    public List<Row> Rows { get; set; } = new();
    public List<Row> Totals { get; set; } = new();
    public List<Row> Maximums { get; set; } = new();
    public List<Row> Minimums { get; set; } = new();
    public int RowCount { get; set; }
    public object? Metadata { get; set; }
    public object? PropertyQuota { get; set; }
    public string? Kind { get; set; }
}

public class BatchRunReportsRequest
{
    [Required]
    public string PropertyId { get; set; } = string.Empty;
    [Required]
    public List<RunReportRequest> Requests { get; set; } = new();
}

public class BatchRunReportsResponse
{
    public List<RunReportResponse> Reports { get; set; } = new();
    public string? Kind { get; set; }
}
