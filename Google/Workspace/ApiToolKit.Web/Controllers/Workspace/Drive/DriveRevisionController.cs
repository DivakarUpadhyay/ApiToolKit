using ApiToolKit.Application.DTOs.Workspace.Drive;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Drive;

/// <summary>Google Drive Revisions API</summary>
[ApiController]
[Route("api/workspace/drive/revisions")]
public class DriveRevisionController : ControllerBase
{
    private readonly IDriveRevisionService _service;

    public DriveRevisionController(IDriveRevisionService service)
    {
        _service = service;
    }

    /// <summary>List revisions for a file</summary>
    [HttpGet("{userId}/{fileId}")]
    public async Task<IActionResult> ListRevisions(string userId, string fileId)
    {
        var result = await _service.ListRevisionsAsync(userId, fileId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific revision</summary>
    [HttpGet("{userId}/{fileId}/{revisionId}")]
    public async Task<IActionResult> GetRevision(string userId, string fileId, string revisionId)
    {
        var result = await _service.GetRevisionAsync(userId, fileId, revisionId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a revision</summary>
    [HttpPatch("{userId}")]
    public async Task<IActionResult> UpdateRevision(string userId, [FromBody] UpdateRevisionRequest request)
    {
        var result = await _service.UpdateRevisionAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a revision</summary>
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteRevision(string userId, [FromBody] DeleteRevisionRequest request)
    {
        var result = await _service.DeleteRevisionAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
