using ApiToolKit.Application.DTOs.Merchant.Relationship;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// relationships (v1)
/// </summary>
[ApiController]
[Route("api/merchant/accounts")]
[Produces("application/json")]
public class MerchantRelationshipController : ControllerBase
{
    private readonly IMerchantRelationshipService _relationshipService;

    public MerchantRelationshipController(IMerchantRelationshipService relationshipService)
    {
        _relationshipService = relationshipService;
    }

    /// <summary>List account relationships for the specified account.</summary>
    [HttpGet("accounts/{accountId}/relationships")]
    public async Task<IActionResult> ListRelationshipsAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListRelationshipsRequest { AccountKey = accountKey, AccountId = accountId, PageSize = pageSize, PageToken = pageToken };
        var result = await _relationshipService.ListRelationshipsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates the account relationship.</summary>
    [HttpPatch("accounts/{accountId}/relationships/{relationshipId}")]
    public async Task<IActionResult> UpdateRelationshipAsync([FromRoute] string accountId, [FromRoute] string relationshipId, [FromBody] UpdateRelationshipRequest request)
    {
        request.AccountId = accountId;
        request.RelationshipId = relationshipId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _relationshipService.UpdateRelationshipAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieve an account relationship.</summary>
    [HttpGet("accounts/{accountId}/relationships/{relationshipId}")]
    public async Task<IActionResult> GetRelationshipAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string relationshipId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetRelationshipRequest { AccountKey = accountKey, AccountId = accountId, RelationshipId = relationshipId };
        var result = await _relationshipService.GetRelationshipAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
