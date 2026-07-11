namespace ApiToolKit.Domain.Entities.Facebook;

public class FacebookPage
{
    public int Id { get; set; }
    public string PageId { get; set; } = string.Empty;
    public string PageName { get; set; } = string.Empty;
    public string? PageCategory { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public long Followers { get; set; }
    public long Likes { get; set; }
    public bool IsPublished { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
