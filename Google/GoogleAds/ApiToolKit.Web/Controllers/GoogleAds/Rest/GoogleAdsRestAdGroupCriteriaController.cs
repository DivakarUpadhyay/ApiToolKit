using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - AdGroupCriteria methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestAdGroupCriteriaController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestAdGroupCriteriaController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Creates, updates, or removes criteria.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MutateAdGroupCriteriaRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/adGroupCriteria/mutate")]
    public async Task<IActionResult> MutateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "AdGroupCriteria.Mutate", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/adGroupCriteria:mutate", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
