namespace ApiToolKit.Domain.Entities.Shopify;
public class ShopifyToken
{
    public int Id { get; set; }
    public string ShopDomain { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string Scope { get; set; } = string.Empty;
    public string TokenType { get; set; } = "offline";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
