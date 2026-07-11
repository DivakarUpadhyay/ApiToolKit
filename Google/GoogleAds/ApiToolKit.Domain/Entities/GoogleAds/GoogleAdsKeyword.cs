namespace ApiToolKit.Domain.Entities.GoogleAds;

public class GoogleAdsKeyword
{
    public int Id { get; set; }
    public string ResourceName { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public string AdGroupResourceName { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string MatchType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public long CpcBidMicros { get; set; }
    public DateTime CreatedAt { get; set; }
}
