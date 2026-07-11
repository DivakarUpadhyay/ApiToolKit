namespace ApiToolKit.Domain.Entities.Firebase;

public class FcmMessage
{
    public int Id { get; set; }
    public string? MessageId { get; set; }
    public string ProjectId { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string? Body { get; set; }
    public string? Topic { get; set; }
    public string? Token { get; set; }
    public string? Condition { get; set; }
    public string? Data { get; set; }
    public string? Platform { get; set; }
    public string? SendStatus { get; set; }
    public DateTime? SentAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
