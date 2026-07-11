using ApiToolKit.Application.DTOs.Flipkart.Listing;
using ApiToolKit.Application.Interfaces.Services.Flipkart;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Flipkart;

[ApiController]
[Route("api/flipkart/listings")]
[Produces("application/json")]
public class FlipkartListingController : ControllerBase
{
    private readonly IFlipkartListingService _service;
    public FlipkartListingController(IFlipkartListingService service) => _service = service;

    /// <summary>Create product listings (max 10 SKUs)</summary>
    [HttpPost]
    public async Task<IActionResult> CreateListing([FromBody] CreateListingRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.CreateListingAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update existing product listings</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateListing([FromBody] UpdateListingRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.UpdateListingAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get listings by SKU IDs (comma-separated, max 10)</summary>
    [HttpGet("{skuIds}")]
    public async Task<IActionResult> GetListingsBySku(string skuIds)
    {
        var result = await _service.GetListingsBySkuAsync(new GetListingsBySkuRequest { SkuIds = skuIds });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get detailed listing information for SKUs</summary>
    [HttpPost("details")]
    public async Task<IActionResult> GetListingDetails([FromBody] GetListingDetailsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.GetListingDetailsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Search listings by internal state</summary>
    [HttpPost("product/search")]
    public async Task<IActionResult> SearchListings([FromBody] SearchListingsRequest request)
    {
        var result = await _service.SearchListingsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get all listings with pagination</summary>
    [HttpPost("search")]
    public async Task<IActionResult> GetAllListings([FromBody] GetAllListingsRequest request)
    {
        var result = await _service.GetAllListingsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update pricing for SKUs</summary>
    [HttpPost("update/price")]
    public async Task<IActionResult> UpdatePrice([FromBody] UpdatePriceRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.UpdatePriceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update inventory for SKUs</summary>
    [HttpPost("update/inventory")]
    public async Task<IActionResult> UpdateInventory([FromBody] UpdateInventoryRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.UpdateInventoryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
