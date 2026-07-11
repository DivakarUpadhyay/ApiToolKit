using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Facebook.AdSet;

public class GeoLocations
{
    [Newtonsoft.Json.JsonProperty("countries")] public List<string>? Countries { get; set; }
    [Newtonsoft.Json.JsonProperty("cities")] public List<string>? Cities { get; set; }
    [Newtonsoft.Json.JsonProperty("regions")] public List<string>? Regions { get; set; }
}

public class TargetingSpec
{
    [Newtonsoft.Json.JsonProperty("geo_locations")] public GeoLocations? GeoLocations { get; set; }
    [Newtonsoft.Json.JsonProperty("age_min")] public int AgeMin { get; set; } = 18;
    [Newtonsoft.Json.JsonProperty("age_max")] public int AgeMax { get; set; } = 65;
    [Newtonsoft.Json.JsonProperty("genders")] public List<int>? Genders { get; set; }
    [Newtonsoft.Json.JsonProperty("interests")] public List<object>? Interests { get; set; }
    [Newtonsoft.Json.JsonProperty("behaviors")] public List<object>? Behaviors { get; set; }
    [Newtonsoft.Json.JsonProperty("connections")] public List<object>? Connections { get; set; }
}

public class CreateAdSetRequest
{
    [Required] public string CampaignId { get; set; } = string.Empty;
    [Required] public string AccountId { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = "PAUSED";
    public string OptimizationGoal { get; set; } = "REACH";
    public string BillingEvent { get; set; } = "IMPRESSIONS";
    public string? BidAmount { get; set; }
    public string? DailyBudget { get; set; }
    public string? LifetimeBudget { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public TargetingSpec? Targeting { get; set; }
    public object? PromotedObject { get; set; }
    public List<string>? RegionalRegulatedCategories { get; set; }
}

public class CreateAdSetResponse
{
    public string Id { get; set; } = string.Empty;
}

public class AdSetResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? CampaignId { get; set; }
    public string? Status { get; set; }
    public string? DailyBudget { get; set; }
    public string? LifetimeBudget { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public string? OptimizationGoal { get; set; }
    public string? BillingEvent { get; set; }
    public string? BidAmount { get; set; }
    public string? CreatedTime { get; set; }
    public string? UpdatedTime { get; set; }
}

public class UpdateAdSetRequest
{
    [Required] public string AdSetId { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Status { get; set; }
    public string? DailyBudget { get; set; }
    public string? BidAmount { get; set; }
    public string? EndTime { get; set; }
}

public class GetAdSetsRequest
{
    [Required] public string CampaignId { get; set; } = string.Empty;
    public string? Status { get; set; }
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
    public string Fields { get; set; } = "id,name,campaign_id,status,daily_budget,lifetime_budget,start_time,end_time,optimization_goal,billing_event,bid_amount,created_time,updated_time";
}

public class AdSetPaging
{
    public string? Before { get; set; }
    public string? After { get; set; }
}

public class GetAdSetsResponse
{
    public List<AdSetResponse> Data { get; set; } = new();
    public AdSetPaging? Paging { get; set; }
}
