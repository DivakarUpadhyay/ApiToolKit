using ApiToolKit.Application.DTOs.GoogleAds.Report;
using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.GoogleAds;

[ApiController]
[Route("api/googleads/reports")]
[Produces("application/json")]
public class GoogleAdsReportController : ControllerBase
{
    private readonly IGoogleAdsReportService _reportService;

    public GoogleAdsReportController(IGoogleAdsReportService reportService)
    {
        _reportService = reportService;
    }

    /// <summary>Run a GAQL report query (e.g. SELECT campaign.id, campaign.name, metrics.impressions FROM campaign ORDER BY metrics.impressions DESC)</summary>
    [HttpPost("run")]
    public async Task<IActionResult> RunReport([FromBody] RunReportRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _reportService.RunReportAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
