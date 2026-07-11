using ApiToolKit.Application.DTOs.GoogleAds.BiddingStrategy;
using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.GoogleAds;

[ApiController]
[Route("api/googleads/bidding-strategies")]
[Produces("application/json")]
public class GoogleAdsBiddingStrategyController : ControllerBase
{
    private readonly IGoogleAdsBiddingStrategyService _biddingStrategyService;

    public GoogleAdsBiddingStrategyController(IGoogleAdsBiddingStrategyService biddingStrategyService)
    {
        _biddingStrategyService = biddingStrategyService;
    }

    /// <summary>List portfolio bidding strategies for a customer</summary>
    [HttpGet]
    public async Task<IActionResult> ListBiddingStrategies([FromQuery] string customerId)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId is required.");
        var request = new ListBiddingStrategiesRequest { CustomerId = customerId };
        var result = await _biddingStrategyService.ListBiddingStrategiesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new portfolio bidding strategy (TARGET_CPA, TARGET_ROAS, or MAXIMIZE_CONVERSIONS)</summary>
    [HttpPost]
    public async Task<IActionResult> CreateBiddingStrategy([FromBody] CreateBiddingStrategyRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _biddingStrategyService.CreateBiddingStrategyAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
