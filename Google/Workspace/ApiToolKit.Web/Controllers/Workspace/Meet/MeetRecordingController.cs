using ApiToolKit.Application.DTOs.Workspace.Meet;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Meet;

/// <summary>Google Meet Recordings API</summary>
[ApiController]
[Route("api/workspace/meet/recordings")]
public class MeetRecordingController : ControllerBase
{
    private readonly IMeetRecordingService _service;

    public MeetRecordingController(IMeetRecordingService service)
    {
        _service = service;
    }

    /// <summary>List recordings for a conference record</summary>
    [HttpGet("{userId}")]
    public async Task<IActionResult> ListRecordings(string userId, [FromQuery] ListRecordingsRequest request)
    {
        var result = await _service.ListRecordingsAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific recording</summary>
    [HttpGet("{userId}/{recordingName}")]
    public async Task<IActionResult> GetRecording(string userId, string recordingName)
    {
        var result = await _service.GetRecordingAsync(userId, recordingName);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List conference records</summary>
    [HttpGet("{userId}/conferenceRecords")]
    public async Task<IActionResult> ListConferenceRecords(string userId, [FromQuery] ListConferenceRecordsRequest request)
    {
        var result = await _service.ListConferenceRecordsAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific conference record</summary>
    [HttpGet("{userId}/conferenceRecords/{conferenceRecordName}")]
    public async Task<IActionResult> GetConferenceRecord(string userId, string conferenceRecordName)
    {
        var result = await _service.GetConferenceRecordAsync(userId, conferenceRecordName);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
