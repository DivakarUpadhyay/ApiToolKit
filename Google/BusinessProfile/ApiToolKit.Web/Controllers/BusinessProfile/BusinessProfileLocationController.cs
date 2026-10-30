using ApiToolKit.Application.DTOs.BusinessProfile.Location;
using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.BusinessProfile;

/// <summary>
/// Business locations (Business Information API).
/// </summary>
[ApiController]
[Route("api/businessprofile/locations")]
[Produces("application/json")]
public class BusinessProfileLocationController : ControllerBase
{
    private readonly IBusinessProfileLocationService _locationService;

    public BusinessProfileLocationController(IBusinessProfileLocationService locationService)
    {
        _locationService = locationService;
    }

    /// <summary>List locations under an account.</summary>
    [HttpGet]
    public async Task<IActionResult> ListLocationsAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string? readMask = null, [FromQuery] string? filter = null, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        var request = new ListLocationsRequest { AccountKey = accountKey, AccountId = accountId, ReadMask = readMask, Filter = filter, PageSize = pageSize, PageToken = pageToken };
        var result = await _locationService.ListLocationsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get one location.</summary>
    [HttpGet("{locationId}")]
    public async Task<IActionResult> GetLocationAsync([FromQuery] string accountKey, [FromRoute] string locationId, [FromQuery] string? readMask = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetLocationRequest { AccountKey = accountKey, LocationId = locationId, ReadMask = readMask };
        var result = await _locationService.GetLocationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new business location.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateLocationAsync([FromBody] CreateLocationRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _locationService.CreateLocationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update location fields listed in UpdateMask.</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateLocationAsync([FromBody] UpdateLocationRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _locationService.UpdateLocationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a location.</summary>
    [HttpDelete("{locationId}")]
    public async Task<IActionResult> DeleteLocationAsync([FromQuery] string accountKey, [FromRoute] string locationId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteLocationRequest { AccountKey = accountKey, LocationId = locationId };
        var result = await _locationService.DeleteLocationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get attributes (amenities, services) of a location.</summary>
    [HttpGet("{locationId}/attributes")]
    public async Task<IActionResult> GetLocationAttributesAsync([FromQuery] string accountKey, [FromRoute] string locationId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetLocationAttributesRequest { AccountKey = accountKey, LocationId = locationId };
        var result = await _locationService.GetLocationAttributesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get the Google-updated version of a location (suggested edits).</summary>
    [HttpGet("{locationId}/google-updated")]
    public async Task<IActionResult> GetGoogleUpdatedLocationAsync([FromQuery] string accountKey, [FromRoute] string locationId, [FromQuery] string? readMask = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetGoogleUpdatedLocationRequest { AccountKey = accountKey, LocationId = locationId, ReadMask = readMask };
        var result = await _locationService.GetGoogleUpdatedLocationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update attributes of a location.</summary>
    [HttpPut("attributes")]
    public async Task<IActionResult> UpdateLocationAttributesAsync([FromBody] UpdateLocationAttributesRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _locationService.UpdateLocationAttributesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get Google-updated attributes of a location.</summary>
    [HttpGet("{locationId}/attributes/google-updated")]
    public async Task<IActionResult> GetGoogleUpdatedAttributesAsync([FromQuery] string accountKey, [FromRoute] string locationId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetGoogleUpdatedAttributesRequest { AccountKey = accountKey, LocationId = locationId };
        var result = await _locationService.GetGoogleUpdatedAttributesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Move a location to another account.</summary>
    [HttpPost("transfer")]
    public async Task<IActionResult> TransferLocationAsync([FromBody] TransferLocationRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _locationService.TransferLocationAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List admins of a location.</summary>
    [HttpGet("{locationId}/admins")]
    public async Task<IActionResult> ListLocationAdminsAsync([FromQuery] string accountKey, [FromRoute] string locationId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListLocationAdminsRequest { AccountKey = accountKey, LocationId = locationId };
        var result = await _locationService.ListLocationAdminsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Invite a user to manage a location.</summary>
    [HttpPost("admins")]
    public async Task<IActionResult> InviteLocationAdminAsync([FromBody] InviteLocationAdminRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _locationService.InviteLocationAdminAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Change the role of a location admin.</summary>
    [HttpPut("admins")]
    public async Task<IActionResult> UpdateLocationAdminAsync([FromBody] UpdateLocationAdminRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _locationService.UpdateLocationAdminAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Remove an admin from a location.</summary>
    [HttpDelete("{locationId}/admins/{adminId}")]
    public async Task<IActionResult> RemoveLocationAdminAsync([FromQuery] string accountKey, [FromRoute] string locationId, [FromRoute] string adminId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new RemoveLocationAdminRequest { AccountKey = accountKey, LocationId = locationId, AdminId = adminId };
        var result = await _locationService.RemoveLocationAdminAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Search Google Maps for existing locations that match a business.</summary>
    [HttpPost("google-locations/search")]
    public async Task<IActionResult> SearchGoogleLocationsAsync([FromBody] SearchGoogleLocationsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _locationService.SearchGoogleLocationsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get follower count of a location.</summary>
    [HttpGet("{locationId}/followers")]
    public async Task<IActionResult> GetFollowersMetadataAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromRoute] string locationId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        var request = new GetFollowersMetadataRequest { AccountKey = accountKey, AccountId = accountId, LocationId = locationId };
        var result = await _locationService.GetFollowersMetadataAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get food menus of a location.</summary>
    [HttpGet("{locationId}/food-menus")]
    public async Task<IActionResult> GetFoodMenusAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromRoute] string locationId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        var request = new GetFoodMenusRequest { AccountKey = accountKey, AccountId = accountId, LocationId = locationId };
        var result = await _locationService.GetFoodMenusAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Replace food menus of a location.</summary>
    [HttpPut("food-menus")]
    public async Task<IActionResult> UpdateFoodMenusAsync([FromBody] UpdateFoodMenusRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _locationService.UpdateFoodMenusAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get the service list of a location.</summary>
    [HttpGet("{locationId}/service-list")]
    public async Task<IActionResult> GetServiceListAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromRoute] string locationId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        var request = new GetServiceListRequest { AccountKey = accountKey, AccountId = accountId, LocationId = locationId };
        var result = await _locationService.GetServiceListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Replace the service list of a location.</summary>
    [HttpPut("service-list")]
    public async Task<IActionResult> UpdateServiceListAsync([FromBody] UpdateServiceListRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _locationService.UpdateServiceListAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get health provider attributes of a location.</summary>
    [HttpGet("{locationId}/health-provider-attributes")]
    public async Task<IActionResult> GetHealthProviderAttributesAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromRoute] string locationId, [FromQuery] string? languageCode = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        var request = new GetHealthProviderAttributesRequest { AccountKey = accountKey, AccountId = accountId, LocationId = locationId, LanguageCode = languageCode };
        var result = await _locationService.GetHealthProviderAttributesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update health provider attributes of a location.</summary>
    [HttpPut("health-provider-attributes")]
    public async Task<IActionResult> UpdateHealthProviderAttributesAsync([FromBody] UpdateHealthProviderAttributesRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _locationService.UpdateHealthProviderAttributesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List insurance networks Google supports for a location.</summary>
    [HttpGet("{locationId}/insurance-networks")]
    public async Task<IActionResult> ListInsuranceNetworksAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromRoute] string locationId, [FromQuery] string? languageCode = null, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        var request = new ListInsuranceNetworksRequest { AccountKey = accountKey, AccountId = accountId, LocationId = locationId, LanguageCode = languageCode, PageSize = pageSize, PageToken = pageToken };
        var result = await _locationService.ListInsuranceNetworksAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List Google locations recommended for the account to claim.</summary>
    [HttpGet("recommended")]
    public async Task<IActionResult> ListRecommendedGoogleLocationsAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        var request = new ListRecommendedGoogleLocationsRequest { AccountKey = accountKey, AccountId = accountId, PageSize = pageSize, PageToken = pageToken };
        var result = await _locationService.ListRecommendedGoogleLocationsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
