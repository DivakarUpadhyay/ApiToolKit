namespace ApiToolKit.Domain.Entities.Workspace.Drive;

public class DriveFile
{
    public int Id { get; set; }
    public string FileId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? MimeType { get; set; }
    public string? Description { get; set; }
    public long Size { get; set; }
    public DateTime? ModifiedTime { get; set; }
    public DateTime? CreatedTime { get; set; }
    public string? WebViewLink { get; set; }
    public string? WebContentLink { get; set; }
    public string? Parents { get; set; }
    public bool IsShared { get; set; }
    public bool IsStarred { get; set; }
    public bool IsTrashed { get; set; }
    public long Version { get; set; }
    public DateTime CreatedAt { get; set; }
}
