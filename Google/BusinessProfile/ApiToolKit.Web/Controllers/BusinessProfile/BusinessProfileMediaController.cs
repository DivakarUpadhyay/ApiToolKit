using ApiToolKit.Application.DTOs.BusinessProfile.Media;
using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.BusinessProfile;

/// <summary>
/// Photos and videos on a location (owner and customer media).
/// </summary>
[ApiController]
[Route("api/businessprofile/media")]
[Produces("application/json")]
public class BusinessProfileMediaController : ControllerBase
{
    private readonly IBusinessProfileMediaService _mediaService;

    public BusinessProfileMediaController(IBusinessProfileMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    /// <summary>List owner media of a location.</summary>
    [HttpGet]
    public async Task<IActionResult> ListMediaAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string locationId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(locationId)) return BadRequest("locationId is required.");
        var request = new ListMediaRequest { AccountKey = accountKey, AccountId = accountId, LocationId = locationId, PageSize = pageSize, PageToken = pageToken };
        var result = await _mediaService.ListMediaAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get one media item.</summary>
    [HttpGet("{mediaId}")]
    public async Task<IActionResult> GetMediaAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string locationId, [FromRoute] string mediaId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(locationId)) return BadRequest("locationId is required.");
        var request = new GetMediaRequest { AccountKey = accountKey, AccountId = accountId, LocationId = locationId, MediaId = mediaId };
        var result = await _mediaService.GetMediaAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Add a photo or video to a location from a public URL.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateMediaAsync([FromBody] CreateMediaRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _mediaService.CreateMediaAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update media metadata (fields in UpdateMask).</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateMediaAsync([FromBody] UpdateMediaRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _mediaService.UpdateMediaAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a media item.</summary>
    [HttpDelete("{mediaId}")]
    public async Task<IActionResult> DeleteMediaAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string locationId, [FromRoute] string mediaId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(locationId)) return BadRequest("locationId is required.");
        var request = new DeleteMediaRequest { AccountKey = accountKey, AccountId = accountId, LocationId = locationId, MediaId = mediaId };
        var result = await _mediaService.DeleteMediaAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Start a byte upload; returns a resource name to upload bytes to.</summary>
    [HttpPost("start-upload")]
    public async Task<IActionResult> StartMediaUploadAsync([FromBody] StartMediaUploadRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _mediaService.StartMediaUploadAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List media uploaded by customers.</summary>
    [HttpGet("customers")]
    public async Task<IActionResult> ListCustomerMediaAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string locationId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(locationId)) return BadRequest("locationId is required.");
        var request = new ListCustomerMediaRequest { AccountKey = accountKey, AccountId = accountId, LocationId = locationId, PageSize = pageSize, PageToken = pageToken };
        var result = await _mediaService.ListCustomerMediaAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get one customer media item.</summary>
    [HttpGet("customers/{mediaId}")]
    public async Task<IActionResult> GetCustomerMediaAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string locationId, [FromRoute] string mediaId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(locationId)) return BadRequest("locationId is required.");
        var request = new GetCustomerMediaRequest { AccountKey = accountKey, AccountId = accountId, LocationId = locationId, MediaId = mediaId };
        var result = await _mediaService.GetCustomerMediaAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
