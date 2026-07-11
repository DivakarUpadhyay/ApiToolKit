namespace ApiToolKit.Domain.Entities.Workspace.Chat;

public class ChatMessage
{
    public int Id { get; set; }
    public string MessageName { get; set; } = string.Empty;
    public string SpaceName { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string? Text { get; set; }
    public string? CreateTime { get; set; }
    public string? LastUpdateTime { get; set; }
    public string? FormattedText { get; set; }
    public DateTime CreatedAt { get; set; }
}
