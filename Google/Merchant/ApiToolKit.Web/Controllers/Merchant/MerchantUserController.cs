using ApiToolKit.Application.DTOs.Merchant.User;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// users (v1)
/// </summary>
[ApiController]
[Route("api/merchant/accounts")]
[Produces("application/json")]
public class MerchantUserController : ControllerBase
{
    private readonly IMerchantUserService _userService;

    public MerchantUserController(IMerchantUserService userService)
    {
        _userService = userService;
    }

    /// <summary>Lists all users of a Merchant Center account.</summary>
    [HttpGet("accounts/{accountId}/users")]
    public async Task<IActionResult> ListUsersAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListUsersRequest { AccountKey = accountKey, AccountId = accountId, PageSize = pageSize, PageToken = pageToken };
        var result = await _userService.ListUsersAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a Merchant Center account user.</summary>
    [HttpGet("accounts/{accountId}/users/{userId}")]
    public async Task<IActionResult> GetUserAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string userId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetUserRequest { AccountKey = accountKey, AccountId = accountId, UserId = userId };
        var result = await _userService.GetUserAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes a Merchant Center account user.</summary>
    [HttpDelete("accounts/{accountId}/users/{userId}")]
    public async Task<IActionResult> DeleteUserAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string userId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteUserRequest { AccountKey = accountKey, AccountId = accountId, UserId = userId };
        var result = await _userService.DeleteUserAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a Merchant Center account user.</summary>
    [HttpPost("accounts/{accountId}/users")]
    public async Task<IActionResult> CreateUserAsync([FromRoute] string accountId, [FromBody] CreateUserRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _userService.CreateUserAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates a Merchant Center account user.</summary>
    [HttpPatch("accounts/{accountId}/users/{userId}")]
    public async Task<IActionResult> UpdateUserAsync([FromRoute] string accountId, [FromRoute] string userId, [FromBody] UpdateUserRequest request)
    {
        request.AccountId = accountId;
        request.UserId = userId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _userService.UpdateUserAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
