using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - AudienceInsights methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestAudienceInsightsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestAudienceInsightsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Lists date ranges for which audience insights data can be requested.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__ListInsightsEligibleDatesRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("audienceInsights/listInsightsEligibleDates")]
    public async Task<IActionResult> ListInsightsEligibleDatesAsync([FromQuery] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId (whose stored token is used) is required.");
        var result = await _restService.SendAsync(customerId, "AudienceInsights.ListInsightsEligibleDates", HttpMethod.Post, $"audienceInsights:listInsightsEligibleDates", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
