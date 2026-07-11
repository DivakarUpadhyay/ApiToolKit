namespace ApiToolKit.Domain.Entities.Facebook;

public class FacebookMessenger
{
    public int Id { get; set; }
    public string SenderId { get; set; } = string.Empty;
    public string RecipientId { get; set; } = string.Empty;
    public string? MessageId { get; set; }
    public string? MessageText { get; set; }
    public string? MessageType { get; set; }
    public string Direction { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
