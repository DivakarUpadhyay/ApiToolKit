using ApiToolKit.Application.DTOs.DisplayVideo.CustomBiddingAlgorithmScript;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// customBiddingAlgorithms / scripts (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoCustomBiddingAlgorithmScriptController : ControllerBase
{
    private readonly IDisplayVideoCustomBiddingAlgorithmScriptService _customBiddingAlgorithmScriptService;

    public DisplayVideoCustomBiddingAlgorithmScriptController(IDisplayVideoCustomBiddingAlgorithmScriptService customBiddingAlgorithmScriptService)
    {
        _customBiddingAlgorithmScriptService = customBiddingAlgorithmScriptService;
    }

    /// <summary>Lists custom bidding scripts that belong to the given algorithm.</summary>
    [HttpGet("customBiddingAlgorithms/{customBiddingAlgorithmId}/scripts")]
    public async Task<IActionResult> ListScriptsAsync([FromQuery] string accountKey, [FromRoute] string customBiddingAlgorithmId, [FromQuery] string? advertiserId = null, [FromQuery] string? partnerId = null, [FromQuery] string? pageToken = null, [FromQuery] string? orderBy = null, [FromQuery] int? pageSize = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListScriptsRequest { AccountKey = accountKey, CustomBiddingAlgorithmId = customBiddingAlgorithmId, AdvertiserId = advertiserId, PartnerId = partnerId, PageToken = pageToken, OrderBy = orderBy, PageSize = pageSize };
        var result = await _customBiddingAlgorithmScriptService.ListScriptsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a custom bidding script.</summary>
    [HttpGet("customBiddingAlgorithms/{customBiddingAlgorithmId}/scripts/{scriptId}")]
    public async Task<IActionResult> GetScriptAsync([FromQuery] string accountKey, [FromRoute] string customBiddingAlgorithmId, [FromRoute] string scriptId, [FromQuery] string? advertiserId = null, [FromQuery] string? partnerId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetScriptRequest { AccountKey = accountKey, CustomBiddingAlgorithmId = customBiddingAlgorithmId, ScriptId = scriptId, AdvertiserId = advertiserId, PartnerId = partnerId };
        var result = await _customBiddingAlgorithmScriptService.GetScriptAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a new custom bidding script.</summary>
    [HttpPost("customBiddingAlgorithms/{customBiddingAlgorithmId}/scripts")]
    public async Task<IActionResult> CreateScriptAsync([FromRoute] string customBiddingAlgorithmId, [FromBody] CreateScriptRequest request)
    {
        request.CustomBiddingAlgorithmId = customBiddingAlgorithmId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _customBiddingAlgorithmScriptService.CreateScriptAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
