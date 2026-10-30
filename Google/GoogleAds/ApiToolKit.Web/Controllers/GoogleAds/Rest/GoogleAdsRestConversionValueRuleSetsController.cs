using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - ConversionValueRuleSets methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestConversionValueRuleSetsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestConversionValueRuleSetsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Creates, updates or removes conversion value rule sets.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MutateConversionValueRuleSetsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/conversionValueRuleSets/mutate")]
    public async Task<IActionResult> MutateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "ConversionValueRuleSets.Mutate", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/conversionValueRuleSets:mutate", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
