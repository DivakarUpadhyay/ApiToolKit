using ApiToolKit.Application.DTOs.Workspace.Drive;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Drive;

/// <summary>Google Drive Shared Drives API</summary>
[ApiController]
[Route("api/workspace/drive/shared-drives")]
public class DriveSharedDriveController : ControllerBase
{
    private readonly IDriveSharedDriveService _service;

    public DriveSharedDriveController(IDriveSharedDriveService service)
    {
        _service = service;
    }

    /// <summary>List shared drives</summary>
    [HttpGet("{userId}")]
    public async Task<IActionResult> ListSharedDrives(string userId)
    {
        var result = await _service.ListSharedDrivesAsync(userId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific shared drive</summary>
    [HttpGet("{userId}/{driveId}")]
    public async Task<IActionResult> GetSharedDrive(string userId, string driveId)
    {
        var result = await _service.GetSharedDriveAsync(userId, driveId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a shared drive</summary>
    [HttpPost("{userId}")]
    public async Task<IActionResult> CreateSharedDrive(string userId, [FromBody] CreateSharedDriveRequest request)
    {
        var result = await _service.CreateSharedDriveAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a shared drive</summary>
    [HttpPatch("{userId}")]
    public async Task<IActionResult> UpdateSharedDrive(string userId, [FromBody] UpdateSharedDriveRequest request)
    {
        var result = await _service.UpdateSharedDriveAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a shared drive</summary>
    [HttpDelete("{userId}/{driveId}")]
    public async Task<IActionResult> DeleteSharedDrive(string userId, string driveId)
    {
        var result = await _service.DeleteSharedDriveAsync(userId, driveId);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
