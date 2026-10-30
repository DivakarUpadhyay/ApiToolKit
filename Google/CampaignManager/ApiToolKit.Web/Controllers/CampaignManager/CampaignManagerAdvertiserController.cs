using ApiToolKit.Application.DTOs.CampaignManager.Advertiser;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.CampaignManager;

/// <summary>
/// advertisers (v1)
/// </summary>
[ApiController]
[Route("api/campaignmanager")]
[Produces("application/json")]
public class CampaignManagerAdvertiserController : ControllerBase
{
    private readonly ICampaignManagerAdvertiserService _advertiserService;

    public CampaignManagerAdvertiserController(ICampaignManagerAdvertiserService advertiserService)
    {
        _advertiserService = advertiserService;
    }

    /// <summary>Inserts a new advertiser.</summary>
    [HttpPost("userprofiles/{userprofileId}/advertisers")]
    public async Task<IActionResult> InsertAdvertiserAsync([FromRoute] string userprofileId, [FromBody] InsertAdvertiserRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _advertiserService.InsertAdvertiserAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Gets one advertiser by ID.</summary>
    [HttpGet("userprofiles/{userprofileId}/advertisers/{advertiserId}")]
    public async Task<IActionResult> GetAdvertiserAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromRoute] string advertiserId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetAdvertiserRequest { AccountKey = accountKey, UserprofileId = userprofileId, AdvertiserId = advertiserId };
        var result = await _advertiserService.GetAdvertiserAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing advertiser.</summary>
    [HttpPatch("userprofiles/{userprofileId}/advertisers")]
    public async Task<IActionResult> UpdateAdvertiserAsync([FromRoute] string userprofileId, [FromBody] UpdateAdvertiserRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _advertiserService.UpdateAdvertiserAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates an existing advertiser.</summary>
    [HttpPut("userprofiles/{userprofileId}/advertisers")]
    public async Task<IActionResult> UpdateAdvertiserAltAsync([FromRoute] string userprofileId, [FromBody] UpdateAdvertiserAltRequest request)
    {
        request.UserprofileId = userprofileId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _advertiserService.UpdateAdvertiserAltAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves a list of advertisers, possibly filtered.</summary>
    [HttpGet("userprofiles/{userprofileId}/advertisers")]
    public async Task<IActionResult> ListAdvertisersAsync([FromQuery] string accountKey, [FromRoute] string userprofileId, [FromQuery] string? sortOrder = null, [FromQuery] string? subaccountId = null, [FromQuery] bool? includeAdvertisersWithoutGroupsOnly = null, [FromQuery] string? searchString = null, [FromQuery] string? sortField = null, [FromQuery] int? maxResults = null, [FromQuery] List<string>? floodlightConfigurationIds = null, [FromQuery] bool? onlyParent = null, [FromQuery] string? status = null, [FromQuery] List<string>? advertiserGroupIds = null, [FromQuery] string? pageToken = null, [FromQuery] List<string>? ids = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListAdvertisersRequest { AccountKey = accountKey, UserprofileId = userprofileId, SortOrder = sortOrder, SubaccountId = subaccountId, IncludeAdvertisersWithoutGroupsOnly = includeAdvertisersWithoutGroupsOnly, SearchString = searchString, SortField = sortField, MaxResults = maxResults, FloodlightConfigurationIds = floodlightConfigurationIds, OnlyParent = onlyParent, Status = status, AdvertiserGroupIds = advertiserGroupIds, PageToken = pageToken, Ids = ids };
        var result = await _advertiserService.ListAdvertisersAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
