using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.SearchConsole.SearchAnalytics;

/// <summary>Query clicks, impressions, CTR and average position grouped by any dimensions.</summary>
public class QuerySearchAnalyticsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Property URL, e.g. https://www.example.com/ or sc-domain:example.com</summary>
    [Required] public string SiteUrl { get; set; } = string.Empty;
    /// <summary>Start date (yyyy-MM-dd)</summary>
    [Required] public string StartDate { get; set; } = string.Empty;
    /// <summary>End date (yyyy-MM-dd)</summary>
    [Required] public string EndDate { get; set; } = string.Empty;
    /// <summary>query, page, country, device, searchAppearance, date</summary>
    public List<string>? Dimensions { get; set; }
    /// <summary>web, image, video, news, discover, googleNews</summary>
    public string? Type { get; set; }
    /// <summary>auto, byPage, byProperty</summary>
    public string? AggregationType { get; set; }
    /// <summary>Rows to return (max 25000)</summary>
    public int? RowLimit { get; set; }
    /// <summary>Zero-based row offset</summary>
    public int? StartRow { get; set; }
    /// <summary>final or all</summary>
    public string? DataState { get; set; }
    /// <summary>Raw dimensionFilterGroups array as documented by Google</summary>
    public object? DimensionFilterGroups { get; set; }
}

/// <summary>Keyword rankings: top search queries with clicks, impressions, CTR and average position.</summary>
public class GetKeywordRankingsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Property URL, e.g. https://www.example.com/ or sc-domain:example.com</summary>
    [Required] public string SiteUrl { get; set; } = string.Empty;
    /// <summary>Start date (yyyy-MM-dd)</summary>
    [Required] public string StartDate { get; set; } = string.Empty;
    /// <summary>End date (yyyy-MM-dd)</summary>
    [Required] public string EndDate { get; set; } = string.Empty;
    /// <summary>Number of keywords (default 100)</summary>
    public int? RowLimit { get; set; }
}

/// <summary>Top landing pages from organic search with clicks, impressions, CTR and position.</summary>
public class GetTopPagesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Property URL, e.g. https://www.example.com/ or sc-domain:example.com</summary>
    [Required] public string SiteUrl { get; set; } = string.Empty;
    /// <summary>Start date (yyyy-MM-dd)</summary>
    [Required] public string StartDate { get; set; } = string.Empty;
    /// <summary>End date (yyyy-MM-dd)</summary>
    [Required] public string EndDate { get; set; } = string.Empty;
    /// <summary>Number of pages (default 100)</summary>
    public int? RowLimit { get; set; }
}
