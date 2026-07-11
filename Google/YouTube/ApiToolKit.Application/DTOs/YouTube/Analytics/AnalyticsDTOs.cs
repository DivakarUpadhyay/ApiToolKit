using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiToolKit.Application.DTOs.YouTube.Analytics;

public class RunAnalyticsReportRequest
{
    [Required] public string Ids { get; set; } = string.Empty;
    [Required] public string StartDate { get; set; } = string.Empty;
    [Required] public string EndDate { get; set; } = string.Empty;
    [Required] public string Metrics { get; set; } = string.Empty;
    public string? Dimensions { get; set; }
    public string? Filters { get; set; }
    public string? Sort { get; set; }
    public int MaxResults { get; set; } = 1000;
}

public class AnalyticsRowHeader
{
    [JsonProperty("columnType")]
    public string ColumnType { get; set; } = string.Empty;

    [JsonProperty("dataType")]
    public string DataType { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
}

public class AnalyticsRow
{
    [JsonProperty("values")]
    public List<string> Values { get; set; } = new();
}

public class AnalyticsReportResponse
{
    [JsonProperty("kind")]
    public string Kind { get; set; } = string.Empty;

    [JsonProperty("columnHeaders")]
    public List<AnalyticsRowHeader> ColumnHeaders { get; set; } = new();

    [JsonProperty("rows")]
    public List<AnalyticsRow> Rows { get; set; } = new();
}
