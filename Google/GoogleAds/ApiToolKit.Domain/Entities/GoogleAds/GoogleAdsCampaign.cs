namespace ApiToolKit.Domain.Entities.GoogleAds;

public class GoogleAdsCampaign
{
    public int Id { get; set; }
    public string ResourceName { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string AdvertisingChannelType { get; set; } = string.Empty;
    public string BiddingStrategyType { get; set; } = string.Empty;
    public long BudgetAmountMicros { get; set; }
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
