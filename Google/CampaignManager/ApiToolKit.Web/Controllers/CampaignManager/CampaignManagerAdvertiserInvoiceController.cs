using ApiToolKit.Application.DTOs.CampaignManager.AdvertiserInvoice;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// advertiserInvoices (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerAdvertiserInvoiceController : ControllerBase
{
    private readonly ICampaignManagerAdvertiserInvoiceService _advertiserInvoiceService;

    public CampaignManagerAdvertiserInvoiceController(ICampaignManagerAdvertiserInvoiceService advertiserInvoiceService)
    {
        _advertiserInvoiceService = advertiserInvoiceService;
    }

    /// <summary>Retrieves a list of invoices for a particular issue month.</summary>
    [HttpGet("userprofiles/{userprofileId}/advertisers/{advertiserId}/invoices")]
    public async Task<IActionResult> ListAdvertiserInvoicesAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string advertiserId, [FromQuery] string? pageToken = null, [FromQuery] int? maxResults = null, [FromQuery] string? issueMonth = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAdvertiserInvoicesRequest { AccountKey = accountKey, UserprofileId = userprofileId, AdvertiserId = advertiserId, PageToken = pageToken, MaxResults = maxResults, IssueMonth = issueMonth };
        var result = await _advertiserInvoiceService.ListAdvertiserInvoicesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
