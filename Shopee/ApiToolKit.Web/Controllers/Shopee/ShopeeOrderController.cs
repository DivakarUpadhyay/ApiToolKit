using ApiToolKit.Application.DTOs.Shopee.Order;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/order")]
[Produces("application/json")]
public class ShopeeOrderController : ControllerBase
{
    private readonly IShopeeOrderService _orderService;

    public ShopeeOrderController(IShopeeOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>Get list of orders for a shop</summary>
    [HttpGet("list")]
    public async Task<IActionResult> GetOrderList([FromQuery] GetOrderListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _orderService.GetOrderListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get detailed info of specific orders</summary>
    [HttpPost("detail")]
    public async Task<IActionResult> GetOrderDetail([FromBody] GetOrderDetailRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _orderService.GetOrderDetailAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Cancel an order</summary>
    [HttpPost("cancel")]
    public async Task<IActionResult> CancelOrder([FromBody] CancelOrderRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _orderService.CancelOrderAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Set a seller note on an order</summary>
    [HttpPost("note")]
    public async Task<IActionResult> SetOrderNote([FromBody] SetNoteRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _orderService.SetOrderNoteAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Handle buyer cancellation request (accept or reject)</summary>
    [HttpPost("buyer-cancellation")]
    public async Task<IActionResult> HandleBuyerCancellation([FromBody] HandleBuyerCancellationRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _orderService.HandleBuyerCancellationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get escrow/payment detail for an order</summary>
    [HttpGet("{shopId}/escrow/{orderSn}")]
    public async Task<IActionResult> GetEscrowDetail(long shopId, string orderSn)
    {
        var result = await _orderService.GetEscrowDetailAsync(shopId, orderSn);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
