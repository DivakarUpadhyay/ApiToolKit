using ApiToolKit.Application.DTOs.Workspace.Auth;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Auth;

/// <summary>Google Workspace OAuth 2.0 authentication</summary>
[ApiController]
[Route("api/workspace/auth")]
public class WorkspaceAuthController : ControllerBase
{
    private readonly IWorkspaceAuthService _authService;

    public WorkspaceAuthController(IWorkspaceAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>Get OAuth 2.0 authorization URL</summary>
    [HttpGet("url")]
    public IActionResult GetAuthUrl([FromQuery] string[] scopes, [FromQuery] string state = "")
    {
        if (scopes.Length == 0)
        {
            scopes = new[]
            {
                "https://www.googleapis.com/auth/gmail.modify",
                "https://www.googleapis.com/auth/drive",
                "https://www.googleapis.com/auth/calendar",
                "https://www.googleapis.com/auth/spreadsheets",
                "https://www.googleapis.com/auth/documents",
                "https://www.googleapis.com/auth/presentations",
                "https://www.googleapis.com/auth/meetings.space.created",
                "https://www.googleapis.com/auth/chat.messages",
                "https://www.googleapis.com/auth/forms.body"
            };
        }
        var result = _authService.GetAuthorizationUrl(scopes, state);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Exchange authorization code for tokens</summary>
    [HttpPost("token")]
    public async Task<IActionResult> ExchangeCode([FromBody] ExchangeCodeRequest request)
    {
        var result = await _authService.ExchangeCodeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Refresh access token using refresh token</summary>
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var result = await _authService.RefreshTokenAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
