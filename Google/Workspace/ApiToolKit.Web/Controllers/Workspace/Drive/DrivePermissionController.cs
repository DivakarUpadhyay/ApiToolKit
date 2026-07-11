using ApiToolKit.Application.DTOs.Workspace.Drive;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Drive;

/// <summary>Google Drive Permissions API</summary>
[ApiController]
[Route("api/workspace/drive/permissions")]
public class DrivePermissionController : ControllerBase
{
    private readonly IDrivePermissionService _service;

    public DrivePermissionController(IDrivePermissionService service)
    {
        _service = service;
    }

    /// <summary>List permissions for a file</summary>
    [HttpGet("{userId}/{fileId}")]
    public async Task<IActionResult> ListPermissions(string userId, string fileId)
    {
        var result = await _service.ListPermissionsAsync(userId, fileId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific permission</summary>
    [HttpGet("{userId}/{fileId}/{permissionId}")]
    public async Task<IActionResult> GetPermission(string userId, string fileId, string permissionId)
    {
        var result = await _service.GetPermissionAsync(userId, fileId, permissionId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a permission for a file</summary>
    [HttpPost("{userId}")]
    public async Task<IActionResult> CreatePermission(string userId, [FromBody] CreatePermissionRequest request)
    {
        var result = await _service.CreatePermissionAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a permission</summary>
    [HttpPatch("{userId}")]
    public async Task<IActionResult> UpdatePermission(string userId, [FromBody] UpdatePermissionRequest request)
    {
        var result = await _service.UpdatePermissionAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a permission</summary>
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeletePermission(string userId, [FromBody] DeletePermissionRequest request)
    {
        var result = await _service.DeletePermissionAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
