using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Workspace.Calendar;

public class Reminder
{
    public string Method { get; set; } = "popup";
    public int Minutes { get; set; }
}

public class CalendarResponse
{
    public string? Id { get; set; }
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public string? TimeZone { get; set; }
    public string? Location { get; set; }
    public string? Kind { get; set; }
    public string? Etag { get; set; }
    public string? ColorId { get; set; }
    public string? BackgroundColor { get; set; }
    public string? ForegroundColor { get; set; }
    public bool Selected { get; set; }
    public string? AccessRole { get; set; }
    public List<Reminder>? DefaultReminders { get; set; }
    public object? ConferenceProperties { get; set; }
}

public class ListCalendarsResponse
{
    public List<CalendarResponse>? Calendars { get; set; }
    public string? NextPageToken { get; set; }
    public string? Etag { get; set; }
}

public class CreateCalendarRequest
{
    [Required]
    public string Summary { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? TimeZone { get; set; }
    public string? Location { get; set; }
}

public class UpdateCalendarRequest
{
    [Required]
    public string CalendarId { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public string? TimeZone { get; set; }
    public string? Location { get; set; }
    public string? ColorId { get; set; }
}

public class DeleteCalendarRequest
{
    [Required]
    public string CalendarId { get; set; } = string.Empty;
}

public class ClearCalendarRequest
{
    [Required]
    public string CalendarId { get; set; } = string.Empty;
}
