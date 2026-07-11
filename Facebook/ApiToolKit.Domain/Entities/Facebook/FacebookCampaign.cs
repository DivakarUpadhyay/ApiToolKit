namespace ApiToolKit.Domain.Entities.Facebook;

public class FacebookCampaign
{
    public int Id { get; set; }
    public string CampaignId { get; set; } = string.Empty;
    public string AccountId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Objective { get; set; }
    public string? Status { get; set; }
    public string? BudgetRemaining { get; set; }
    public string? DailyBudget { get; set; }
    public string? LifetimeBudget { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
