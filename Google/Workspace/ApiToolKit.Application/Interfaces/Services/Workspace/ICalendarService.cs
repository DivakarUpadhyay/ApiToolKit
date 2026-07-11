using ApiToolKit.Application.DTOs.Workspace.Calendar;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface ICalendarService
{
    Task<Result<ListCalendarsResponse>> ListCalendarsAsync(string userId);
    Task<Result<CalendarResponse>> GetCalendarAsync(string userId, string calendarId);
    Task<Result<CalendarResponse>> CreateCalendarAsync(string userId, CreateCalendarRequest request);
    Task<Result<CalendarResponse>> UpdateCalendarAsync(string userId, UpdateCalendarRequest request);
    Task<Result<bool>> DeleteCalendarAsync(string userId, DeleteCalendarRequest request);
    Task<Result<bool>> ClearCalendarAsync(string userId, ClearCalendarRequest request);
}
