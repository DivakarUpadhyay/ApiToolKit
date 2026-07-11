using ApiToolKit.Application.DTOs.GoogleAds.Ad;
using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.GoogleAds;

[ApiController]
[Route("api/googleads/ads")]
[Produces("application/json")]
public class GoogleAdsAdController : ControllerBase
{
    private readonly IGoogleAdsAdService _adService;

    public GoogleAdsAdController(IGoogleAdsAdService adService)
    {
        _adService = adService;
    }

    /// <summary>List ads for a customer, optionally filtered by ad group</summary>
    [HttpGet]
    public async Task<IActionResult> ListAds([FromQuery] string customerId, [FromQuery] string? adGroupResourceName = null, [FromQuery] int pageSize = 50)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId is required.");
        var request = new ListAdsRequest { CustomerId = customerId, AdGroupResourceName = adGroupResourceName, PageSize = pageSize };
        var result = await _adService.ListAdsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new responsive search ad</summary>
    [HttpPost]
    public async Task<IActionResult> CreateResponsiveSearchAd([FromBody] CreateResponsiveSearchAdRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adService.CreateResponsiveSearchAdAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing ad (status)</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateAd([FromBody] UpdateAdRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adService.UpdateAdAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Remove (delete) an ad</summary>
    [HttpDelete("{customerId}/{*resourceName}")]
    public async Task<IActionResult> RemoveAd(string customerId, string resourceName)
    {
        var result = await _adService.RemoveAdAsync(customerId, resourceName);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
