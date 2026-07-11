namespace ApiToolKit.Domain.Entities.Facebook;

public class FacebookComment
{
    public int Id { get; set; }
    public string CommentId { get; set; } = string.Empty;
    public string ObjectId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string? Message { get; set; }
    public string? FromName { get; set; }
    public string? FromId { get; set; }
    public bool CanRemove { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
