using ApiToolKit.Application.DTOs.Workspace.Calendar;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Calendar;

/// <summary>Google Calendar API - Calendar Management</summary>
[ApiController]
[Route("api/workspace/calendar")]
public class CalendarController : ControllerBase
{
    private readonly ICalendarService _service;

    public CalendarController(ICalendarService service)
    {
        _service = service;
    }

    /// <summary>List user's calendars</summary>
    [HttpGet("{userId}")]
    public async Task<IActionResult> ListCalendars(string userId)
    {
        var result = await _service.ListCalendarsAsync(userId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific calendar</summary>
    [HttpGet("{userId}/{calendarId}")]
    public async Task<IActionResult> GetCalendar(string userId, string calendarId)
    {
        var result = await _service.GetCalendarAsync(userId, calendarId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new calendar</summary>
    [HttpPost("{userId}")]
    public async Task<IActionResult> CreateCalendar(string userId, [FromBody] CreateCalendarRequest request)
    {
        var result = await _service.CreateCalendarAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a calendar</summary>
    [HttpPatch("{userId}")]
    public async Task<IActionResult> UpdateCalendar(string userId, [FromBody] UpdateCalendarRequest request)
    {
        var result = await _service.UpdateCalendarAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a calendar</summary>
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteCalendar(string userId, [FromBody] DeleteCalendarRequest request)
    {
        var result = await _service.DeleteCalendarAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Clear all events from a calendar</summary>
    [HttpPost("{userId}/clear")]
    public async Task<IActionResult> ClearCalendar(string userId, [FromBody] ClearCalendarRequest request)
    {
        var result = await _service.ClearCalendarAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
