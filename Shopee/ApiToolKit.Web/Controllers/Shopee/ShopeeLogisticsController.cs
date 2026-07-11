using ApiToolKit.Application.DTOs.Shopee.Logistics;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/logistics")]
[Produces("application/json")]
public class ShopeeLogisticsController : ControllerBase
{
    private readonly IShopeeLogisticsService _logisticsService;

    public ShopeeLogisticsController(IShopeeLogisticsService logisticsService)
    {
        _logisticsService = logisticsService;
    }

    /// <summary>Get shipping parameters / logistic info for an order</summary>
    [HttpGet("info")]
    public async Task<IActionResult> GetLogisticInfo([FromQuery] GetLogisticInfoRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _logisticsService.GetLogisticInfoAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Initialize logistics for an order</summary>
    [HttpPost("init")]
    public async Task<IActionResult> InitLogistics([FromBody] InitLogisticsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _logisticsService.InitLogisticsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Confirm order shipment</summary>
    [HttpPost("ship")]
    public async Task<IActionResult> ShipOrder([FromBody] ShipOrderRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _logisticsService.ShipOrderAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get tracking number for an order</summary>
    [HttpGet("tracking-number")]
    public async Task<IActionResult> GetTrackingNumber([FromQuery] GetTrackingNumberRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _logisticsService.GetTrackingNumberAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get tracking info for an order</summary>
    [HttpGet("{shopId}/tracking/{orderSn}")]
    public async Task<IActionResult> GetOrderLogistics(long shopId, string orderSn)
    {
        var result = await _logisticsService.GetOrderLogisticsAsync(shopId, orderSn);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update logistics / shipping order details</summary>
    [HttpPut("update")]
    public async Task<IActionResult> UpdateLogisticsOrder([FromBody] UpdateLogisticsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _logisticsService.UpdateLogisticsOrderAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get list of pickup addresses for a shop</summary>
    [HttpGet("addresses")]
    public async Task<IActionResult> GetAddressList([FromQuery] GetAddressListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _logisticsService.GetAddressListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
