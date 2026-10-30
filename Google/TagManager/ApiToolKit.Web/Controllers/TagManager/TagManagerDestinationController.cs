using ApiToolKit.Application.DTOs.TagManager.Destination;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.TagManager;

/// <summary>
/// Destinations (GA4, Google Ads) linked to a container.
/// </summary>
[ApiController]
[Route("api/tagmanager/destinations")]
[Produces("application/json")]
public class TagManagerDestinationController : ControllerBase
{
    private readonly ITagManagerDestinationService _destinationService;

    public TagManagerDestinationController(ITagManagerDestinationService destinationService)
    {
        _destinationService = destinationService;
    }

    /// <summary>List destinations linked to a container.</summary>
    [HttpGet]
    public async Task<IActionResult> ListDestinationsAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        var request = new ListDestinationsRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId };
        var result = await _destinationService.ListDestinationsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get one linked destination.</summary>
    [HttpGet("{destinationLinkId}")]
    public async Task<IActionResult> GetDestinationAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromRoute] string destinationLinkId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        var request = new GetDestinationRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, DestinationLinkId = destinationLinkId };
        var result = await _destinationService.GetDestinationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Link a destination (e.g. G-XXXX) to a container.</summary>
    [HttpPost("link")]
    public async Task<IActionResult> LinkDestinationAsync([FromBody] LinkDestinationRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _destinationService.LinkDestinationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
