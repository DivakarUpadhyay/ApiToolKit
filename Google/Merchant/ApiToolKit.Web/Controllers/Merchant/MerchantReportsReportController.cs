using ApiToolKit.Application.DTOs.Merchant.ReportsReport;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// reports (v1)
/// </summary>
[ApiController]
[Route("api/merchant/reports")]
[Produces("application/json")]
public class MerchantReportsReportController : ControllerBase
{
    private readonly IMerchantReportsReportService _reportsReportService;

    public MerchantReportsReportController(IMerchantReportsReportService reportsReportService)
    {
        _reportsReportService = reportsReportService;
    }

    /// <summary>Retrieves a report defined by a search query.</summary>
    [HttpPost("accounts/{accountId}/reports/search")]
    public async Task<IActionResult> SearchReportAsync([FromRoute] string accountId, [FromBody] SearchReportRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _reportsReportService.SearchReportAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
