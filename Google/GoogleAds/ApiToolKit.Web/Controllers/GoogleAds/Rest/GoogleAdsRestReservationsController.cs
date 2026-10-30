using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - Reservations methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestReservationsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestReservationsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Proposes quotes for booking campaigns.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__QuoteCampaignsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/reservations/quoteCampaigns")]
    public async Task<IActionResult> QuoteCampaignsAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Reservations.QuoteCampaigns", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/reservations:quoteCampaigns", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Books the requested campaigns.</summary>
    /// <remarks>Body: GoogleAdsGoogleadsV26Services__BookCampaignsRequest JSON as documented at developers.google.com/google-ads/api/rest/reference/rest/v26.</remarks>
    [HttpPost("customers/{customerId}/reservations/bookCampaigns")]
    public async Task<IActionResult> BookCampaignsAsync([FromRoute] string customerId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonObject? body)
    {
        var result = await _restService.SendAsync(customerId, "Reservations.BookCampaigns", HttpMethod.Post, $"customers/{Uri.EscapeDataString(customerId)}/reservations:bookCampaigns", null, body);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
