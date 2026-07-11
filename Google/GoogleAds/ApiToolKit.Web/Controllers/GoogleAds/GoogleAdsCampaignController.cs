using ApiToolKit.Application.DTOs.GoogleAds.Campaign;
using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.GoogleAds;

[ApiController]
[Route("api/googleads/campaigns")]
[Produces("application/json")]
public class GoogleAdsCampaignController : ControllerBase
{
    private readonly IGoogleAdsCampaignService _campaignService;

    public GoogleAdsCampaignController(IGoogleAdsCampaignService campaignService)
    {
        _campaignService = campaignService;
    }

    /// <summary>List campaigns for a customer</summary>
    [HttpGet]
    public async Task<IActionResult> ListCampaigns([FromQuery] string customerId, [FromQuery] int pageSize = 50, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId is required.");
        var request = new ListCampaignsRequest { CustomerId = customerId, PageSize = pageSize, PageToken = pageToken };
        var result = await _campaignService.ListCampaignsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific campaign by resource name</summary>
    [HttpGet("{customerId}/{*resourceName}")]
    public async Task<IActionResult> GetCampaign(string customerId, string resourceName)
    {
        var result = await _campaignService.GetCampaignAsync(customerId, resourceName);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new campaign</summary>
    [HttpPost]
    public async Task<IActionResult> CreateCampaign([FromBody] CreateCampaignRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _campaignService.CreateCampaignAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing campaign</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateCampaign([FromBody] UpdateCampaignRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _campaignService.UpdateCampaignAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Remove (delete) a campaign</summary>
    [HttpDelete("{customerId}/{*resourceName}")]
    public async Task<IActionResult> RemoveCampaign(string customerId, string resourceName)
    {
        var result = await _campaignService.RemoveCampaignAsync(customerId, resourceName);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
