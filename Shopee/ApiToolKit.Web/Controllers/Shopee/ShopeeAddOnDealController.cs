using ApiToolKit.Application.DTOs.Shopee.AddOnDeal;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;
namespace ApiToolKit.Web.Controllers.Shopee;
[ApiController][Route("api/shopee/add-on-deal")][Produces("application/json")]
public class ShopeeAddOnDealController : ControllerBase
{
    private readonly IShopeeAddOnDealService _svc;
    public ShopeeAddOnDealController(IShopeeAddOnDealService svc) { _svc = svc; }
    /// <summary>Create a new add-on deal promotion</summary>
    [HttpPost] public async Task<IActionResult> AddAddOnDeal([FromBody] AddAddOnDealRequest r) { var res = await _svc.AddAddOnDealAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Update an existing add-on deal</summary>
    [HttpPut] public async Task<IActionResult> UpdateAddOnDeal([FromBody] UpdateAddOnDealRequest r) { var res = await _svc.UpdateAddOnDealAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Delete an add-on deal</summary>
    [HttpDelete] public async Task<IActionResult> DeleteAddOnDeal([FromBody] DeleteAddOnDealRequest r) { var res = await _svc.DeleteAddOnDealAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>End an active add-on deal early</summary>
    [HttpPost("end")] public async Task<IActionResult> EndAddOnDeal([FromBody] EndAddOnDealRequest r) { var res = await _svc.EndAddOnDealAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Get details of a specific add-on deal</summary>
    [HttpGet("{shopId}/{addOnDealId}")] public async Task<IActionResult> GetAddOnDeal(long shopId, long addOnDealId) { var res = await _svc.GetAddOnDealAsync(new GetAddOnDealRequest { ShopId = shopId, AddOnDealId = addOnDealId }); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Get list of add-on deals for a shop</summary>
    [HttpGet("list")] public async Task<IActionResult> GetAddOnDealList([FromQuery] GetAddOnDealListRequest r) { var res = await _svc.GetAddOnDealListAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Add main items to an add-on deal</summary>
    [HttpPost("main-items")] public async Task<IActionResult> AddMainItem([FromBody] AddOnDealMainItemRequest r) { var res = await _svc.AddAddOnDealMainItemAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Remove main items from an add-on deal</summary>
    [HttpDelete("main-items")] public async Task<IActionResult> DeleteMainItem([FromBody] DeleteAddOnItemRequest r) { var res = await _svc.DeleteAddOnDealMainItemAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Update main items in an add-on deal</summary>
    [HttpPut("main-items")] public async Task<IActionResult> UpdateMainItem([FromBody] AddOnDealMainItemRequest r) { var res = await _svc.UpdateAddOnDealMainItemAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Get main items in an add-on deal</summary>
    [HttpGet("{shopId}/{addOnDealId}/main-items")] public async Task<IActionResult> GetMainItem(long shopId, long addOnDealId) { var res = await _svc.GetAddOnDealMainItemAsync(shopId, addOnDealId); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Add sub/gift items to an add-on deal</summary>
    [HttpPost("sub-items")] public async Task<IActionResult> AddSubItem([FromBody] AddOnDealSubItemRequest r) { var res = await _svc.AddAddOnDealSubItemAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Remove sub items from an add-on deal</summary>
    [HttpDelete("sub-items")] public async Task<IActionResult> DeleteSubItem([FromBody] DeleteAddOnItemRequest r) { var res = await _svc.DeleteAddOnDealSubItemAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Update sub items in an add-on deal</summary>
    [HttpPut("sub-items")] public async Task<IActionResult> UpdateSubItem([FromBody] UpdateAddOnSubItemRequest r) { var res = await _svc.UpdateAddOnDealSubItemAsync(r); return res.Success ? Ok(res) : BadRequest(res); }
    /// <summary>Get sub items in an add-on deal</summary>
    [HttpGet("{shopId}/{addOnDealId}/sub-items")] public async Task<IActionResult> GetSubItem(long shopId, long addOnDealId) { var res = await _svc.GetAddOnDealSubItemAsync(shopId, addOnDealId); return res.Success ? Ok(res) : BadRequest(res); }
}
