namespace ApiToolKit.Domain.Entities.DisplayVideo;

public class DisplayVideoToken
{
    public int Id { get; set; }
    public string AccountKey { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string? RefreshToken { get; set; }
    public DateTime ExpiresAt { get; set; }
    public string? Scope { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
