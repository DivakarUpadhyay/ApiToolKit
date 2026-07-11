namespace ApiToolKit.Domain.Entities.Facebook;

public class FacebookAd
{
    public int Id { get; set; }
    public string AdId { get; set; } = string.Empty;
    public string AdSetId { get; set; } = string.Empty;
    public string CampaignId { get; set; } = string.Empty;
    public string AccountId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Status { get; set; }
    public string? CreativeId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
