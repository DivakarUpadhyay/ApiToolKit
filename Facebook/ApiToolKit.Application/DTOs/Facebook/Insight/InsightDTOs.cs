using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Facebook.Insight;

public enum DatePreset
{
    today,
    yesterday,
    last_7d,
    last_14d,
    last_28d,
    last_30d,
    last_90d,
    last_month,
    this_month,
    this_quarter,
    this_year
}

public class GetPageInsightsRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    public List<string> Metric { get; set; } = new();
    public string Period { get; set; } = "day";
    public DateTime? Since { get; set; }
    public DateTime? Until { get; set; }
}

public class InsightValue
{
    public object? Value { get; set; }
    public string? EndTime { get; set; }
}

public class PageInsightResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Period { get; set; }
    public List<InsightValue> Values { get; set; } = new();
    public string? Title { get; set; }
    public string? Description { get; set; }
}

public class InsightPaging
{
    public string? Previous { get; set; }
    public string? Next { get; set; }
}

public class GetPageInsightsResponse
{
    public List<PageInsightResponse> Data { get; set; } = new();
    public InsightPaging? Paging { get; set; }
}

public class GetAdInsightsRequest
{
    [Required] public string AccountId { get; set; } = string.Empty;
    public string? CampaignId { get; set; }
    public string? AdSetId { get; set; }
    public string? AdId { get; set; }
    public string Level { get; set; } = "account";
    public List<string> Fields { get; set; } = new();
    public string? DatePreset { get; set; }
    public string? Since { get; set; }
    public string? Until { get; set; }
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
    public List<string>? Breakdowns { get; set; }
    public List<string>? ActionAttributionWindows { get; set; }
}

public class AdAction
{
    public string ActionType { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}

public class AdInsightResponse
{
    public string? AccountId { get; set; }
    public string? CampaignId { get; set; }
    public string? AdSetId { get; set; }
    public string? AdId { get; set; }
    public string? Impressions { get; set; }
    public string? Reach { get; set; }
    public string? Clicks { get; set; }
    public string? Spend { get; set; }
    public string? Cpc { get; set; }
    public string? Cpm { get; set; }
    public string? Ctr { get; set; }
    public string? Cpp { get; set; }
    public string? Frequency { get; set; }
    public string? UniqueClicks { get; set; }
    public List<AdAction> Actions { get; set; } = new();
    public string? DateStart { get; set; }
    public string? DateStop { get; set; }
}

public class AdInsightPaging
{
    public string? Before { get; set; }
    public string? After { get; set; }
}

public class GetAdInsightsResponse
{
    public List<AdInsightResponse> Data { get; set; } = new();
    public AdInsightPaging? Paging { get; set; }
}

public class VideoInsightRequest
{
    [Required] public string VideoId { get; set; } = string.Empty;
    public string Metric { get; set; } = string.Empty;
    public string Period { get; set; } = "lifetime";
    public DateTime? Since { get; set; }
    public DateTime? Until { get; set; }
}
