using ApiToolKit.Application.DTOs.GoogleAds.AdGroup;
using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.GoogleAds;

[ApiController]
[Route("api/googleads/adgroups")]
[Produces("application/json")]
public class GoogleAdsAdGroupController : ControllerBase
{
    private readonly IGoogleAdsAdGroupService _adGroupService;

    public GoogleAdsAdGroupController(IGoogleAdsAdGroupService adGroupService)
    {
        _adGroupService = adGroupService;
    }

    /// <summary>List ad groups for a customer, optionally filtered by campaign</summary>
    [HttpGet]
    public async Task<IActionResult> ListAdGroups([FromQuery] string customerId, [FromQuery] string? campaignResourceName = null, [FromQuery] int pageSize = 50)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest("customerId is required.");
        var request = new ListAdGroupsRequest { CustomerId = customerId, CampaignResourceName = campaignResourceName, PageSize = pageSize };
        var result = await _adGroupService.ListAdGroupsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific ad group by resource name</summary>
    [HttpGet("{customerId}/{*resourceName}")]
    public async Task<IActionResult> GetAdGroup(string customerId, string resourceName)
    {
        var result = await _adGroupService.GetAdGroupAsync(customerId, resourceName);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new ad group</summary>
    [HttpPost]
    public async Task<IActionResult> CreateAdGroup([FromBody] CreateAdGroupRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adGroupService.CreateAdGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing ad group</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateAdGroup([FromBody] UpdateAdGroupRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adGroupService.UpdateAdGroupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Remove (delete) an ad group</summary>
    [HttpDelete("{customerId}/{*resourceName}")]
    public async Task<IActionResult> RemoveAdGroup(string customerId, string resourceName)
    {
        var result = await _adGroupService.RemoveAdGroupAsync(customerId, resourceName);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
