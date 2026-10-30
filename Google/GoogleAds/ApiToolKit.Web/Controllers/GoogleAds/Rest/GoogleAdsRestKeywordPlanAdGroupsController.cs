using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - KeywordPlanAdGroups methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestKeywordPlanAdGroupsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestKeywordPlanAdGroupsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Creates, updates, or removes Keyword Plan ad groups.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MutateKeywordPlanAdGroupsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/keywordPlanAdGroups/mutate")]
    public async Task<IActionResult> MutateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "KeywordPlanAdGroups.Mutate", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/keywordPlanAdGroups:mutate", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
