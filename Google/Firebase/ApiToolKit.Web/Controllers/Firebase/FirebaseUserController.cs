using ApiToolKit.Application.DTOs.Firebase.FirebaseAuth;
using ApiToolKit.Application.Interfaces.Services.Firebase;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Firebase;

[ApiController]
[Route("api/firebase/users")]
public class FirebaseUserController : ControllerBase
{
    private readonly IFirebaseUserService _userService;

    public FirebaseUserController(IFirebaseUserService userService)
    {
        _userService = userService;
    }

    private string GetAccessToken()
    {
        var authHeader = Request.Headers.Authorization.ToString();
        return authHeader.Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase).Trim();
    }

    /// <summary>Sign up a new Firebase user with email and password.</summary>
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] FirebaseSignUpRequest request)
    {
        var result = await _userService.SignUpAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Sign in an existing Firebase user with email and password.</summary>
    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] FirebaseSignInRequest request)
    {
        var result = await _userService.SignInAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get Firebase user account info using an ID token, localId, or email.</summary>
    [HttpGet("account")]
    public async Task<IActionResult> GetUser([FromQuery] string? idToken, [FromQuery] string? localId, [FromQuery] string? email)
    {
        var result = await _userService.GetUserAsync(new GetFirebaseUserRequest
        {
            IdToken = idToken,
            LocalId = localId,
            Email = email
        });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update the Firebase user's display name or photo URL.</summary>
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateFirebaseUserRequest request)
    {
        var result = await _userService.UpdateUserAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List all users in a Firebase project (Admin).</summary>
    [HttpGet("admin/list")]
    public async Task<IActionResult> AdminListUsers([FromQuery] AdminListUsersRequest request)
    {
        request.AccessToken = GetAccessToken();
        var result = await _userService.AdminListUsersAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new Firebase user (Admin).</summary>
    [HttpPost("admin")]
    public async Task<IActionResult> AdminCreateUser([FromBody] AdminCreateUserRequest request)
    {
        request.AccessToken = GetAccessToken();
        var result = await _userService.AdminCreateUserAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing Firebase user (Admin).</summary>
    [HttpPut("admin")]
    public async Task<IActionResult> AdminUpdateUser([FromBody] AdminUpdateUserRequest request)
    {
        request.AccessToken = GetAccessToken();
        var result = await _userService.AdminUpdateUserAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a Firebase user by localId (Admin).</summary>
    [HttpDelete("admin/{localId}")]
    public async Task<IActionResult> AdminDeleteUser(string localId, [FromQuery] string projectId)
    {
        var result = await _userService.AdminDeleteUserAsync(new AdminDeleteUserRequest
        {
            ProjectId = projectId,
            LocalId = localId,
            AccessToken = GetAccessToken()
        });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Verify a Firebase ID token.</summary>
    [HttpPost("verify-token")]
    public async Task<IActionResult> VerifyIdToken([FromBody] VerifyIdTokenRequest request)
    {
        request.AccessToken = GetAccessToken();
        var result = await _userService.VerifyIdTokenAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a Firebase custom token for a user.</summary>
    [HttpPost("custom-token")]
    public async Task<IActionResult> CreateCustomToken([FromBody] CreateCustomTokenRequest request)
    {
        request.AccessToken = GetAccessToken();
        var result = await _userService.CreateCustomTokenAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
