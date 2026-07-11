using ApiToolKit.Application.DTOs.Shopify.Analytics;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>Shopify Reports API</summary>
[ApiController]
[Route("api/shopify/reports")]
[Produces("application/json")]
public class ShopifyReportController : ControllerBase
{
    private readonly IShopifyReportService _reportService;

    public ShopifyReportController(IShopifyReportService reportService)
    {
        _reportService = reportService;
    }

    /// <summary>List all reports</summary>
    [HttpGet]
    public async Task<IActionResult> ListReportsAsync([FromQuery] string shopDomain, [FromQuery] int? limit = null, [FromQuery] string? sinceId = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _reportService.ListReportsAsync(shopDomain, limit, sinceId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a report by ID</summary>
    [HttpGet("{reportId}")]
    public async Task<IActionResult> GetReportAsync([FromQuery] string shopDomain, string reportId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _reportService.GetReportAsync(shopDomain, reportId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Count reports</summary>
    [HttpGet("count")]
    public async Task<IActionResult> CountReportsAsync([FromQuery] string shopDomain)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _reportService.CountReportsAsync(shopDomain);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a report</summary>
    [HttpPost]
    public async Task<IActionResult> CreateReportAsync([FromQuery] string shopDomain, [FromBody] CreateReportRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _reportService.CreateReportAsync(shopDomain, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a report</summary>
    [HttpPut("{reportId}")]
    public async Task<IActionResult> UpdateReportAsync([FromQuery] string shopDomain, string reportId, [FromBody] UpdateReportRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _reportService.UpdateReportAsync(shopDomain, reportId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a report</summary>
    [HttpDelete("{reportId}")]
    public async Task<IActionResult> DeleteReportAsync([FromQuery] string shopDomain, string reportId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _reportService.DeleteReportAsync(shopDomain, reportId);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
