namespace ApiToolKit.Domain.Entities.Shopify;
public class ShopifyConfig
{
    public int Id { get; set; }
    public string ApiKey { get; set; } = string.Empty;
    public string ApiSecret { get; set; } = string.Empty;
    public string ShopDomain { get; set; } = string.Empty;
    public string RedirectUrl { get; set; } = string.Empty;
    public string Scopes { get; set; } = string.Empty;
    public string ApiVersion { get; set; } = "2024-10";
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
