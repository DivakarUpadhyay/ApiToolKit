using ApiToolKit.Application.DTOs.DisplayVideo.Partner;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// partners (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoPartnerController : ControllerBase
{
    private readonly IDisplayVideoPartnerService _partnerService;

    public DisplayVideoPartnerController(IDisplayVideoPartnerService partnerService)
    {
        _partnerService = partnerService;
    }

    /// <summary>Edits targeting options under a single partner.</summary>
    [HttpPost("partners/{partnerId}/editAssignedTargetingOptions")]
    public async Task<IActionResult> EditAssignedTargetingOptionsPartnerAsync([FromRoute] string partnerId, [FromBody] EditAssignedTargetingOptionsPartnerRequest request)
    {
        request.PartnerId = partnerId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _partnerService.EditAssignedTargetingOptionsPartnerAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists partners that are accessible to the current user.</summary>
    [HttpGet("partners")]
    public async Task<IActionResult> ListPartnersAsync([FromQuery] string accountKey, [FromQuery] string? filter = null, [FromQuery] int? pageSize = null, [FromQuery] string? orderBy = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListPartnersRequest { AccountKey = accountKey, Filter = filter, PageSize = pageSize, OrderBy = orderBy, PageToken = pageToken };
        var result = await _partnerService.ListPartnersAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a partner.</summary>
    [HttpGet("partners/{partnerId}")]
    public async Task<IActionResult> GetPartnerAsync([FromQuery] string accountKey, [FromRoute] string partnerId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetPartnerRequest { AccountKey = accountKey, PartnerId = partnerId };
        var result = await _partnerService.GetPartnerAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
