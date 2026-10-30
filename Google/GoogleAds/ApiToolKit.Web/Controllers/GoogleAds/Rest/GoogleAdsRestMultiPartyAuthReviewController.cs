using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - MultiPartyAuthReview methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestMultiPartyAuthReviewController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestMultiPartyAuthReviewController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Resolves the status of a Multi-Party Authorization review.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__ResolveMultiPartyAuthReviewRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/multiPartyAuthReview/resolve")]
    public async Task<IActionResult> ResolveAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "MultiPartyAuthReview.Resolve", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/multiPartyAuthReview:resolve", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
