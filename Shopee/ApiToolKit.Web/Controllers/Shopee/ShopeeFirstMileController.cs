using ApiToolKit.Application.DTOs.Shopee.FirstMile;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/first-mile")]
[Produces("application/json")]
public class ShopeeFirstMileController : ControllerBase
{
    private readonly IShopeeFirstMileService _svc;

    public ShopeeFirstMileController(IShopeeFirstMileService svc)
    {
        _svc = svc;
    }

    /// <summary>Generate first-mile tracking numbers</summary>
    [HttpPost("generate")]
    public async Task<IActionResult> GenerateFirstMileTrackingNumber([FromBody] GenerateFirstMileTrackingNumberRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.GenerateFirstMileTrackingNumberAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get first-mile tracking number details</summary>
    [HttpGet("tracking-number")]
    public async Task<IActionResult> GetFirstMileTrackingNumber([FromQuery] GetFirstMileTrackingNumberRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.GetFirstMileTrackingNumberAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Upload first-mile tracking number with order bindings</summary>
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFirstMileTrackingNumber([FromBody] UploadFirstMileTrackingNumberRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.UploadFirstMileTrackingNumberAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Unbind a first-mile tracking number from its orders</summary>
    [HttpPost("unbind")]
    public async Task<IActionResult> UnbindFirstMileTrackingNumber([FromBody] UnbindFirstMileTrackingNumberRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.UnbindFirstMileTrackingNumberAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get list of orders not yet bound to a first-mile tracking number</summary>
    [HttpGet("unbind-orders")]
    public async Task<IActionResult> GetUnbindOrderList([FromQuery] GetUnbindOrderListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.GetUnbindOrderListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a first-mile channel shipment for a batch of orders</summary>
    [HttpPost("create-channel")]
    public async Task<IActionResult> CreateChannel([FromBody] CreateChannelRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.CreateChannelAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get available first-mile logistics channels</summary>
    [HttpGet("channels")]
    public async Task<IActionResult> GetFirstMileChannelList([FromQuery] GetFirstMileChannelListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.GetFirstMileChannelListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get paginated list of first-mile tracking entries</summary>
    [HttpGet("tracking-list")]
    public async Task<IActionResult> GetFirstMileTrackingList([FromQuery] GetFirstMileTrackingListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.GetFirstMileTrackingListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
