using ApiToolKit.Application.DTOs.Flipkart.Reports;
using ApiToolKit.Application.Interfaces.Services.Flipkart;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Flipkart;

[ApiController]
[Route("api/flipkart/reports")]
[Produces("application/json")]
public class FlipkartReportController : ControllerBase
{
    private readonly IFlipkartReportService _service;
    public FlipkartReportController(IFlipkartReportService service) => _service = service;

    /// <summary>Request report generation</summary>
    [HttpPost]
    public async Task<IActionResult> RequestReport([FromBody] RequestReportRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.RequestReportAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get report details and download URL</summary>
    [HttpGet("{reportId}")]
    public async Task<IActionResult> GetReportDetail(string reportId)
    {
        var result = await _service.GetReportDetailAsync(new GetReportRequest { ReportId = reportId });
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
