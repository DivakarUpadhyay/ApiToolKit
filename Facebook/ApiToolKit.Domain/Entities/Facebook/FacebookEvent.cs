namespace ApiToolKit.Domain.Entities.Facebook;

public class FacebookEvent
{
    public int Id { get; set; }
    public string EventId { get; set; } = string.Empty;
    public string PageId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string? Place { get; set; }
    public int AttendingCount { get; set; }
    public int InterestedCount { get; set; }
    public int DeclinedCount { get; set; }
    public bool IsOnline { get; set; }
    public string? Privacy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
