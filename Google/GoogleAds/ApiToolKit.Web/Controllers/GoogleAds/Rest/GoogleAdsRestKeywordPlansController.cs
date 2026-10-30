using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - KeywordPlans methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestKeywordPlansController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestKeywordPlansController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Creates, updates, or removes keyword plans.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MutateKeywordPlansRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/keywordPlans/mutate")]
    public async Task<IActionResult> MutateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "KeywordPlans.Mutate", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/keywordPlans:mutate", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
