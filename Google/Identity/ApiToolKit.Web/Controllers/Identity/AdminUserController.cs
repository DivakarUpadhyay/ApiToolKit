using ApiToolKit.Application.DTOs.Identity.Toolkit;
using ApiToolKit.Application.Interfaces.Services.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Identity;

[ApiController]
[Route("api/identity/admin/users")]
public class AdminUserController : ControllerBase
{
    private readonly IAdminUserService _adminUserService;

    public AdminUserController(IAdminUserService adminUserService)
    {
        _adminUserService = adminUserService;
    }

    private string GetAccessToken()
    {
        var authHeader = Request.Headers.Authorization.ToString();
        return authHeader.Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase).Trim();
    }

    /// <summary>List all users in the Identity project (Admin).</summary>
    [HttpGet]
    public async Task<IActionResult> ListUsers([FromQuery] ListUsersRequest request)
    {
        var result = await _adminUserService.ListUsersAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific user by localId (Admin).</summary>
    [HttpGet("{localId}")]
    public async Task<IActionResult> GetUser(string localId)
    {
        var result = await _adminUserService.GetUserAsync(new AdminGetUserRequest { LocalId = localId }, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new user (Admin).</summary>
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] AdminCreateUserRequest request)
    {
        var result = await _adminUserService.CreateUserAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing user (Admin).</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] AdminUpdateUserRequest request)
    {
        var result = await _adminUserService.UpdateUserAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a user by localId (Admin).</summary>
    [HttpDelete("{localId}")]
    public async Task<IActionResult> DeleteUser(string localId)
    {
        var result = await _adminUserService.DeleteUserAsync(new AdminDeleteUserRequest { LocalId = localId }, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
