namespace ApiToolKit.Domain.Entities.GoogleAds;

public class GoogleAdsAdGroup
{
    public int Id { get; set; }
    public string ResourceName { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public string CampaignResourceName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public long CpcBidMicros { get; set; }
    public DateTime CreatedAt { get; set; }
}
