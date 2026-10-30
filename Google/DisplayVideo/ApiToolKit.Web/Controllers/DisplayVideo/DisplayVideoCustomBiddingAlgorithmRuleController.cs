using ApiToolKit.Application.DTOs.DisplayVideo.CustomBiddingAlgorithmRule;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// customBiddingAlgorithms / rules (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoCustomBiddingAlgorithmRuleController : ControllerBase
{
    private readonly IDisplayVideoCustomBiddingAlgorithmRuleService _customBiddingAlgorithmRuleService;

    public DisplayVideoCustomBiddingAlgorithmRuleController(IDisplayVideoCustomBiddingAlgorithmRuleService customBiddingAlgorithmRuleService)
    {
        _customBiddingAlgorithmRuleService = customBiddingAlgorithmRuleService;
    }

    /// <summary>Creates a new rules resource.</summary>
    [HttpPost("customBiddingAlgorithms/{customBiddingAlgorithmId}/rules")]
    public async Task<IActionResult> CreateRuleAsync([FromRoute] string customBiddingAlgorithmId, [FromBody] CreateRuleRequest request)
    {
        request.CustomBiddingAlgorithmId = customBiddingAlgorithmId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _customBiddingAlgorithmRuleService.CreateRuleAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a rules resource.</summary>
    [HttpGet("customBiddingAlgorithms/{customBiddingAlgorithmId}/rules/{ruleId}")]
    public async Task<IActionResult> GetRuleAsync([FromQuery] string accountKey, [FromRoute] string customBiddingAlgorithmId, [FromRoute] string ruleId, [FromQuery] string? partnerId = null, [FromQuery] string? advertiserId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetRuleRequest { AccountKey = accountKey, CustomBiddingAlgorithmId = customBiddingAlgorithmId, RuleId = ruleId, PartnerId = partnerId, AdvertiserId = advertiserId };
        var result = await _customBiddingAlgorithmRuleService.GetRuleAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists rules resources that belong to the given algorithm.</summary>
    [HttpGet("customBiddingAlgorithms/{customBiddingAlgorithmId}/rules")]
    public async Task<IActionResult> ListRulesAsync([FromQuery] string accountKey, [FromRoute] string customBiddingAlgorithmId, [FromQuery] int? pageSize = null, [FromQuery] string? advertiserId = null, [FromQuery] string? orderBy = null, [FromQuery] string? partnerId = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListRulesRequest { AccountKey = accountKey, CustomBiddingAlgorithmId = customBiddingAlgorithmId, PageSize = pageSize, AdvertiserId = advertiserId, OrderBy = orderBy, PartnerId = partnerId, PageToken = pageToken };
        var result = await _customBiddingAlgorithmRuleService.ListRulesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
