using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - CampaignDrafts methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestCampaignDraftsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestCampaignDraftsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Creates, updates, or removes campaign drafts.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__MutateCampaignDraftsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/campaignDrafts/mutate")]
    public async Task<IActionResult> MutateAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "CampaignDrafts.Mutate", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/campaignDrafts:mutate", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Promotes the changes in a draft back to the base campaign.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__PromoteCampaignDraftRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/campaignDrafts/{campaignDraftId}/promote")]
    public async Task<IActionResult> PromoteAsync([FromRoute] string customerId, [FromRoute] string campaignDraftId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "CampaignDrafts.Promote", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/campaignDrafts/{Uri.EscapeDataString(campaignDraftId)}:promote", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Returns all errors that occurred during CampaignDraft promote.</summary>
    [HttpGet("customers/{customerId}/campaignDrafts/{campaignDraftId}/listAsyncErrors")]
    public async Task<IActionResult> ListAsyncErrorsAsync([FromRoute] string customerId, [FromRoute] string campaignDraftId, [FromQuery(Name = "pageSize")] string? pageSize = null, [FromQuery(Name = "pageToken")] string? pageToken = null)
    {
        var query = new List<KeyValuePair<string, string?>>();
        query.Add(new KeyValuePair<string, string?>("pageSize", pageSize));
        query.Add(new KeyValuePair<string, string?>("pageToken", pageToken));
        var result = await _restService.SendAsync(customerId, "CampaignDrafts.ListAsyncErrors", HttpMethod.Get, $"customers/{Uri.EscapeDataString(customerId)}/campaignDrafts/{Uri.EscapeDataString(campaignDraftId)}:listAsyncErrors", query, null);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
