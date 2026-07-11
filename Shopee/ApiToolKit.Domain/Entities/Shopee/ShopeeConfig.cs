namespace ApiToolKit.Domain.Entities.Shopee;

public class ShopeeConfig
{
    public int Id { get; set; }
    public string PartnerKey { get; set; } = string.Empty;
    public long PartnerId { get; set; }
    public string RedirectUrl { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
