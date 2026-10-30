using ApiToolKit.Application.DTOs.BusinessProfile.Lodging;
using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.BusinessProfile;

/// <summary>
/// Hotel and lodging attributes (Lodging API).
/// </summary>
[ApiController]
[Route("api/businessprofile/lodging")]
[Produces("application/json")]
public class BusinessProfileLodgingController : ControllerBase
{
    private readonly IBusinessProfileLodgingService _lodgingService;

    public BusinessProfileLodgingController(IBusinessProfileLodgingService lodgingService)
    {
        _lodgingService = lodgingService;
    }

    /// <summary>Get lodging attributes of a hotel location.</summary>
    [HttpGet("{locationId}")]
    public async Task<IActionResult> GetLodgingAsync([FromQuery] string accountKey, [FromRoute] string locationId, [FromQuery] string? readMask = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetLodgingRequest { AccountKey = accountKey, LocationId = locationId, ReadMask = readMask };
        var result = await _lodgingService.GetLodgingAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update lodging attributes (fields in UpdateMask).</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateLodgingAsync([FromBody] UpdateLodgingRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _lodgingService.UpdateLodgingAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get Google-updated lodging attributes.</summary>
    [HttpGet("{locationId}/google-updated")]
    public async Task<IActionResult> GetGoogleUpdatedLodgingAsync([FromQuery] string accountKey, [FromRoute] string locationId, [FromQuery] string? readMask = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetGoogleUpdatedLodgingRequest { AccountKey = accountKey, LocationId = locationId, ReadMask = readMask };
        var result = await _lodgingService.GetGoogleUpdatedLodgingAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
