using ApiToolKit.Application.DTOs.DisplayVideo.FirstPartyAndPartnerAudience;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// firstPartyAndPartnerAudiences (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoFirstPartyAndPartnerAudienceController : ControllerBase
{
    private readonly IDisplayVideoFirstPartyAndPartnerAudienceService _firstPartyAndPartnerAudienceService;

    public DisplayVideoFirstPartyAndPartnerAudienceController(IDisplayVideoFirstPartyAndPartnerAudienceService firstPartyAndPartnerAudienceService)
    {
        _firstPartyAndPartnerAudienceService = firstPartyAndPartnerAudienceService;
    }

    /// <summary>Updates the member list of a Customer Match audience.</summary>
    [HttpPost("firstPartyAndPartnerAudiences/{firstPartyAndPartnerAudienceId}/editCustomerMatchMembers")]
    public async Task<IActionResult> EditCustomerMatchMembersFirstPartyAndPartnerAudienceAsync([FromRoute] string firstPartyAndPartnerAudienceId, [FromBody] EditCustomerMatchMembersFirstPartyAndPartnerAudienceRequest request)
    {
        request.FirstPartyAndPartnerAudienceId = firstPartyAndPartnerAudienceId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _firstPartyAndPartnerAudienceService.EditCustomerMatchMembersFirstPartyAndPartnerAudienceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists first party and partner audiences.</summary>
    [HttpGet("firstPartyAndPartnerAudiences")]
    public async Task<IActionResult> ListFirstPartyAndPartnerAudiencesAsync([FromQuery] string accountKey, [FromQuery] string? orderBy = null, [FromQuery] string? filter = null, [FromQuery] int? pageSize = null, [FromQuery] string? advertiserId = null, [FromQuery] string? partnerId = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListFirstPartyAndPartnerAudiencesRequest { AccountKey = accountKey, OrderBy = orderBy, Filter = filter, PageSize = pageSize, AdvertiserId = advertiserId, PartnerId = partnerId, PageToken = pageToken };
        var result = await _firstPartyAndPartnerAudienceService.ListFirstPartyAndPartnerAudiencesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets a first party or partner audience.</summary>
    [HttpGet("firstPartyAndPartnerAudiences/{firstPartyAndPartnerAudienceId}")]
    public async Task<IActionResult> GetFirstPartyAndPartnerAudienceAsync([FromQuery] string accountKey, [FromRoute] string firstPartyAndPartnerAudienceId, [FromQuery] string? partnerId = null, [FromQuery] string? advertiserId = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetFirstPartyAndPartnerAudienceRequest { AccountKey = accountKey, FirstPartyAndPartnerAudienceId = firstPartyAndPartnerAudienceId, PartnerId = partnerId, AdvertiserId = advertiserId };
        var result = await _firstPartyAndPartnerAudienceService.GetFirstPartyAndPartnerAudienceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing FirstPartyAndPartnerAudience.</summary>
    [HttpPatch("firstPartyAndPartnerAudiences/{firstPartyAndPartnerAudienceId}")]
    public async Task<IActionResult> UpdateFirstPartyAndPartnerAudienceAsync([FromRoute] string firstPartyAndPartnerAudienceId, [FromBody] UpdateFirstPartyAndPartnerAudienceRequest request)
    {
        request.FirstPartyAndPartnerAudienceId = firstPartyAndPartnerAudienceId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _firstPartyAndPartnerAudienceService.UpdateFirstPartyAndPartnerAudienceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates a FirstPartyAndPartnerAudience.</summary>
    [HttpPost("firstPartyAndPartnerAudiences")]
    public async Task<IActionResult> CreateFirstPartyAndPartnerAudienceAsync([FromBody] CreateFirstPartyAndPartnerAudienceRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _firstPartyAndPartnerAudienceService.CreateFirstPartyAndPartnerAudienceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
