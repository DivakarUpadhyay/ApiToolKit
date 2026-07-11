using ApiToolKit.Application.DTOs.Shopee.Fbs;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/fbs")]
[Produces("application/json")]
public class ShopeeFbsController : ControllerBase
{
    private readonly IShopeeFbsService _svc;

    public ShopeeFbsController(IShopeeFbsService svc)
    {
        _svc = svc;
    }

    /// <summary>Get paginated list of FBS items</summary>
    [HttpGet("items")]
    public async Task<IActionResult> GetItemList([FromQuery] FbsGetItemListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.GetItemListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Enroll a new item into FBS</summary>
    [HttpPost("items")]
    public async Task<IActionResult> CreateItem([FromBody] FbsCreateItemRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.CreateItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update SKU list for an existing FBS item</summary>
    [HttpPut("items")]
    public async Task<IActionResult> UpdateItem([FromBody] FbsUpdateItemRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.UpdateItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Remove one or more items from FBS</summary>
    [HttpPost("items/delete")]
    public async Task<IActionResult> DeleteItem([FromBody] FbsDeleteItemRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.DeleteItemAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
