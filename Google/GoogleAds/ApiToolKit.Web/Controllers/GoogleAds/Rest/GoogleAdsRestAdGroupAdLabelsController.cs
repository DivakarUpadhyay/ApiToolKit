using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - AdGroupAdLabels methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestAdGroupAdLabelsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestAdGroupAdLabelsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Creates and removes ad group ad labels.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MutateAdGroupAdLabelsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/adGroupAdLabels/mutate")]
    public async Task<IActionResult> MutateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "AdGroupAdLabels.Mutate", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/adGroupAdLabels:mutate", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
