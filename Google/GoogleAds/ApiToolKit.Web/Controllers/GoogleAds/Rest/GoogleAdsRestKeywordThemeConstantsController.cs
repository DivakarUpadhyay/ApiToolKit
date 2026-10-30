using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - KeywordThemeConstants methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestKeywordThemeConstantsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestKeywordThemeConstantsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Returns KeywordThemeConstant suggestions by keyword themes.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__SuggestKeywordThemeConstantsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("keywordThemeConstants/suggest")]
    public async Task<IActionResult> SuggestAsync([FromQuery] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId (whose stored token is used) is required.");
        var result = await _restService.SendAsync(customerId, "KeywordThemeConstants.Suggest", HttpMethod.Post, $"keywordThemeConstants:suggest", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
