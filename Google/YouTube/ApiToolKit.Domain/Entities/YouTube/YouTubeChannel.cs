namespace ApiToolKit.Domain.Entities.YouTube;

public class YouTubeChannel
{
    public int Id { get; set; }
    public string ChannelId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CustomUrl { get; set; } = string.Empty;
    public DateTime PublishedAt { get; set; }
    public string Country { get; set; } = string.Empty;
    public long ViewCount { get; set; }
    public long SubscriberCount { get; set; }
    public long VideoCount { get; set; }
    public string ThumbnailUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
