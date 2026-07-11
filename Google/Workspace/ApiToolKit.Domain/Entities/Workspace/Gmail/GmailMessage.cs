namespace ApiToolKit.Domain.Entities.Workspace.Gmail;

public class GmailMessage
{
    public int Id { get; set; }
    public string MessageId { get; set; } = string.Empty;
    public string ThreadId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string? Subject { get; set; }
    public string? From { get; set; }
    public string? To { get; set; }
    public string? Cc { get; set; }
    public string? Snippet { get; set; }
    public string? Body { get; set; }
    public string? LabelIds { get; set; }
    public bool IsRead { get; set; }
    public bool IsStarred { get; set; }
    public long InternalDate { get; set; }
    public int SizeEstimate { get; set; }
    public DateTime CreatedAt { get; set; }
}
