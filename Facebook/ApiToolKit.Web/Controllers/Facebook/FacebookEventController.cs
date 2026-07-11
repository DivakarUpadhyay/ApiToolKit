using ApiToolKit.Application.DTOs.Facebook.Event;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/event")]
[Produces("application/json")]
public class FacebookEventController : ControllerBase
{
    private readonly IFacebookEventService _eventService;

    public FacebookEventController(IFacebookEventService eventService)
    {
        _eventService = eventService;
    }

    /// <summary>Create a new Facebook event</summary>
    [HttpPost]
    public async Task<IActionResult> CreateEventAsync([FromBody] CreateEventRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _eventService.CreateEventAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get event details by event ID</summary>
    [HttpGet("{eventId}")]
    public async Task<IActionResult> GetEventAsync(string eventId, [FromQuery] string accessToken)
    {
        var result = await _eventService.GetEventAsync(eventId, accessToken);
        return Ok(result);
    }

    /// <summary>Update an existing event</summary>
    [HttpPut("{eventId}")]
    public async Task<IActionResult> UpdateEventAsync([FromBody] UpdateEventRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _eventService.UpdateEventAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Delete an event</summary>
    [HttpDelete("{eventId}")]
    public async Task<IActionResult> DeleteEventAsync(string eventId, [FromQuery] string accessToken)
    {
        var result = await _eventService.DeleteEventAsync(eventId, accessToken);
        return Ok(result);
    }

    /// <summary>Get all events for a page</summary>
    [HttpGet("{pageId}/events")]
    public async Task<IActionResult> GetEventsByPageAsync(string pageId, [FromQuery] string accessToken, [FromQuery] int limit = 25)
    {
        var result = await _eventService.GetEventsByPageAsync(pageId, accessToken, limit);
        return Ok(result);
    }

    /// <summary>Get list of people attending an event</summary>
    [HttpPost("{eventId}/attendees")]
    public async Task<IActionResult> GetEventAttendeesAsync([FromBody] GetEventAttendeesRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _eventService.GetEventAttendeesAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>RSVP to an event (attending, maybe, declined)</summary>
    [HttpPost("{eventId}/rsvp")]
    public async Task<IActionResult> RsvpEventAsync([FromBody] RsvpEventRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _eventService.RsvpEventAsync(request, accessToken);
        return Ok(result);
    }
}
