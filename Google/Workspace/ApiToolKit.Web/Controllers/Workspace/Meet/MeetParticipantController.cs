using ApiToolKit.Application.DTOs.Workspace.Meet;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Meet;

/// <summary>Google Meet Participants API</summary>
[ApiController]
[Route("api/workspace/meet/participants")]
public class MeetParticipantController : ControllerBase
{
    private readonly IMeetParticipantService _service;

    public MeetParticipantController(IMeetParticipantService service)
    {
        _service = service;
    }

    /// <summary>List participants for a conference record</summary>
    [HttpGet("{userId}")]
    public async Task<IActionResult> ListParticipants(string userId, [FromQuery] ListParticipantsRequest request)
    {
        var result = await _service.ListParticipantsAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific participant</summary>
    [HttpGet("{userId}/{participantName}")]
    public async Task<IActionResult> GetParticipant(string userId, string participantName)
    {
        var result = await _service.GetParticipantAsync(userId, participantName);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
