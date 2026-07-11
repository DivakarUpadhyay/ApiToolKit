namespace ApiToolKit.Domain.Entities.YouTube;

public class YouTubeVideo
{
    public int Id { get; set; }
    public string VideoId { get; set; } = string.Empty;
    public string ChannelId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime PublishedAt { get; set; }
    public long ViewCount { get; set; }
    public long LikeCount { get; set; }
    public long CommentCount { get; set; }
    public string Duration { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
