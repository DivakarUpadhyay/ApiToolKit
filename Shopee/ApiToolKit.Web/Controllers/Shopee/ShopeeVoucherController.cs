using ApiToolKit.Application.DTOs.Shopee.Voucher;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/voucher")]
[Produces("application/json")]
public class ShopeeVoucherController : ControllerBase
{
    private readonly IShopeeVoucherService _voucherService;

    public ShopeeVoucherController(IShopeeVoucherService voucherService)
    {
        _voucherService = voucherService;
    }

    /// <summary>Create a new shop voucher</summary>
    [HttpPost]
    public async Task<IActionResult> CreateVoucher([FromBody] CreateVoucherRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _voucherService.CreateVoucherAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a voucher</summary>
    [HttpDelete]
    public async Task<IActionResult> DeleteVoucher([FromBody] DeleteVoucherRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _voucherService.DeleteVoucherAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing voucher</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateVoucher([FromBody] UpdateVoucherRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _voucherService.UpdateVoucherAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>End/stop an active voucher early</summary>
    [HttpPost("end")]
    public async Task<IActionResult> EndVoucher([FromBody] EndVoucherRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _voucherService.EndVoucherAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get details of a specific voucher</summary>
    [HttpGet("{shopId}/{voucherId}")]
    public async Task<IActionResult> GetVoucher(long shopId, long voucherId)
    {
        var result = await _voucherService.GetVoucherAsync(new GetVoucherRequest { ShopId = shopId, VoucherId = voucherId });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get list of all vouchers for a shop</summary>
    [HttpGet("list")]
    public async Task<IActionResult> GetVoucherList([FromQuery] GetVoucherListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _voucherService.GetVoucherListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
