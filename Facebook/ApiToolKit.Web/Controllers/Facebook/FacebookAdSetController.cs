using ApiToolKit.Application.DTOs.Facebook.AdSet;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/adset")]
[Produces("application/json")]
public class FacebookAdSetController : ControllerBase
{
    private readonly IFacebookAdSetService _adSetService;

    public FacebookAdSetController(IFacebookAdSetService adSetService)
    {
        _adSetService = adSetService;
    }

    /// <summary>Create a new ad set</summary>
    [HttpPost]
    public async Task<IActionResult> CreateAdSetAsync([FromBody] CreateAdSetRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adSetService.CreateAdSetAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get an ad set by its ID</summary>
    [HttpGet("{adSetId}")]
    public async Task<IActionResult> GetAdSetAsync(string adSetId, [FromQuery] string accessToken)
    {
        var result = await _adSetService.GetAdSetAsync(adSetId, accessToken);
        return Ok(result);
    }

    /// <summary>Get all ad sets for a campaign</summary>
    [HttpPost("list")]
    public async Task<IActionResult> GetAdSetsAsync([FromBody] GetAdSetsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adSetService.GetAdSetsAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Update an existing ad set</summary>
    [HttpPut("{adSetId}")]
    public async Task<IActionResult> UpdateAdSetAsync([FromBody] UpdateAdSetRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adSetService.UpdateAdSetAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Delete an ad set</summary>
    [HttpDelete("{adSetId}")]
    public async Task<IActionResult> DeleteAdSetAsync(string adSetId, [FromQuery] string accessToken)
    {
        var result = await _adSetService.DeleteAdSetAsync(adSetId, accessToken);
        return Ok(result);
    }

    /// <summary>Get ads belonging to an ad set</summary>
    [HttpGet("{adSetId}/ads")]
    public async Task<IActionResult> GetAdSetAdsAsync(string adSetId, [FromQuery] string accessToken, [FromQuery] int limit = 25)
    {
        var result = await _adSetService.GetAdSetAdsAsync(adSetId, accessToken, limit);
        return Ok(result);
    }
}
