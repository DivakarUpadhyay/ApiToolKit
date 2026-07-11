namespace ApiToolKit.Domain.Entities.YouTube;

public class YouTubePlaylist
{
    public int Id { get; set; }
    public string PlaylistId { get; set; } = string.Empty;
    public string ChannelId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int ItemCount { get; set; }
    public DateTime PublishedAt { get; set; }
    public string ThumbnailUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
