using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - SharedSets methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestSharedSetsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestSharedSetsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Creates, updates, or removes shared sets.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MutateSharedSetsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/sharedSets/mutate")]
    public async Task<IActionResult> MutateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "SharedSets.Mutate", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/sharedSets:mutate", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
