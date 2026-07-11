using ApiToolKit.Application.DTOs.Shopee.TopPicks;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/top-picks")]
[Produces("application/json")]
public class ShopeeTopPicksController : ControllerBase
{
    private readonly IShopeeTopPicksService _topPicksService;

    public ShopeeTopPicksController(IShopeeTopPicksService topPicksService)
    {
        _topPicksService = topPicksService;
    }

    /// <summary>Get list of top picks collections</summary>
    [HttpGet("list")]
    public async Task<IActionResult> GetTopPicksList([FromQuery] GetTopPicksListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _topPicksService.GetTopPicksListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new top picks collection</summary>
    [HttpPost]
    public async Task<IActionResult> AddTopPicks([FromBody] AddTopPicksRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _topPicksService.AddTopPicksAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing top picks collection</summary>
    [HttpPost("update")]
    public async Task<IActionResult> UpdateTopPicks([FromBody] UpdateTopPicksRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _topPicksService.UpdateTopPicksAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a top picks collection</summary>
    [HttpPost("delete")]
    public async Task<IActionResult> DeleteTopPicks([FromBody] DeleteTopPicksRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _topPicksService.DeleteTopPicksAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
