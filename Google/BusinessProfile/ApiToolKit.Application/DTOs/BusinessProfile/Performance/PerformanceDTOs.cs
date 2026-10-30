using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.BusinessProfile.Performance;

/// <summary>Daily metrics such as BUSINESS_IMPRESSIONS_DESKTOP_SEARCH, CALL_CLICKS, WEBSITE_CLICKS, BUSINESS_DIRECTION_REQUESTS.</summary>
public class GetDailyMetricsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>One or more DailyMetric names</summary>
    [Required] public List<string> DailyMetrics { get; set; } = new();
    /// <summary>yyyy-MM-dd</summary>
    [Required] public string StartDate { get; set; } = string.Empty;
    /// <summary>yyyy-MM-dd</summary>
    [Required] public string EndDate { get; set; } = string.Empty;
}

/// <summary>Monthly search keywords that showed the business in Search and Maps.</summary>
public class GetSearchKeywordImpressionsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>yyyy-MM</summary>
    [Required] public string StartMonth { get; set; } = string.Empty;
    /// <summary>yyyy-MM</summary>
    [Required] public string EndMonth { get; set; } = string.Empty;
    /// <summary>Maximum results per page</summary>
    public int? PageSize { get; set; }
    /// <summary>Token for the next page</summary>
    public string? PageToken { get; set; }
}

/// <summary>Single daily metric time series for a location.</summary>
public class GetDailyMetricTimeSeriesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>One DailyMetric name</summary>
    [Required] public string DailyMetric { get; set; } = string.Empty;
    /// <summary>yyyy-MM-dd</summary>
    [Required] public string StartDate { get; set; } = string.Empty;
    /// <summary>yyyy-MM-dd</summary>
    [Required] public string EndDate { get; set; } = string.Empty;
}
