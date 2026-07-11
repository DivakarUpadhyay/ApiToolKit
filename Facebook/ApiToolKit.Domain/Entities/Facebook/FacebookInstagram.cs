namespace ApiToolKit.Domain.Entities.Facebook;

public class FacebookInstagram
{
    public int Id { get; set; }
    public string IgUserId { get; set; } = string.Empty;
    public string PageId { get; set; } = string.Empty;
    public string? Username { get; set; }
    public string? Name { get; set; }
    public string? Biography { get; set; }
    public long Followers { get; set; }
    public long FollowsCount { get; set; }
    public int MediaCount { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? Website { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
