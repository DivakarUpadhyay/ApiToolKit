using ApiToolKit.Application.DTOs.Shopee.Ads;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopee;

[ApiController]
[Route("api/shopee/ads")]
[Produces("application/json")]
public class ShopeeAdsController : ControllerBase
{
    private readonly IShopeeAdsService _adsService;

    public ShopeeAdsController(IShopeeAdsService adsService)
    {
        _adsService = adsService;
    }

    /// <summary>Get recommended keywords for an item</summary>
    [HttpGet("recommend-keywords")]
    public async Task<IActionResult> GetRecommendKeyword([FromQuery] GetRecommendKeywordRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adsService.GetRecommendKeywordAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new ad campaign</summary>
    [HttpPost("campaign")]
    public async Task<IActionResult> CreateCampaign([FromBody] CreateCampaignRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adsService.CreateCampaignAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get list of ad campaigns</summary>
    [HttpGet("campaigns")]
    public async Task<IActionResult> GetCampaignList([FromQuery] GetCampaignListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adsService.GetCampaignListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get details of a specific campaign</summary>
    [HttpGet("campaign")]
    public async Task<IActionResult> GetCampaign([FromQuery] GetCampaignRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adsService.GetCampaignAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing ad campaign</summary>
    [HttpPost("campaign/update")]
    public async Task<IActionResult> UpdateCampaign([FromBody] UpdateCampaignRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adsService.UpdateCampaignAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete ad campaigns</summary>
    [HttpPost("campaign/delete")]
    public async Task<IActionResult> DeleteCampaign([FromBody] DeleteCampaignRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adsService.DeleteCampaignAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new ad group within a campaign</summary>
    [HttpPost("adgroup")]
    public async Task<IActionResult> CreateAdGroup([FromBody] CreateAdGroupRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adsService.CreateAdGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get list of ad groups for a campaign</summary>
    [HttpGet("adgroups")]
    public async Task<IActionResult> GetAdGroupList([FromQuery] GetAdGroupListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adsService.GetAdGroupListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing ad group</summary>
    [HttpPost("adgroup/update")]
    public async Task<IActionResult> UpdateAdGroup([FromBody] UpdateAdGroupRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adsService.UpdateAdGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new ad within an ad group</summary>
    [HttpPost("ad")]
    public async Task<IActionResult> CreateAd([FromBody] CreateAdRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adsService.CreateAdAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get list of ads within an ad group</summary>
    [HttpGet("ads")]
    public async Task<IActionResult> GetAdList([FromQuery] GetAdListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adsService.GetAdListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing ad</summary>
    [HttpPost("ad/update")]
    public async Task<IActionResult> UpdateAd([FromBody] UpdateAdRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adsService.UpdateAdAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get daily performance data for an ad</summary>
    [HttpGet("ad/performance")]
    public async Task<IActionResult> GetAdDailyPerformance([FromQuery] GetAdDailyPerformanceRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adsService.GetAdDailyPerformanceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get daily performance data for a campaign</summary>
    [HttpGet("campaign/performance")]
    public async Task<IActionResult> GetCampaignDailyPerformance([FromQuery] GetCampaignDailyPerformanceRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adsService.GetCampaignDailyPerformanceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Add keywords to an ad group</summary>
    [HttpPost("keywords")]
    public async Task<IActionResult> AddKeyword([FromBody] AddKeywordRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adsService.AddKeywordAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get list of keywords in an ad group</summary>
    [HttpGet("keywords")]
    public async Task<IActionResult> GetKeywordList([FromQuery] GetKeywordListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adsService.GetKeywordListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update keywords in an ad group</summary>
    [HttpPost("keywords/update")]
    public async Task<IActionResult> UpdateKeyword([FromBody] UpdateKeywordRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adsService.UpdateKeywordAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete keywords from an ad group</summary>
    [HttpPost("keywords/delete")]
    public async Task<IActionResult> DeleteKeyword([FromBody] DeleteKeywordRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adsService.DeleteKeywordAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
