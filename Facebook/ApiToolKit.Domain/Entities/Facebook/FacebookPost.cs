namespace ApiToolKit.Domain.Entities.Facebook;

public class FacebookPost
{
    public int Id { get; set; }
    public string PostId { get; set; } = string.Empty;
    public string PageId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string? Message { get; set; }
    public string? Story { get; set; }
    public string? FullPicture { get; set; }
    public string? PermalinkUrl { get; set; }
    public bool IsPublished { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
