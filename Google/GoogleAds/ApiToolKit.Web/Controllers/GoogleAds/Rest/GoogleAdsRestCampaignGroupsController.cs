using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - CampaignGroups methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestCampaignGroupsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestCampaignGroupsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Creates, updates, or removes campaign groups.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MutateCampaignGroupsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/campaignGroups/mutate")]
    public async Task<IActionResult> MutateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "CampaignGroups.Mutate", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/campaignGroups:mutate", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
