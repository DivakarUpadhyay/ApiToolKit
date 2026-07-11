using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Facebook.Campaign;

public enum Objective
{
    OUTCOME_AWARENESS,
    OUTCOME_TRAFFIC,
    OUTCOME_ENGAGEMENT,
    OUTCOME_LEADS,
    OUTCOME_APP_PROMOTION,
    OUTCOME_SALES,
    BRAND_AWARENESS,
    REACH,
    LINK_CLICKS,
    VIDEO_VIEWS,
    PAGE_LIKES,
    POST_ENGAGEMENT,
    CONVERSIONS,
    CATALOG_SALES,
    STORE_VISITS,
    MESSAGES
}

public class CreateCampaignRequest
{
    [Required] public string AccountId { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Objective { get; set; } = string.Empty;
    public string Status { get; set; } = "PAUSED";
    public List<string>? SpecialAdCategories { get; set; }
    public string? DailyBudget { get; set; }
    public string? LifetimeBudget { get; set; }
    public string? StartTime { get; set; }
    public string? StopTime { get; set; }
    public bool? BudgetRebalanceFlag { get; set; }
}

public class CreateCampaignResponse
{
    public string Id { get; set; } = string.Empty;
}

public class CampaignResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Objective { get; set; }
    public string? Status { get; set; }
    public string? DailyBudget { get; set; }
    public string? LifetimeBudget { get; set; }
    public string? StartTime { get; set; }
    public string? StopTime { get; set; }
    public string? BudgetRemaining { get; set; }
    public string? CreatedTime { get; set; }
    public string? UpdatedTime { get; set; }
}

public class UpdateCampaignRequest
{
    [Required] public string CampaignId { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Status { get; set; }
    public string? DailyBudget { get; set; }
    public string? LifetimeBudget { get; set; }
}

public class GetCampaignsRequest
{
    [Required] public string AccountId { get; set; } = string.Empty;
    public string? Status { get; set; }
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
    public string Fields { get; set; } = "id,name,objective,status,daily_budget,lifetime_budget,start_time,stop_time,budget_remaining,created_time,updated_time";
}

public class CampaignPaging
{
    public string? Before { get; set; }
    public string? After { get; set; }
}

public class GetCampaignsResponse
{
    public List<CampaignResponse> Data { get; set; } = new();
    public CampaignPaging? Paging { get; set; }
}
