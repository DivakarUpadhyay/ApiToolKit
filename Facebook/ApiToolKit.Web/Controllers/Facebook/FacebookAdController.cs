using ApiToolKit.Application.DTOs.Facebook.Ad;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/ad")]
[Produces("application/json")]
public class FacebookAdController : ControllerBase
{
    private readonly IFacebookAdService _adService;

    public FacebookAdController(IFacebookAdService adService)
    {
        _adService = adService;
    }

    /// <summary>Create a new ad</summary>
    [HttpPost]
    public async Task<IActionResult> CreateAdAsync([FromBody] CreateAdRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adService.CreateAdAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get an ad by its ID</summary>
    [HttpGet("{adId}")]
    public async Task<IActionResult> GetAdAsync(string adId, [FromQuery] string accessToken)
    {
        var result = await _adService.GetAdAsync(adId, accessToken);
        return Ok(result);
    }

    /// <summary>Get all ads for an ad set</summary>
    [HttpPost("list")]
    public async Task<IActionResult> GetAdsAsync([FromBody] GetAdsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adService.GetAdsAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Update an existing ad</summary>
    [HttpPut("{adId}")]
    public async Task<IActionResult> UpdateAdAsync([FromBody] UpdateAdRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adService.UpdateAdAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Delete an ad</summary>
    [HttpDelete("{adId}")]
    public async Task<IActionResult> DeleteAdAsync(string adId, [FromQuery] string accessToken)
    {
        var result = await _adService.DeleteAdAsync(adId, accessToken);
        return Ok(result);
    }

    /// <summary>Create an ad creative</summary>
    [HttpPost("creative")]
    public async Task<IActionResult> CreateAdCreativeAsync([FromBody] AdCreativeRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adService.CreateAdCreativeAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get a preview of an ad</summary>
    [HttpGet("{adId}/preview")]
    public async Task<IActionResult> GetAdPreviewAsync(string adId, [FromQuery] string adFormat, [FromQuery] string accessToken)
    {
        var result = await _adService.GetAdPreviewAsync(adId, adFormat, accessToken);
        return Ok(result);
    }

    /// <summary>Get ad creatives for an account</summary>
    [HttpGet("account/{accountId}/creatives")]
    public async Task<IActionResult> GetAdCreativesAsync(string accountId, [FromQuery] string accessToken, [FromQuery] int limit = 25)
    {
        var result = await _adService.GetAdCreativesAsync(accountId, accessToken, limit);
        return Ok(result);
    }
}
