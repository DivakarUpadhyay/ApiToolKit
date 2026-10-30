using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - CampaignConversionGoals methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestCampaignConversionGoalsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestCampaignConversionGoalsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Creates, updates or removes campaign conversion goals.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MutateCampaignConversionGoalsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/campaignConversionGoals/mutate")]
    public async Task<IActionResult> MutateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "CampaignConversionGoals.Mutate", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/campaignConversionGoals:mutate", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
