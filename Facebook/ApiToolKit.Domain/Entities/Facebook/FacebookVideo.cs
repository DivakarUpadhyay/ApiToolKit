namespace ApiToolKit.Domain.Entities.Facebook;

public class FacebookVideo
{
    public int Id { get; set; }
    public string VideoId { get; set; } = string.Empty;
    public string PageId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Source { get; set; }
    public string? EmbedHtml { get; set; }
    public string? Status { get; set; }
    public double Length { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
