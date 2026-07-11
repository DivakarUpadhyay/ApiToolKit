using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Workspace.Calendar;

public class EventDateTime
{
    public DateTime? DateTime { get; set; }
    public string? Date { get; set; }
    public string? TimeZone { get; set; }
}

public class EventAttendee
{
    public string? Email { get; set; }
    public string? DisplayName { get; set; }
    public bool Self { get; set; }
    public bool Organizer { get; set; }
    public string? ResponseStatus { get; set; }
    public bool Optional { get; set; }
    public string? Comment { get; set; }
}

public class ConferenceEntryPoint
{
    public string? EntryPointType { get; set; }
    public string? Uri { get; set; }
    public string? Label { get; set; }
    public string? MeetingCode { get; set; }
    public string? Passcode { get; set; }
    public string? Password { get; set; }
    public string? Pin { get; set; }
}

public class ConferenceSolution
{
    public string? Name { get; set; }
    public string? IconUri { get; set; }
}

public class ConferenceData
{
    public List<ConferenceEntryPoint>? EntryPoints { get; set; }
    public ConferenceSolution? ConferenceSolution { get; set; }
    public string? ConferenceId { get; set; }
    public string? Signature { get; set; }
    public string? Notes { get; set; }
}

public class EventCreator
{
    public string? Email { get; set; }
    public string? DisplayName { get; set; }
    public bool Self { get; set; }
}

public class EventReminders
{
    public bool UseDefault { get; set; }
    public List<Reminder>? Overrides { get; set; }
}

public class EventResponse
{
    public string? Id { get; set; }
    public string? CalendarId { get; set; }
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public string? HtmlLink { get; set; }
    public string? Status { get; set; }
    public EventDateTime? Start { get; set; }
    public EventDateTime? End { get; set; }
    public List<string>? Recurrence { get; set; }
    public string? RecurringEventId { get; set; }
    public List<EventAttendee>? Attendees { get; set; }
    public EventReminders? Reminders { get; set; }
    public ConferenceData? ConferenceData { get; set; }
    public EventCreator? Creator { get; set; }
    public EventCreator? Organizer { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }
    public string? ColorId { get; set; }
    public string? Visibility { get; set; }
    public string? Transparency { get; set; }
    public bool GuestsCanInviteOthers { get; set; }
    public bool GuestsCanSeeOtherGuests { get; set; }
}

public class ListEventsRequest
{
    [Required]
    public string CalendarId { get; set; } = string.Empty;
    public int MaxResults { get; set; } = 250;
    public string? PageToken { get; set; }
    public string OrderBy { get; set; } = "startTime";
    public bool SingleEvents { get; set; } = true;
    public DateTime? TimeMin { get; set; }
    public DateTime? TimeMax { get; set; }
    public string? Q { get; set; }
    public bool ShowDeleted { get; set; } = false;
}

public class EventListResponse
{
    public List<EventResponse>? Events { get; set; }
    public string? NextPageToken { get; set; }
    public string? Summary { get; set; }
    public string? TimeZone { get; set; }
    public DateTime? Updated { get; set; }
    public string? NextSyncToken { get; set; }
}

public class CreateEventRequest
{
    [Required]
    public string CalendarId { get; set; } = string.Empty;

    [Required]
    public string Summary { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Location { get; set; }

    [Required]
    public EventDateTime Start { get; set; } = new();

    [Required]
    public EventDateTime End { get; set; } = new();
    public List<string>? Attendees { get; set; }
    public List<string>? Recurrence { get; set; }
    public bool SendNotifications { get; set; } = true;
    public string? ColorId { get; set; }
    public string Visibility { get; set; } = "default";
    public List<Reminder>? Reminders { get; set; }
    public int ConferenceDataVersion { get; set; }
}

public class UpdateEventRequest
{
    [Required]
    public string CalendarId { get; set; } = string.Empty;

    [Required]
    public string EventId { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public EventDateTime? Start { get; set; }
    public EventDateTime? End { get; set; }
    public List<string>? Attendees { get; set; }
    public bool SendNotifications { get; set; } = true;
}

public class DeleteEventRequest
{
    [Required]
    public string CalendarId { get; set; } = string.Empty;

    [Required]
    public string EventId { get; set; } = string.Empty;
    public bool SendNotifications { get; set; } = true;
}

public class QuickAddEventRequest
{
    [Required]
    public string CalendarId { get; set; } = string.Empty;

    [Required]
    public string Text { get; set; } = string.Empty;
}

public class MoveEventRequest
{
    [Required]
    public string CalendarId { get; set; } = string.Empty;

    [Required]
    public string EventId { get; set; } = string.Empty;

    [Required]
    public string Destination { get; set; } = string.Empty;
}

public class FreeBusyPeriod
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}

public class FreeBusyCalendar
{
    public List<FreeBusyPeriod>? Busy { get; set; }
    public List<object>? Errors { get; set; }
}

public class FreeBusyRequest
{
    [Required]
    public DateTime TimeMin { get; set; }

    [Required]
    public DateTime TimeMax { get; set; }
    public string TimeZone { get; set; } = "UTC";

    [Required]
    public List<string> Items { get; set; } = new();
}

public class FreeBusyResponse
{
    public string? Kind { get; set; }
    public DateTime TimeMin { get; set; }
    public DateTime TimeMax { get; set; }
    public Dictionary<string, FreeBusyCalendar>? Calendars { get; set; }
}
