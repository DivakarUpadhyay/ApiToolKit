using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - Recommendations methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestRecommendationsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestRecommendationsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Dismisses given recommendations.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__DismissRecommendationRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/recommendations/dismiss")]
    public async Task<IActionResult> DismissAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Recommendations.Dismiss", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/recommendations:dismiss", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Generates Recommendations based off the requested recommendation_types.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__GenerateRecommendationsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/recommendations/generate")]
    public async Task<IActionResult> GenerateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Recommendations.Generate", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/recommendations:generate", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Applies given recommendations with corresponding apply parameters.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__ApplyRecommendationRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/recommendations/apply")]
    public async Task<IActionResult> ApplyAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Recommendations.Apply", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/recommendations:apply", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
