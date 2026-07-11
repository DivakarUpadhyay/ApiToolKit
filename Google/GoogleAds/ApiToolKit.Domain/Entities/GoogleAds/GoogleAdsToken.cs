namespace ApiToolKit.Domain.Entities.GoogleAds;

public class GoogleAdsToken
{
    public int Id { get; set; }
    public string CustomerId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public string Scope { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
