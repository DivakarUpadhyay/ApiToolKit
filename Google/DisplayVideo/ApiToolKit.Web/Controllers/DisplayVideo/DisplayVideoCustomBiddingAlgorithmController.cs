using ApiToolKit.Application.DTOs.DisplayVideo.CustomBiddingAlgorithm;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// customBiddingAlgorithms (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoCustomBiddingAlgorithmController : ControllerBase
{
    private readonly IDisplayVideoCustomBiddingAlgorithmService _customBiddingAlgorithmService;

    public DisplayVideoCustomBiddingAlgorithmController(IDisplayVideoCustomBiddingAlgorithmService customBiddingAlgorithmService)
    {
        _customBiddingAlgorithmService = customBiddingAlgorithmService;
    }

    /// <summary>Creates a rules reference object for an AlgorithmRules file.</summary>
    [HttpGet("customBiddingAlgorithms/{customBiddingAlgorithmId}/uploadRules")]
    public async Task<IActionResult> UploadRulesCustomBiddingAlgorithmAsync([FromQuery] string accountKey, [FromRoute] string customBiddingAlgorithmId, [FromQuery] string? advertiserId = null, [FromQuery] string? partnerId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new UploadRulesCustomBiddingAlgorithmRequest { AccountKey = accountKey, CustomBiddingAlgorithmId = customBiddingAlgorithmId, AdvertiserId = advertiserId, PartnerId = partnerId };
        var result = await _customBiddingAlgorithmService.UploadRulesCustomBiddingAlgorithmAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists custom bidding algorithms that are accessible to the current user and can be used in bidding stratgies.</summary>
    [HttpGet("customBiddingAlgorithms")]
    public async Task<IActionResult> ListCustomBiddingAlgorithmsAsync([FromQuery] string accountKey, [FromQuery] string? filter = null, [FromQuery] string? partnerId = null, [FromQuery] string? pageToken = null, [FromQuery] string? orderBy = null, [FromQuery] int? pageSize = null, [FromQuery] string? advertiserId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListCustomBiddingAlgorithmsRequest { AccountKey = accountKey, Filter = filter, PartnerId = partnerId, PageToken = pageToken, OrderBy = orderBy, PageSize = pageSize, AdvertiserId = advertiserId };
        var result = await _customBiddingAlgorithmService.ListCustomBiddingAlgorithmsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a custom bidding algorithm.</summary>
    [HttpGet("customBiddingAlgorithms/{customBiddingAlgorithmId}")]
    public async Task<IActionResult> GetCustomBiddingAlgorithmAsync([FromQuery] string accountKey, [FromRoute] string customBiddingAlgorithmId, [FromQuery] string? advertiserId = null, [FromQuery] string? partnerId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetCustomBiddingAlgorithmRequest { AccountKey = accountKey, CustomBiddingAlgorithmId = customBiddingAlgorithmId, AdvertiserId = advertiserId, PartnerId = partnerId };
        var result = await _customBiddingAlgorithmService.GetCustomBiddingAlgorithmAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a custom bidding script reference object for a script file.</summary>
    [HttpGet("customBiddingAlgorithms/{customBiddingAlgorithmId}/uploadScript")]
    public async Task<IActionResult> UploadScriptCustomBiddingAlgorithmAsync([FromQuery] string accountKey, [FromRoute] string customBiddingAlgorithmId, [FromQuery] string? advertiserId = null, [FromQuery] string? partnerId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new UploadScriptCustomBiddingAlgorithmRequest { AccountKey = accountKey, CustomBiddingAlgorithmId = customBiddingAlgorithmId, AdvertiserId = advertiserId, PartnerId = partnerId };
        var result = await _customBiddingAlgorithmService.UploadScriptCustomBiddingAlgorithmAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a new custom bidding algorithm.</summary>
    [HttpPost("customBiddingAlgorithms")]
    public async Task<IActionResult> CreateCustomBiddingAlgorithmAsync([FromBody] CreateCustomBiddingAlgorithmRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _customBiddingAlgorithmService.CreateCustomBiddingAlgorithmAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing custom bidding algorithm.</summary>
    [HttpPatch("customBiddingAlgorithms/{customBiddingAlgorithmId}")]
    public async Task<IActionResult> UpdateCustomBiddingAlgorithmAsync([FromRoute] string customBiddingAlgorithmId, [FromBody] UpdateCustomBiddingAlgorithmRequest request)
    {
        request.CustomBiddingAlgorithmId = customBiddingAlgorithmId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _customBiddingAlgorithmService.UpdateCustomBiddingAlgorithmAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
