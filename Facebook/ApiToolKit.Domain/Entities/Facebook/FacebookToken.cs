namespace ApiToolKit.Domain.Entities.Facebook;

public class FacebookToken
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string? PageId { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public string TokenType { get; set; } = string.Empty;
    public DateTime? ExpiresAt { get; set; }
    public string? LongLivedToken { get; set; }
    public string? Scope { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
