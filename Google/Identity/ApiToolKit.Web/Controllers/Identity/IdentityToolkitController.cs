using ApiToolKit.Application.DTOs.Identity.Toolkit;
using ApiToolKit.Application.Interfaces.Services.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Identity;

[ApiController]
[Route("api/identity/toolkit")]
public class IdentityToolkitController : ControllerBase
{
    private readonly IIdentityToolkitService _toolkitService;

    public IdentityToolkitController(IIdentityToolkitService toolkitService)
    {
        _toolkitService = toolkitService;
    }

    /// <summary>Sign up a new user with email and password.</summary>
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        var result = await _toolkitService.SignUpAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Sign in an existing user with email and password.</summary>
    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
    {
        var result = await _toolkitService.SignInAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get account info for the authenticated user.</summary>
    [HttpPost("account")]
    public async Task<IActionResult> GetAccountInfo([FromBody] GetAccountInfoRequest request)
    {
        var result = await _toolkitService.GetAccountInfoAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update the authenticated user's display name or photo URL.</summary>
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
    {
        var result = await _toolkitService.UpdateProfileAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Change the authenticated user's password.</summary>
    [HttpPut("password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var result = await _toolkitService.ChangePasswordAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Change the authenticated user's email address.</summary>
    [HttpPut("email")]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailRequest request)
    {
        var result = await _toolkitService.ChangeEmailAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Send a verification email to the authenticated user.</summary>
    [HttpPost("verify-email")]
    public async Task<IActionResult> SendVerificationEmail([FromBody] VerifyEmailRequest request)
    {
        var result = await _toolkitService.SendVerificationEmailAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Send a password reset email to the specified email address.</summary>
    [HttpPost("reset-password")]
    public async Task<IActionResult> SendPasswordReset([FromBody] ResetPasswordRequest request)
    {
        var result = await _toolkitService.SendPasswordResetAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete the authenticated user's account.</summary>
    [HttpDelete("account")]
    public async Task<IActionResult> DeleteAccount([FromBody] DeleteAccountRequest request)
    {
        var result = await _toolkitService.DeleteAccountAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
