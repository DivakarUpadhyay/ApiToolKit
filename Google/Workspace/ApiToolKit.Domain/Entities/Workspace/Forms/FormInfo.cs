namespace ApiToolKit.Domain.Entities.Workspace.Forms;

public class FormInfo
{
    public int Id { get; set; }
    public string FormId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? DocumentTitle { get; set; }
    public string? RevisionId { get; set; }
    public string? ResponderUri { get; set; }
    public DateTime CreatedAt { get; set; }
}
