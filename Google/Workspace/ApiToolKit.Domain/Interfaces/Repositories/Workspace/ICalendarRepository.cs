using ApiToolKit.Domain.Entities.Workspace.Calendar;

namespace ApiToolKit.Domain.Interfaces.Repositories.Workspace;

public interface ICalendarRepository
{
    Task SaveEventAsync(CalendarEvent calendarEvent);
    Task UpdateEventAsync(CalendarEvent calendarEvent);
    Task DeleteEventAsync(string eventId, string calendarId);
    Task<List<CalendarEvent>> GetEventsByCalendarIdAsync(string calendarId, string userId);
}
