using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - ThirdPartyAppAnalyticsLinks methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestThirdPartyAppAnalyticsLinksController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestThirdPartyAppAnalyticsLinksController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Regenerate ThirdPartyAppAnalyticsLink.shareable_link_id that should be provided to the third party when setting up app analytics.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__RegenerateShareableLinkIdRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/thirdPartyAppAnalyticsLinks/{thirdPartyAppAnalyticsLinkId}/regenerateShareableLinkId")]
    public async Task<IActionResult> RegenerateShareableLinkIdAsync([FromRoute] string customerId, [FromRoute] string thirdPartyAppAnalyticsLinkId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "ThirdPartyAppAnalyticsLinks.RegenerateShareableLinkId", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/thirdPartyAppAnalyticsLinks/{Uri.EscapeDataString(thirdPartyAppAnalyticsLinkId)}:regenerateShareableLinkId", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
