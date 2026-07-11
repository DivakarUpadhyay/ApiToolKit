namespace ApiToolKit.Domain.Entities.Shopify;
public class ShopifyStorefrontConfig
{
    public int Id { get; set; }
    public string ShopDomain { get; set; } = string.Empty;
    public string StorefrontAccessToken { get; set; } = string.Empty;
    public string ApiVersion { get; set; } = "2024-10";
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
