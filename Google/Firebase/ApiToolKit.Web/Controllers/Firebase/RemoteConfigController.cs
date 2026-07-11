using ApiToolKit.Application.DTOs.Firebase.RemoteConfig;
using ApiToolKit.Application.Interfaces.Services.Firebase;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Firebase;

[ApiController]
[Route("api/firebase/remote-config")]
public class RemoteConfigController : ControllerBase
{
    private readonly IRemoteConfigService _remoteConfigService;

    public RemoteConfigController(IRemoteConfigService remoteConfigService)
    {
        _remoteConfigService = remoteConfigService;
    }

    private string GetAccessToken()
    {
        var authHeader = Request.Headers.Authorization.ToString();
        return authHeader.Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase).Trim();
    }

    /// <summary>Get the current Firebase Remote Config template for a project.</summary>
    [HttpGet("{projectId}")]
    public async Task<IActionResult> GetRemoteConfig(string projectId)
    {
        var result = await _remoteConfigService.GetRemoteConfigAsync(new GetRemoteConfigRequest
        {
            ProjectId = projectId,
            AccessToken = GetAccessToken()
        });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update the Firebase Remote Config template for a project.</summary>
    [HttpPut("{projectId}")]
    public async Task<IActionResult> UpdateRemoteConfig(string projectId, [FromBody] UpdateRemoteConfigRequest request)
    {
        request.ProjectId = projectId;
        request.AccessToken = GetAccessToken();
        var result = await _remoteConfigService.UpdateRemoteConfigAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List all versions of the Firebase Remote Config template.</summary>
    [HttpGet("{projectId}/versions")]
    public async Task<IActionResult> ListVersions(string projectId, [FromQuery] int pageSize = 10, [FromQuery] string? pageToken = null)
    {
        var result = await _remoteConfigService.ListVersionsAsync(new ListRemoteConfigVersionsRequest
        {
            ProjectId = projectId,
            AccessToken = GetAccessToken(),
            PageSize = pageSize,
            PageToken = pageToken
        });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Rollback the Firebase Remote Config to a specific version.</summary>
    [HttpPost("{projectId}/rollback")]
    public async Task<IActionResult> Rollback(string projectId, [FromBody] RollbackRemoteConfigRequest request)
    {
        request.ProjectId = projectId;
        request.AccessToken = GetAccessToken();
        var result = await _remoteConfigService.RollbackAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
