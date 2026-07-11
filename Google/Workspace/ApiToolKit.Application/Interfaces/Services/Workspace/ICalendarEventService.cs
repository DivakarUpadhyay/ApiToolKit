using ApiToolKit.Application.DTOs.Workspace.Calendar;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Workspace;

public interface ICalendarEventService
{
    Task<Result<EventListResponse>> ListEventsAsync(string userId, ListEventsRequest request);
    Task<Result<EventResponse>> GetEventAsync(string userId, string calendarId, string eventId);
    Task<Result<EventResponse>> CreateEventAsync(string userId, CreateEventRequest request);
    Task<Result<EventResponse>> UpdateEventAsync(string userId, UpdateEventRequest request);
    Task<Result<bool>> DeleteEventAsync(string userId, DeleteEventRequest request);
    Task<Result<EventResponse>> QuickAddEventAsync(string userId, QuickAddEventRequest request);
    Task<Result<EventResponse>> MoveEventAsync(string userId, MoveEventRequest request);
}
