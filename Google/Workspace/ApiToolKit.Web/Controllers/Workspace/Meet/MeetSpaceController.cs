using ApiToolKit.Application.DTOs.Workspace.Meet;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Meet;

/// <summary>Google Meet Spaces API</summary>
[ApiController]
[Route("api/workspace/meet/spaces")]
public class MeetSpaceController : ControllerBase
{
    private readonly IMeetSpaceService _service;

    public MeetSpaceController(IMeetSpaceService service)
    {
        _service = service;
    }

    /// <summary>Create a new Meet space</summary>
    [HttpPost("{userId}")]
    public async Task<IActionResult> CreateSpace(string userId, [FromBody] CreateSpaceRequest request)
    {
        var result = await _service.CreateSpaceAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a Meet space</summary>
    [HttpGet("{userId}/{spaceName}")]
    public async Task<IActionResult> GetSpace(string userId, string spaceName)
    {
        var result = await _service.GetSpaceAsync(userId, spaceName);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>End the active conference in a space</summary>
    [HttpPost("{userId}/end")]
    public async Task<IActionResult> EndSpace(string userId, [FromBody] EndSpaceRequest request)
    {
        var result = await _service.EndSpaceAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
