using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Facebook.Event;

public class CreateEventRequest
{
    [Required] public string PageId { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string? Location { get; set; }
    public string? LocationId { get; set; }
    public string? CoverUrl { get; set; }
    public string Privacy { get; set; } = "OPEN";
    public bool IsOnline { get; set; }
    public string? OnlineEventFormat { get; set; }
}

public class CreateEventResponse
{
    public string Id { get; set; } = string.Empty;
}

public class EventPlace
{
    public string? Name { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
}

public class EventResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public EventPlace? Place { get; set; }
    public object? Cover { get; set; }
    public int AttendingCount { get; set; }
    public int InterestedCount { get; set; }
    public int DeclinedCount { get; set; }
    public bool GuestListEnabled { get; set; }
}

public class UpdateEventRequest
{
    [Required] public string EventId { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}

public class GetEventAttendeesRequest
{
    [Required] public string EventId { get; set; } = string.Empty;
    public int Limit { get; set; } = 25;
    public string? After { get; set; }
}

public class EventAttendeeResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Rsvp { get; set; }
}

public class RsvpEventRequest
{
    [Required] public string EventId { get; set; } = string.Empty;
    [Required] public string Rsvp { get; set; } = "attending";
}
