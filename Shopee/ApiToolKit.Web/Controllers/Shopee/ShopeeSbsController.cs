using ApiToolKit.Application.DTOs.Shopee.Sbs;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/sbs")]
[Produces("application/json")]
public class ShopeeSbsController : ControllerBase
{
    private readonly IShopeeSbsService _svc;

    public ShopeeSbsController(IShopeeSbsService svc)
    {
        _svc = svc;
    }

    /// <summary>Get paginated list of SBS items</summary>
    [HttpGet("items")]
    public async Task<IActionResult> GetItemList([FromQuery] SbsGetItemListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.GetItemListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new SBS item</summary>
    [HttpPost("items")]
    public async Task<IActionResult> CreateItem([FromBody] SbsCreateItemRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.CreateItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing SBS item's price and stock</summary>
    [HttpPut("items")]
    public async Task<IActionResult> UpdateItem([FromBody] SbsUpdateItemRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.UpdateItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete one or more SBS items</summary>
    [HttpPost("items/delete")]
    public async Task<IActionResult> DeleteItem([FromBody] SbsDeleteItemRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.DeleteItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get details of a single SBS item</summary>
    [HttpGet("item")]
    public async Task<IActionResult> GetItem([FromQuery] SbsGetItemRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.GetItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
