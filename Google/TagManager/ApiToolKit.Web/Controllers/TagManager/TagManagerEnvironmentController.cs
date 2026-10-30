using ApiToolKit.Application.DTOs.TagManager.Environment;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.TagManager;

/// <summary>
/// Container environments (staging, QA).
/// </summary>
[ApiController]
[Route("api/tagmanager/environments")]
[Produces("application/json")]
public class TagManagerEnvironmentController : ControllerBase
{
    private readonly ITagManagerEnvironmentService _environmentService;

    public TagManagerEnvironmentController(ITagManagerEnvironmentService environmentService)
    {
        _environmentService = environmentService;
    }

    /// <summary>List environments of a container.</summary>
    [HttpGet]
    public async Task<IActionResult> ListEnvironmentsAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        var request = new ListEnvironmentsRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId };
        var result = await _environmentService.ListEnvironmentsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get one environment.</summary>
    [HttpGet("{environmentId}")]
    public async Task<IActionResult> GetEnvironmentAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromRoute] string environmentId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        var request = new GetEnvironmentRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, EnvironmentId = environmentId };
        var result = await _environmentService.GetEnvironmentAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a user environment.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateEnvironmentAsync([FromBody] CreateEnvironmentRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _environmentService.CreateEnvironmentAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an environment.</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateEnvironmentAsync([FromBody] UpdateEnvironmentRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _environmentService.UpdateEnvironmentAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete an environment.</summary>
    [HttpDelete("{environmentId}")]
    public async Task<IActionResult> DeleteEnvironmentAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromRoute] string environmentId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        var request = new DeleteEnvironmentRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, EnvironmentId = environmentId };
        var result = await _environmentService.DeleteEnvironmentAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Regenerate the authorization code of an environment.</summary>
    [HttpPost("reauthorize")]
    public async Task<IActionResult> ReauthorizeEnvironmentAsync([FromBody] ReauthorizeEnvironmentRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _environmentService.ReauthorizeEnvironmentAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
