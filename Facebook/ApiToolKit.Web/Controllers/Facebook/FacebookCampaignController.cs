using ApiToolKit.Application.DTOs.Facebook.Campaign;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/campaign")]
[Produces("application/json")]
public class FacebookCampaignController : ControllerBase
{
    private readonly IFacebookCampaignService _campaignService;

    public FacebookCampaignController(IFacebookCampaignService campaignService)
    {
        _campaignService = campaignService;
    }

    /// <summary>Create a new ad campaign</summary>
    [HttpPost]
    public async Task<IActionResult> CreateCampaignAsync([FromBody] CreateCampaignRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _campaignService.CreateCampaignAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get a campaign by its ID</summary>
    [HttpGet("{campaignId}")]
    public async Task<IActionResult> GetCampaignAsync(string campaignId, [FromQuery] string accessToken)
    {
        var result = await _campaignService.GetCampaignAsync(campaignId, accessToken);
        return Ok(result);
    }

    /// <summary>Get all campaigns for an ad account</summary>
    [HttpPost("list")]
    public async Task<IActionResult> GetCampaignsAsync([FromBody] GetCampaignsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _campaignService.GetCampaignsAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Update an existing campaign</summary>
    [HttpPut("{campaignId}")]
    public async Task<IActionResult> UpdateCampaignAsync([FromBody] UpdateCampaignRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _campaignService.UpdateCampaignAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Delete a campaign</summary>
    [HttpDelete("{campaignId}")]
    public async Task<IActionResult> DeleteCampaignAsync(string campaignId, [FromQuery] string accessToken)
    {
        var result = await _campaignService.DeleteCampaignAsync(campaignId, accessToken);
        return Ok(result);
    }

    /// <summary>Get ad sets belonging to a campaign</summary>
    [HttpGet("{campaignId}/adsets")]
    public async Task<IActionResult> GetCampaignAdSetsAsync(string campaignId, [FromQuery] string accessToken, [FromQuery] int limit = 25)
    {
        var result = await _campaignService.GetCampaignAdSetsAsync(campaignId, accessToken, limit);
        return Ok(result);
    }

    /// <summary>Get ads belonging to a campaign</summary>
    [HttpGet("{campaignId}/ads")]
    public async Task<IActionResult> GetCampaignAdsAsync(string campaignId, [FromQuery] string accessToken, [FromQuery] int limit = 25)
    {
        var result = await _campaignService.GetCampaignAdsAsync(campaignId, accessToken, limit);
        return Ok(result);
    }
}
