using ApiToolKit.Application.DTOs.DisplayVideo.Invoice;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.DisplayVideo;

/// <summary>
/// invoices (v1)
/// </summary>
[ApiController]
[Route("api/displayvideo")]
[Produces("application/json")]
public class DisplayVideoInvoiceController : ControllerBase
{
    private readonly IDisplayVideoInvoiceService _invoiceService;

    public DisplayVideoInvoiceController(IDisplayVideoInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    /// <summary>Retrieves the invoice currency used by an advertiser in a given month.</summary>
    [HttpGet("advertisers/{advertiserId}/invoices/lookupInvoiceCurrency")]
    public async Task<IActionResult> LookupInvoiceCurrencyAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromQuery] string? invoiceMonth = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new LookupInvoiceCurrencyRequest { AccountKey = accountKey, AdvertiserId = advertiserId, InvoiceMonth = invoiceMonth };
        var result = await _invoiceService.LookupInvoiceCurrencyAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists invoices posted for an advertiser in a given month.</summary>
    [HttpGet("advertisers/{advertiserId}/invoices")]
    public async Task<IActionResult> ListInvoicesAsync([FromQuery] string accountKey, [FromRoute] string advertiserId, [FromQuery] string? pageToken = null, [FromQuery] int? pageSize = null, [FromQuery] string? loiSapinInvoiceType = null, [FromQuery] string? issueMonth = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListInvoicesRequest { AccountKey = accountKey, AdvertiserId = advertiserId, PageToken = pageToken, PageSize = pageSize, LoiSapinInvoiceType = loiSapinInvoiceType, IssueMonth = issueMonth };
        var result = await _invoiceService.ListInvoicesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
