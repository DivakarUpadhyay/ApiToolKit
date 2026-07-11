using ApiToolKit.Application.DTOs.GoogleAds.Conversion;
using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.GoogleAds;

[ApiController]
[Route("api/googleads/conversions")]
[Produces("application/json")]
public class GoogleAdsConversionController : ControllerBase
{
    private readonly IGoogleAdsConversionService _conversionService;

    public GoogleAdsConversionController(IGoogleAdsConversionService conversionService)
    {
        _conversionService = conversionService;
    }

    /// <summary>List conversion actions for a customer</summary>
    [HttpGet]
    public async Task<IActionResult> ListConversionActions([FromQuery] string customerId)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId is required.");
        var request = new ListConversionActionsRequest { CustomerId = customerId };
        var result = await _conversionService.ListConversionActionsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new conversion action</summary>
    [HttpPost]
    public async Task<IActionResult> CreateConversionAction([FromBody] CreateConversionActionRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _conversionService.CreateConversionActionAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
