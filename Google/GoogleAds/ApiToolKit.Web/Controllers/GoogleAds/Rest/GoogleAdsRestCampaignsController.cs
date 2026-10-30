using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - Campaigns methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestCampaignsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestCampaignsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Creates, updates, or removes campaigns.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MutateCampaignsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/campaigns/mutate")]
    public async Task<IActionResult> MutateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Campaigns.Mutate", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/campaigns:mutate", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Enables Brand Guidelines for Performance Max campaigns.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__EnablePMaxBrandGuidelinesRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/campaigns/enablePMaxBrandGuidelines")]
    public async Task<IActionResult> EnablePMaxBrandGuidelinesAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Campaigns.EnablePMaxBrandGuidelines", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/campaigns:enablePMaxBrandGuidelines", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
