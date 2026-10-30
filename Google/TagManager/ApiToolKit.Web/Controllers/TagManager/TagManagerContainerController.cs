using ApiToolKit.Application.DTOs.TagManager.Container;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.TagManager;

/// <summary>
/// Containers (web, app, server).
/// </summary>
[ApiController]
[Route("api/tagmanager/containers")]
[Produces("application/json")]
public class TagManagerContainerController : ControllerBase
{
    private readonly ITagManagerContainerService _containerService;

    public TagManagerContainerController(ITagManagerContainerService containerService)
    {
        _containerService = containerService;
    }

    /// <summary>List containers in an account.</summary>
    [HttpGet]
    public async Task<IActionResult> ListContainersAsync([FromQuery] string accountKey, [FromQuery] string accountId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        var request = new ListContainersRequest { AccountKey = accountKey, AccountId = accountId };
        var result = await _containerService.ListContainersAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get one container.</summary>
    [HttpGet("{containerId}")]
    public async Task<IActionResult> GetContainerAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromRoute] string containerId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        var request = new GetContainerRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId };
        var result = await _containerService.GetContainerAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a container.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateContainerAsync([FromBody] CreateContainerRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _containerService.CreateContainerAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a container.</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateContainerAsync([FromBody] UpdateContainerRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _containerService.UpdateContainerAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a container.</summary>
    [HttpDelete("{containerId}")]
    public async Task<IActionResult> DeleteContainerAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromRoute] string containerId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        var request = new DeleteContainerRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId };
        var result = await _containerService.DeleteContainerAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get the GTM install snippet for a container.</summary>
    [HttpGet("{containerId}/snippet")]
    public async Task<IActionResult> GetContainerSnippetAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromRoute] string containerId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        var request = new GetContainerSnippetRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId };
        var result = await _containerService.GetContainerSnippetAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Find a container by a destination id (e.g. G-XXXX, AW-XXXX) or tag id.</summary>
    [HttpGet("lookup")]
    public async Task<IActionResult> LookupContainerAsync([FromQuery] string accountKey, [FromQuery] string? destinationId = null, [FromQuery] string? tagId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new LookupContainerRequest { AccountKey = accountKey, DestinationId = destinationId, TagId = tagId };
        var result = await _containerService.LookupContainerAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Combine another container into this one.</summary>
    [HttpPost("combine")]
    public async Task<IActionResult> CombineContainersAsync([FromBody] CombineContainersRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _containerService.CombineContainersAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Move a Google tag id out of a container into a new destination container.</summary>
    [HttpPost("move-tag-id")]
    public async Task<IActionResult> MoveTagIdAsync([FromBody] MoveTagIdRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _containerService.MoveTagIdAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
