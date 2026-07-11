namespace ApiToolKit.Domain.Entities.Cloud;

public class CloudToken
{
    public int Id { get; set; }
    public string ProjectId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string? RefreshToken { get; set; }
    public DateTime ExpiresAt { get; set; }
    public string? Scope { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
