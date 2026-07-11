namespace ApiToolKit.Domain.Entities.Shopify;

public class ShopifyProduct
{
    public int Id { get; set; }
    public string ShopDomain { get; set; } = string.Empty;
    public string ShopifyProductId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? BodyHtml { get; set; }
    public string? Vendor { get; set; }
    public string? ProductType { get; set; }
    public string? Status { get; set; }
    public string? Tags { get; set; }
    public DateTime? PublishedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
