using ApiToolKit.Application.DTOs.Identity.People;
using ApiToolKit.Application.Interfaces.Services.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Identity;

[ApiController]
[Route("api/identity/people")]
public class PeopleController : ControllerBase
{
    private readonly IPeopleService _peopleService;

    public PeopleController(IPeopleService peopleService)
    {
        _peopleService = peopleService;
    }

    private string GetAccessToken()
    {
        var authHeader = Request.Headers.Authorization.ToString();
        return authHeader.Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase).Trim();
    }

    /// <summary>Get the authenticated user's own profile.</summary>
    [HttpGet("me")]
    public async Task<IActionResult> GetMe([FromQuery] string personFields = "names,emailAddresses,phoneNumbers,photos,organizations,addresses,urls,biographies")
    {
        var result = await _peopleService.GetMeAsync(new GetMeRequest { PersonFields = personFields }, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List all connections (contacts) for the authenticated user.</summary>
    [HttpGet("connections")]
    public async Task<IActionResult> ListConnections([FromQuery] ListConnectionsRequest request)
    {
        var result = await _peopleService.ListConnectionsAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Search contacts by query string.</summary>
    [HttpGet("search")]
    public async Task<IActionResult> SearchContacts([FromQuery] SearchContactsRequest request)
    {
        var result = await _peopleService.SearchContactsAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new contact.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateContact([FromBody] CreateContactRequest request)
    {
        var result = await _peopleService.CreateContactAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing contact.</summary>
    [HttpPatch]
    public async Task<IActionResult> UpdateContact([FromBody] UpdateContactRequest request)
    {
        var result = await _peopleService.UpdateContactAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a contact by resourceName.</summary>
    [HttpDelete("{resourceName}")]
    public async Task<IActionResult> DeleteContact(string resourceName)
    {
        var result = await _peopleService.DeleteContactAsync(
            new DeleteContactRequest { ResourceName = Uri.UnescapeDataString(resourceName) }, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Batch get multiple contacts by resourceNames.</summary>
    [HttpPost("batch-get")]
    public async Task<IActionResult> BatchGetContacts([FromBody] BatchGetContactsRequest request)
    {
        var result = await _peopleService.BatchGetContactsAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List all contact groups for the authenticated user.</summary>
    [HttpGet("groups")]
    public async Task<IActionResult> ListContactGroups([FromQuery] ListContactGroupsRequest request)
    {
        var result = await _peopleService.ListContactGroupsAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new contact group.</summary>
    [HttpPost("groups")]
    public async Task<IActionResult> CreateContactGroup([FromBody] CreateContactGroupRequest request)
    {
        var result = await _peopleService.CreateContactGroupAsync(request, GetAccessToken());
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
