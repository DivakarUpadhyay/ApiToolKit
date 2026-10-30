using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - SmartCampaignSettings methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestSmartCampaignSettingsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestSmartCampaignSettingsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Returns the status of the requested Smart campaign.</summary>
    [HttpGet("customers/{customerId}/smartCampaignSettings/{smartCampaignSettingId}/getSmartCampaignStatus")]
    public async Task<IActionResult> GetSmartCampaignStatusAsync([FromRoute] string customerId, [FromRoute] string smartCampaignSettingId)
    {
        var result = await _restService.SendAsync(customerId, "SmartCampaignSettings.GetSmartCampaignStatus", HttpMethod.Get, $"customers/{Uri.EscapeDataString(customerId)}/smartCampaignSettings/{Uri.EscapeDataString(smartCampaignSettingId)}:getSmartCampaignStatus", null, null);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates Smart campaign settings for campaigns.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MutateSmartCampaignSettingsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/smartCampaignSettings/mutate")]
    public async Task<IActionResult> MutateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "SmartCampaignSettings.Mutate", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/smartCampaignSettings:mutate", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Generates a Performance Max (PMax) draft campaign from an existing Smart campaign, initialized with a status of [CampaignStatus.PAUSED] and a creation status of [CampaignCreatio...</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__GeneratePMaxDraftCampaignRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/smartCampaignSettings/{smartCampaignSettingId}/generatePMaxDraftCampaign")]
    public async Task<IActionResult> GeneratePMaxDraftCampaignAsync([FromRoute] string customerId, [FromRoute] string smartCampaignSettingId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "SmartCampaignSettings.GeneratePMaxDraftCampaign", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/smartCampaignSettings/{Uri.EscapeDataString(smartCampaignSettingId)}:generatePMaxDraftCampaign", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
