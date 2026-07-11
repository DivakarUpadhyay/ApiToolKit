namespace ApiToolKit.Domain.Entities.GoogleAds;

public class GoogleAdsConfig
{
    public int Id { get; set; }
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string DeveloperToken { get; set; } = string.Empty;
    public string RedirectUri { get; set; } = string.Empty;
    public string LoginCustomerId { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
