using ApiToolKit.Application.DTOs.TagManager.Version;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.TagManager;

/// <summary>
/// Container versions and publishing.
/// </summary>
[ApiController]
[Route("api/tagmanager/versions")]
[Produces("application/json")]
public class TagManagerVersionController : ControllerBase
{
    private readonly ITagManagerVersionService _versionService;

    public TagManagerVersionController(ITagManagerVersionService versionService)
    {
        _versionService = versionService;
    }

    /// <summary>Get the live (published) container version.</summary>
    [HttpGet("live")]
    public async Task<IActionResult> GetLiveVersionAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        var request = new GetLiveVersionRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId };
        var result = await _versionService.GetLiveVersionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List all container version headers.</summary>
    [HttpGet("headers")]
    public async Task<IActionResult> ListVersionHeadersAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] bool? includeDeleted = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        var request = new ListVersionHeadersRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, IncludeDeleted = includeDeleted };
        var result = await _versionService.ListVersionHeadersAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get the latest container version header.</summary>
    [HttpGet("headers/latest")]
    public async Task<IActionResult> GetLatestVersionHeaderAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        var request = new GetLatestVersionHeaderRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId };
        var result = await _versionService.GetLatestVersionHeaderAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get one container version.</summary>
    [HttpGet("{versionId}")]
    public async Task<IActionResult> GetVersionAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromRoute] string versionId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        var request = new GetVersionRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, VersionId = versionId };
        var result = await _versionService.GetVersionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Publish a container version (makes it live).</summary>
    [HttpPost("publish")]
    public async Task<IActionResult> PublishVersionAsync([FromBody] PublishVersionRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _versionService.PublishVersionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a container version.</summary>
    [HttpDelete("{versionId}")]
    public async Task<IActionResult> DeleteVersionAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromRoute] string versionId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        var request = new DeleteVersionRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, VersionId = versionId };
        var result = await _versionService.DeleteVersionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a container version name or description.</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateVersionAsync([FromBody] UpdateVersionRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _versionService.UpdateVersionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Set a version as the latest version used for synchronization.</summary>
    [HttpPost("set-latest")]
    public async Task<IActionResult> SetLatestVersionAsync([FromBody] SetLatestVersionRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _versionService.SetLatestVersionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Restore a deleted container version.</summary>
    [HttpPost("undelete")]
    public async Task<IActionResult> UndeleteVersionAsync([FromBody] UndeleteVersionRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _versionService.UndeleteVersionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
