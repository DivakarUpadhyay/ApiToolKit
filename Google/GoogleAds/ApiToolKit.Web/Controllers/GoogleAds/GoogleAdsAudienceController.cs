using ApiToolKit.Application.DTOs.GoogleAds.Audience;
using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.GoogleAds;

[ApiController]
[Route("api/googleads/audiences")]
[Produces("application/json")]
public class GoogleAdsAudienceController : ControllerBase
{
    private readonly IGoogleAdsAudienceService _audienceService;

    public GoogleAdsAudienceController(IGoogleAdsAudienceService audienceService)
    {
        _audienceService = audienceService;
    }

    /// <summary>List audiences for a customer</summary>
    [HttpGet]
    public async Task<IActionResult> ListAudiences([FromQuery] string customerId, [FromQuery] int pageSize = 50)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId is required.");
        var request = new ListAudiencesRequest { CustomerId = customerId, PageSize = pageSize };
        var result = await _audienceService.ListAudiencesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new audience</summary>
    [HttpPost]
    public async Task<IActionResult> CreateAudience([FromBody] CreateAudienceRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _audienceService.CreateAudienceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
