using ApiToolKit.Application.DTOs.Workspace.Calendar;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Calendar;

/// <summary>Google Calendar Events API</summary>
[ApiController]
[Route("api/workspace/calendar/events")]
public class CalendarEventController : ControllerBase
{
    private readonly ICalendarEventService _service;

    public CalendarEventController(ICalendarEventService service)
    {
        _service = service;
    }

    /// <summary>List events in a calendar</summary>
    [HttpGet("{userId}")]
    public async Task<IActionResult> ListEvents(string userId, [FromQuery] ListEventsRequest request)
    {
        var result = await _service.ListEventsAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific event</summary>
    [HttpGet("{userId}/{calendarId}/{eventId}")]
    public async Task<IActionResult> GetEvent(string userId, string calendarId, string eventId)
    {
        var result = await _service.GetEventAsync(userId, calendarId, eventId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new event</summary>
    [HttpPost("{userId}")]
    public async Task<IActionResult> CreateEvent(string userId, [FromBody] CreateEventRequest request)
    {
        var result = await _service.CreateEventAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing event</summary>
    [HttpPatch("{userId}")]
    public async Task<IActionResult> UpdateEvent(string userId, [FromBody] UpdateEventRequest request)
    {
        var result = await _service.UpdateEventAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Quick add an event using natural language text</summary>
    [HttpPost("{userId}/quickAdd")]
    public async Task<IActionResult> QuickAddEvent(string userId, [FromBody] QuickAddEventRequest request)
    {
        var result = await _service.QuickAddEventAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Move an event to another calendar</summary>
    [HttpPost("{userId}/move")]
    public async Task<IActionResult> MoveEvent(string userId, [FromBody] MoveEventRequest request)
    {
        var result = await _service.MoveEventAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete an event</summary>
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteEvent(string userId, [FromBody] DeleteEventRequest request)
    {
        var result = await _service.DeleteEventAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
