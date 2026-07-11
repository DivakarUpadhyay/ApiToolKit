using ApiToolKit.Application.DTOs.Shopee.BundleDeal;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;
namespace ApiToolKit.Web.Controllers.Shopee;
[ApiController][Route("api/shopee/bundle-deal")][Produces("application/json")]
public class ShopeeBundleDealController : ControllerBase
{
    private readonly IShopeeBundleDealService _svc;
    public ShopeeBundleDealController(IShopeeBundleDealService svc) { _svc = svc; }
    /// <summary>Create a new bundle deal promotion</summary>
    [HttpPost] public async Task<IActionResult> AddBundleDeal([FromBody] AddBundleDealRequest r) { var res = await _svc.AddBundleDealAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Update an existing bundle deal</summary>
    [HttpPut] public async Task<IActionResult> UpdateBundleDeal([FromBody] UpdateBundleDealRequest r) { var res = await _svc.UpdateBundleDealAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Delete a bundle deal</summary>
    [HttpDelete] public async Task<IActionResult> DeleteBundleDeal([FromBody] DeleteBundleDealRequest r) { var res = await _svc.DeleteBundleDealAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>End/stop an active bundle deal early</summary>
    [HttpPost("end")] public async Task<IActionResult> EndBundleDeal([FromBody] EndBundleDealRequest r) { var res = await _svc.EndBundleDealAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Get details of a specific bundle deal</summary>
    [HttpGet("{shopId}/{bundleDealId}")] public async Task<IActionResult> GetBundleDeal(long shopId, long bundleDealId) { var res = await _svc.GetBundleDealAsync(new GetBundleDealRequest { ShopId = shopId, BundleDealId = bundleDealId }); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Get list of bundle deals for a shop</summary>
    [HttpGet("list")] public async Task<IActionResult> GetBundleDealList([FromQuery] GetBundleDealListRequest r) { var res = await _svc.GetBundleDealListAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Get items in a bundle deal</summary>
    [HttpGet("{shopId}/{bundleDealId}/items")] public async Task<IActionResult> GetBundleDealItem(long shopId, long bundleDealId) { var res = await _svc.GetBundleDealItemAsync(shopId, bundleDealId); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Add items to a bundle deal</summary>
    [HttpPost("items")] public async Task<IActionResult> AddBundleDealItem([FromBody] AddBundleDealItemRequest r) { var res = await _svc.AddBundleDealItemAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Remove items from a bundle deal</summary>
    [HttpDelete("items")] public async Task<IActionResult> DeleteBundleDealItem([FromBody] DeleteBundleDealItemRequest r) { var res = await _svc.DeleteBundleDealItemAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Update items in a bundle deal</summary>
    [HttpPut("items")] public async Task<IActionResult> UpdateBundleDealItem([FromBody] UpdateBundleDealItemRequest r) { var res = await _svc.UpdateBundleDealItemAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
}
