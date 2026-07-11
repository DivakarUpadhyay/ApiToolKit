namespace ApiToolKit.Domain.Entities.Workspace.Sheets;

public class SpreadsheetInfo
{
    public int Id { get; set; }
    public string SpreadsheetId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? SpreadsheetUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}
