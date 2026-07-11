namespace ApiToolKit.Domain.Entities.Facebook;

public class FacebookPhoto
{
    public int Id { get; set; }
    public string PhotoId { get; set; } = string.Empty;
    public string PageId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Source { get; set; }
    public string? AlbumId { get; set; }
    public string? Link { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
