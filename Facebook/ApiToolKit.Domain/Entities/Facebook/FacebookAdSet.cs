namespace ApiToolKit.Domain.Entities.Facebook;

public class FacebookAdSet
{
    public int Id { get; set; }
    public string AdSetId { get; set; } = string.Empty;
    public string CampaignId { get; set; } = string.Empty;
    public string AccountId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Status { get; set; }
    public string? DailyBudget { get; set; }
    public string? LifetimeBudget { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string? BillingEvent { get; set; }
    public string? OptimizationGoal { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
