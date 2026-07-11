namespace ApiToolKit.Domain.Entities.Workspace.Docs;

public class DocumentInfo
{
    public int Id { get; set; }
    public string DocumentId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? DocumentUrl { get; set; }
    public string? RevisionId { get; set; }
    public DateTime CreatedAt { get; set; }
}
