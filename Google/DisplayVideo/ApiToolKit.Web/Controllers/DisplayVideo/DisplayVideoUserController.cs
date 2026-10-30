using ApiToolKit.Application.DTOs.DisplayVideo.User;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// users (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoUserController : ControllerBase
{
    private readonly IDisplayVideoUserService _userService;

    public DisplayVideoUserController(IDisplayVideoUserService userService)
    {
        _userService = userService;
    }

    /// <summary>Creates a new user.</summary>
    [HttpPost("users")]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _userService.CreateUserAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a user.</summary>
    [HttpGet("users/{userId}")]
    public async Task<IActionResult> GetUserAsync([FromQuery] string accountKey, [FromRoute] string userId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetUserRequest { AccountKey = accountKey, UserId = userId };
        var result = await _userService.GetUserAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing user.</summary>
    [HttpPatch("users/{userId}")]
    public async Task<IActionResult> UpdateUserAsync([FromRoute] string userId, [FromBody] UpdateUserRequest request)
    {
        request.UserId = userId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _userService.UpdateUserAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes a user.</summary>
    [HttpDelete("users/{userId}")]
    public async Task<IActionResult> DeleteUserAsync([FromQuery] string accountKey, [FromRoute] string userId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteUserRequest { AccountKey = accountKey, UserId = userId };
        var result = await _userService.DeleteUserAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Bulk edits user roles for a user.</summary>
    [HttpPost("users/{userId}/bulkEditAssignedUserRoles")]
    public async Task<IActionResult> BulkEditAssignedUserRolesAsync([FromRoute] string userId, [FromBody] BulkEditAssignedUserRolesRequest request)
    {
        request.UserId = userId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _userService.BulkEditAssignedUserRolesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists users that are accessible to the current user.</summary>
    [HttpGet("users")]
    public async Task<IActionResult> ListUsersAsync([FromQuery] string accountKey, [FromQuery] string? pageToken = null, [FromQuery] int? pageSize = null, [FromQuery] string? orderBy = null, [FromQuery] string? filter = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListUsersRequest { AccountKey = accountKey, PageToken = pageToken, PageSize = pageSize, OrderBy = orderBy, Filter = filter };
        var result = await _userService.ListUsersAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
