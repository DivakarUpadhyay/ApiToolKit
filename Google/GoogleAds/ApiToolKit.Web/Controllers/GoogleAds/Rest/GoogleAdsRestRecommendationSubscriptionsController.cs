using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - RecommendationSubscriptions methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestRecommendationSubscriptionsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestRecommendationSubscriptionsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Mutates given subscription with corresponding apply parameters.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MutateRecommendationSubscriptionRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/recommendationSubscriptions/mutateRecommendationSubscription")]
    public async Task<IActionResult> MutateRecommendationSubscriptionAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "RecommendationSubscriptions.MutateRecommendationSubscription", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/recommendationSubscriptions:mutateRecommendationSubscription", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
