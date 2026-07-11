namespace ApiToolKit.Domain.Entities.Workspace.Calendar;

public class CalendarEvent
{
    public int Id { get; set; }
    public string EventId { get; set; } = string.Empty;
    public string CalendarId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public string? StartTimeZone { get; set; }
    public string? EndTimeZone { get; set; }
    public string? Status { get; set; }
    public string? HtmlLink { get; set; }
    public string? RecurringEventId { get; set; }
    public string? Recurrence { get; set; }
    public string? Attendees { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
