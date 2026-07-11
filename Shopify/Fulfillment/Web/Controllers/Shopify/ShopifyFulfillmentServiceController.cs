using ApiToolKit.Application.DTOs.Shopify.Fulfillment;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

/// <summary>
/// Manages Shopify Fulfillment Services
/// </summary>
[ApiController]
[Route("api/shopify/fulfillment-services")]
[Produces("application/json")]
public class ShopifyFulfillmentServiceController : ControllerBase
{
    private readonly IShopifyFulfillmentServiceService _fulfillmentServiceService;

    public ShopifyFulfillmentServiceController(IShopifyFulfillmentServiceService fulfillmentServiceService)
    {
        _fulfillmentServiceService = fulfillmentServiceService;
    }

    /// <summary>
    /// List all fulfillment services
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> ListFulfillmentServices([FromQuery] string shopDomain, [FromQuery] string? scope = null)
    {
        var result = await _fulfillmentServiceService.ListFulfillmentServicesAsync(shopDomain, scope);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode ?? 500, new { error = result.ErrorMessage });
        return Ok(result.Data);
    }

    /// <summary>
    /// Get a specific fulfillment service
    /// </summary>
    [HttpGet("{fulfillmentServiceId}")]
    public async Task<IActionResult> GetFulfillmentService(string fulfillmentServiceId, [FromQuery] string shopDomain)
    {
        var result = await _fulfillmentServiceService.GetFulfillmentServiceAsync(shopDomain, fulfillmentServiceId);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode ?? 500, new { error = result.ErrorMessage });
        return Ok(result.Data);
    }

    /// <summary>
    /// Create a new fulfillment service
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateFulfillmentService([FromQuery] string shopDomain, [FromBody] CreateFulfillmentServiceRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _fulfillmentServiceService.CreateFulfillmentServiceAsync(shopDomain, request);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode ?? 500, new { error = result.ErrorMessage });
        return StatusCode(201, result.Data);
    }

    /// <summary>
    /// Update an existing fulfillment service
    /// </summary>
    [HttpPut("{fulfillmentServiceId}")]
    public async Task<IActionResult> UpdateFulfillmentService(string fulfillmentServiceId, [FromQuery] string shopDomain, [FromBody] UpdateFulfillmentServiceRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _fulfillmentServiceService.UpdateFulfillmentServiceAsync(shopDomain, fulfillmentServiceId, request);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode ?? 500, new { error = result.ErrorMessage });
        return Ok(result.Data);
    }

    /// <summary>
    /// Delete a fulfillment service
    /// </summary>
    [HttpDelete("{fulfillmentServiceId}")]
    public async Task<IActionResult> DeleteFulfillmentService(string fulfillmentServiceId, [FromQuery] string shopDomain)
    {
        var result = await _fulfillmentServiceService.DeleteFulfillmentServiceAsync(shopDomain, fulfillmentServiceId);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode ?? 500, new { error = result.ErrorMessage });
        return Ok(new { deleted = result.Data });
    }
}
