using ApiToolKit.Application.DTOs.BusinessProfile.PlaceAction;
using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.BusinessProfile;

/// <summary>
/// Place action links (book, order, reserve buttons) via the Place Actions API.
/// </summary>
[ApiController]
[Route("api/businessprofile/place-actions")]
[Produces("application/json")]
public class BusinessProfilePlaceActionController : ControllerBase
{
    private readonly IBusinessProfilePlaceActionService _placeActionService;

    public BusinessProfilePlaceActionController(IBusinessProfilePlaceActionService placeActionService)
    {
        _placeActionService = placeActionService;
    }

    /// <summary>List place action links of a location.</summary>
    [HttpGet]
    public async Task<IActionResult> ListPlaceActionLinksAsync([FromQuery] string accountKey, [FromQuery] string locationId, [FromQuery] string? filter = null, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(locationId)) return BadRequest("locationId is required.");
        var request = new ListPlaceActionLinksRequest { AccountKey = accountKey, LocationId = locationId, Filter = filter, PageSize = pageSize, PageToken = pageToken };
        var result = await _placeActionService.ListPlaceActionLinksAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get one place action link.</summary>
    [HttpGet("{linkId}")]
    public async Task<IActionResult> GetPlaceActionLinkAsync([FromQuery] string accountKey, [FromQuery] string locationId, [FromRoute] string linkId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(locationId)) return BadRequest("locationId is required.");
        var request = new GetPlaceActionLinkRequest { AccountKey = accountKey, LocationId = locationId, LinkId = linkId };
        var result = await _placeActionService.GetPlaceActionLinkAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a place action link.</summary>
    [HttpPost]
    public async Task<IActionResult> CreatePlaceActionLinkAsync([FromBody] CreatePlaceActionLinkRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _placeActionService.CreatePlaceActionLinkAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a place action link (fields in UpdateMask).</summary>
    [HttpPut]
    public async Task<IActionResult> UpdatePlaceActionLinkAsync([FromBody] UpdatePlaceActionLinkRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _placeActionService.UpdatePlaceActionLinkAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a place action link.</summary>
    [HttpDelete("{linkId}")]
    public async Task<IActionResult> DeletePlaceActionLinkAsync([FromQuery] string accountKey, [FromQuery] string locationId, [FromRoute] string linkId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(locationId)) return BadRequest("locationId is required.");
        var request = new DeletePlaceActionLinkRequest { AccountKey = accountKey, LocationId = locationId, LinkId = linkId };
        var result = await _placeActionService.DeletePlaceActionLinkAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List place action types available for a location or region.</summary>
    [HttpGet("types")]
    public async Task<IActionResult> ListPlaceActionTypesAsync([FromQuery] string accountKey, [FromQuery] string? languageCode = null, [FromQuery] string? filter = null, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListPlaceActionTypesRequest { AccountKey = accountKey, LanguageCode = languageCode, Filter = filter, PageSize = pageSize, PageToken = pageToken };
        var result = await _placeActionService.ListPlaceActionTypesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
