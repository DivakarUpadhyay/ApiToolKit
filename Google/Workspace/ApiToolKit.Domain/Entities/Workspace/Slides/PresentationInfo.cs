namespace ApiToolKit.Domain.Entities.Workspace.Slides;

public class PresentationInfo
{
    public int Id { get; set; }
    public string PresentationId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? PresentationUrl { get; set; }
    public string? RevisionId { get; set; }
    public DateTime CreatedAt { get; set; }
}
