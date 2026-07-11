using ApiToolKit.Application.DTOs.Firebase.AppCheck;
using ApiToolKit.Application.Interfaces.Services.Firebase;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Firebase;

[ApiController]
[Route("api/firebase/app-check")]
public class AppCheckController : ControllerBase
{
    private readonly IAppCheckService _appCheckService;

    public AppCheckController(IAppCheckService appCheckService)
    {
        _appCheckService = appCheckService;
    }

    private string GetAccessToken()
    {
        var authHeader = Request.Headers.Authorization.ToString();
        return authHeader.Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase).Trim();
    }

    /// <summary>Verify a Firebase App Check token.</summary>
    [HttpPost("verify")]
    public async Task<IActionResult> VerifyToken([FromBody] VerifyAppCheckTokenRequest request)
    {
        request.AccessToken = GetAccessToken();
        var result = await _appCheckService.VerifyTokenAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Batch get App Check enforcement configurations for multiple apps.</summary>
    [HttpGet("configs")]
    public async Task<IActionResult> BatchGetConfigs([FromQuery] string projectId, [FromQuery] List<string> appIds)
    {
        var result = await _appCheckService.BatchGetConfigsAsync(new BatchGetAppCheckConfigsRequest
        {
            ProjectId = projectId,
            AppIds = appIds,
            AccessToken = GetAccessToken()
        });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Batch update App Check enforcement configurations for multiple apps.</summary>
    [HttpPatch("configs")]
    public async Task<IActionResult> BatchUpdateConfigs([FromBody] BatchUpdateAppCheckConfigsRequest request)
    {
        request.AccessToken = GetAccessToken();
        var result = await _appCheckService.BatchUpdateConfigsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
