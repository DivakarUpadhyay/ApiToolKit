using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - PaymentsAccounts methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestPaymentsAccountsController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestPaymentsAccountsController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Returns all payments accounts associated with all managers between the login customer ID and specified serving customer in the hierarchy, inclusive.</summary>
    [HttpGet("customers/{customerId}/paymentsAccounts")]
    public async Task<IActionResult> ListAsync([FromRoute] string customerId)
    {
        var result = await _restService.SendAsync(customerId, "PaymentsAccounts.List", HttpMethod.Get, $"customers/{Uri.EscapeDataString(customerId)}/paymentsAccounts", null, null);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
