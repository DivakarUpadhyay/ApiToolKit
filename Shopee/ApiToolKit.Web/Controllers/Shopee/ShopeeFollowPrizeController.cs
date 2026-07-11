using ApiToolKit.Application.DTOs.Shopee.FollowPrize;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/follow-prize")]
[Produces("application/json")]
public class ShopeeFollowPrizeController : ControllerBase
{
    private readonly IShopeeFollowPrizeService _followPrizeService;

    public ShopeeFollowPrizeController(IShopeeFollowPrizeService followPrizeService)
    {
        _followPrizeService = followPrizeService;
    }

    /// <summary>Get information about the shop's follow prize</summary>
    [HttpGet("info")]
    public async Task<IActionResult> GetFollowPrizeInfo([FromQuery] GetFollowPrizeInfoRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _followPrizeService.GetFollowPrizeInfoAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new follow prize campaign</summary>
    [HttpPost]
    public async Task<IActionResult> CreateFollowPrize([FromBody] CreateFollowPrizeRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _followPrizeService.CreateFollowPrizeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing follow prize campaign</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateFollowPrize([FromBody] UpdateFollowPrizeRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _followPrizeService.UpdateFollowPrizeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>End a follow prize campaign early</summary>
    [HttpPost("end")]
    public async Task<IActionResult> EndFollowPrize([FromBody] EndFollowPrizeRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _followPrizeService.EndFollowPrizeAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get the list of participants for a follow prize campaign</summary>
    [HttpGet("participants")]
    public async Task<IActionResult> GetFollowPrizeParticipantList([FromQuery] GetFollowPrizeParticipantListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _followPrizeService.GetFollowPrizeParticipantListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get the list of winners for a follow prize campaign</summary>
    [HttpGet("winners")]
    public async Task<IActionResult> GetFollowPrizeWinnerList([FromQuery] GetFollowPrizeWinnerListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _followPrizeService.GetFollowPrizeWinnerListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
