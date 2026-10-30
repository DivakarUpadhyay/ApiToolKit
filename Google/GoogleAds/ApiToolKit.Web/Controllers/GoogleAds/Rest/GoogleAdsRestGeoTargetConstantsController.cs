using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - GeoTargetConstants methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestGeoTargetConstantsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestGeoTargetConstantsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Returns GeoTargetConstant suggestions by location name or by resource name.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__SuggestGeoTargetConstantsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("geoTargetConstants/suggest")]
    public async Task<IActionResult> SuggestAsync([FromQuery] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId (whose stored token is used) is required.");
        var result = await _restService.SendAsync(customerId, "GeoTargetConstants.Suggest", HttpMethod.Post, $"geoTargetConstants:suggest", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
