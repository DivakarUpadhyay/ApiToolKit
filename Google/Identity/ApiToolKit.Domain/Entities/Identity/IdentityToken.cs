namespace ApiToolKit.Domain.Entities.Identity;

public class IdentityToken
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string? RefreshToken { get; set; }
    public string? IdToken { get; set; }
    public DateTime ExpiresAt { get; set; }
    public string? Scope { get; set; }
    public string TokenType { get; set; } = "Bearer";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
