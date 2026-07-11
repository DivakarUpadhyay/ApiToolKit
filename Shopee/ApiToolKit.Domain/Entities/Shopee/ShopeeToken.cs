namespace ApiToolKit.Domain.Entities.Shopee;

public class ShopeeToken
{
    public int Id { get; set; }
    public long ShopId { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public long ExpireIn { get; set; }
    public long RefreshTokenExpireIn { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
