using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace ApiToolKit.Web.Controllers.GoogleAds.Rest;

/// <summary>
/// Google Ads API v26 - Invoices methods. Request and response bodies are passed through exactly as Google documents them.
/// </summary>
[ApiController]
[Route("api/googleads/rest")]
[Produces("application/json")]
public class GoogleAdsRestInvoicesController : ControllerBase
{
    private readonly IGoogleAdsRestService _restService;

    public GoogleAdsRestInvoicesController(IGoogleAdsRestService restService)
    {
        _restService = restService;
    }

    /// <summary>Returns all invoices associated with a billing setup, for a given month.</summary>
    [HttpGet("customers/{customerId}/invoices")]
    public async Task<IActionResult> ListAsync([FromRoute] string customerId, [FromQuery(Name = "issueYear")] string? issueYear = null, [FromQuery(Name = "issueMonth")] string? issueMonth = null, [FromQuery(Name = "billingSetup")] string? billingSetup = null, [FromQuery(Name = "includeGranularLevelInvoiceDetails")] string? includeGranularLevelInvoiceDetails = null)
    {
        var query = new List<KeyValuePair<string, string?>>();
        query.Add(new KeyValuePair<string, string?>("issueYear", issueYear));
        query.Add(new KeyValuePair<string, string?>("issueMonth", issueMonth));
        query.Add(new KeyValuePair<string, string?>("billingSetup", billingSetup));
        query.Add(new KeyValuePair<string, string?>("includeGranularLevelInvoiceDetails", includeGranularLevelInvoiceDetails));
        var result = await _restService.SendAsync(customerId, "Invoices.List", HttpMethod.Get, $"customers/{Uri.EscapeDataString(customerId)}/invoices", query, null);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}
