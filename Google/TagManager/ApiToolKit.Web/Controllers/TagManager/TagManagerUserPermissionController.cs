using ApiToolKit.Application.DTOs.TagManager.UserPermission;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.TagManager;

/// <summary>
/// User access to an account and its containers.
/// </summary>
[ApiController]
[Route("api/tagmanager/user-permissions")]
[Produces("application/json")]
public class TagManagerUserPermissionController : ControllerBase
{
    private readonly ITagManagerUserPermissionService _userPermissionService;

    public TagManagerUserPermissionController(ITagManagerUserPermissionService userPermissionService)
    {
        _userPermissionService = userPermissionService;
    }

    /// <summary>List users with access to an account.</summary>
    [HttpGet]
    public async Task<IActionResult> ListUserPermissionsAsync([FromQuery] string accountKey, [FromQuery] string accountId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        var request = new ListUserPermissionsRequest { AccountKey = accountKey, AccountId = accountId };
        var result = await _userPermissionService.ListUserPermissionsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get one user permission.</summary>
    [HttpGet("{permissionId}")]
    public async Task<IActionResult> GetUserPermissionAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromRoute] string permissionId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        var request = new GetUserPermissionRequest { AccountKey = accountKey, AccountId = accountId, PermissionId = permissionId };
        var result = await _userPermissionService.GetUserPermissionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Grant a user access to an account and containers.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateUserPermissionAsync([FromBody] CreateUserPermissionRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _userPermissionService.CreateUserPermissionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Change a user permission.</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateUserPermissionAsync([FromBody] UpdateUserPermissionRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _userPermissionService.UpdateUserPermissionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Remove a user from an account.</summary>
    [HttpDelete("{permissionId}")]
    public async Task<IActionResult> DeleteUserPermissionAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromRoute] string permissionId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        var request = new DeleteUserPermissionRequest { AccountKey = accountKey, AccountId = accountId, PermissionId = permissionId };
        var result = await _userPermissionService.DeleteUserPermissionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
