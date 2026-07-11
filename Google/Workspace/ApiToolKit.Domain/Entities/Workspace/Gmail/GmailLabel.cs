namespace ApiToolKit.Domain.Entities.Workspace.Gmail;

public class GmailLabel
{
    public int Id { get; set; }
    public string LabelId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Type { get; set; }
    public string? MessageListVisibility { get; set; }
    public string? LabelListVisibility { get; set; }
    public int MessagesTotal { get; set; }
    public int MessagesUnread { get; set; }
    public int ThreadsTotal { get; set; }
    public int ThreadsUnread { get; set; }
    public DateTime CreatedAt { get; set; }
}
