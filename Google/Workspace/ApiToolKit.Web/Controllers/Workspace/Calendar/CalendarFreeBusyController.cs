using ApiToolKit.Application.DTOs.Workspace.Calendar;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Calendar;

/// <summary>Google Calendar Free/Busy API</summary>
[ApiController]
[Route("api/workspace/calendar/freebusy")]
public class CalendarFreeBusyController : ControllerBase
{
    private readonly ICalendarFreeBusyService _service;

    public CalendarFreeBusyController(ICalendarFreeBusyService service)
    {
        _service = service;
    }

    /// <summary>Query free/busy information for calendars</summary>
    [HttpPost("{userId}")]
    public async Task<IActionResult> QueryFreeBusy(string userId, [FromBody] FreeBusyRequest request)
    {
        var result = await _service.QueryFreeBusyAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
