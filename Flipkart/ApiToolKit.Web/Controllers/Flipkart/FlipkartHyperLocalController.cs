using ApiToolKit.Application.DTOs.Flipkart.HyperLocal;
using ApiToolKit.Application.Interfaces.Services.Flipkart;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Flipkart;

[ApiController]
[Route("api/flipkart/hyperlocal")]
[Produces("application/json")]
public class FlipkartHyperLocalController : ControllerBase
{
    private readonly IFlipkartHyperLocalService _service;
    public FlipkartHyperLocalController(IFlipkartHyperLocalService service) => _service = service;

    /// <summary>Create hyperlocal listings</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateHyperLocalListingRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.CreateHyperLocalListingAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update hyperlocal listings</summary>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateHyperLocalListingRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.UpdateHyperLocalListingAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get hyperlocal listing by SKU IDs</summary>
    [HttpGet("{skuIds}")]
    public async Task<IActionResult> Get(string skuIds)
    {
        var result = await _service.GetHyperLocalListingAsync(new GetHLListingRequest { SkuIds = skuIds });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update hyperlocal listing price</summary>
    [HttpPost("update/price")]
    public async Task<IActionResult> UpdatePrice([FromBody] UpdateHLPriceRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.UpdateHLPriceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update hyperlocal listing inventory</summary>
    [HttpPost("update/inventory")]
    public async Task<IActionResult> UpdateInventory([FromBody] UpdateHLInventoryRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.UpdateHLInventoryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
