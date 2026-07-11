using ApiToolKit.Application.DTOs.Shopee.AccountHealth;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/account-health")]
[Produces("application/json")]
public class ShopeeAccountHealthController : ControllerBase
{
    private readonly IShopeeAccountHealthService _svc;

    public ShopeeAccountHealthController(IShopeeAccountHealthService svc)
    {
        _svc = svc;
    }

    /// <summary>Get the shop's current penalty points and violation history</summary>
    [HttpGet("penalty")]
    public async Task<IActionResult> GetShopPenalty([FromQuery] GetShopPenaltyRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.GetShopPenaltyAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get performance metric values for the shop</summary>
    [HttpGet("metrics")]
    public async Task<IActionResult> GetMetricList([FromQuery] GetMetricListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.GetMetricListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a brief summary of the shop's penalty tier status</summary>
    [HttpGet("brief-penalty")]
    public async Task<IActionResult> GetShopBriefPenalty([FromQuery] GetShopBriefPenaltyRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _svc.GetShopBriefPenaltyAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
