using ApiToolKit.Domain.Entities.Workspace.Calendar;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Workspace;

public class CalendarRepository : ICalendarRepository
{
    private readonly DapperContext _context;

    public CalendarRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task SaveEventAsync(CalendarEvent calendarEvent)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_Workspace_Calendar_SaveEvent",
            new
            {
                calendarEvent.EventId,
                calendarEvent.CalendarId,
                calendarEvent.UserId,
                calendarEvent.Summary,
                calendarEvent.Description,
                calendarEvent.Location,
                calendarEvent.StartDateTime,
                calendarEvent.EndDateTime,
                calendarEvent.StartTimeZone,
                calendarEvent.EndTimeZone,
                calendarEvent.Status,
                calendarEvent.HtmlLink,
                calendarEvent.RecurringEventId,
                calendarEvent.Recurrence,
                calendarEvent.Attendees
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task UpdateEventAsync(CalendarEvent calendarEvent)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_Workspace_Calendar_UpdateEvent",
            new
            {
                calendarEvent.EventId,
                calendarEvent.CalendarId,
                calendarEvent.Summary,
                calendarEvent.Description,
                calendarEvent.Location,
                calendarEvent.StartDateTime,
                calendarEvent.EndDateTime,
                calendarEvent.Status,
                calendarEvent.Attendees
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task DeleteEventAsync(string eventId, string calendarId)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(
            "sp_Workspace_Calendar_DeleteEvent",
            new { EventId = eventId, CalendarId = calendarId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<List<CalendarEvent>> GetEventsByCalendarIdAsync(string calendarId, string userId)
    {
        using var conn = _context.CreateConnection();
        var result = await conn.QueryAsync<CalendarEvent>(
            "sp_Workspace_Calendar_GetEventsByCalendarId",
            new { CalendarId = calendarId, UserId = userId },
            commandType: System.Data.CommandType.StoredProcedure);
        return result.ToList();
    }
}
