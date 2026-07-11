namespace ApiToolKit.Domain.Entities.GoogleAds;

public class GoogleAdsConversionAction
{
    public int Id { get; set; }
    public string ResourceName { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string CountingType { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
