using ApiToolKit.Application.DTOs.Workspace.Gmail;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Gmail;

/// <summary>Gmail Settings API</summary>
[ApiController]
[Route("api/workspace/gmail/settings")]
public class GmailSettingsController : ControllerBase
{
    private readonly IGmailSettingsService _service;

    public GmailSettingsController(IGmailSettingsService service)
    {
        _service = service;
    }

    /// <summary>Get vacation auto-reply settings</summary>
    [HttpGet("{userId}/vacation")]
    public async Task<IActionResult> GetVacationSettings(string userId)
    {
        var result = await _service.GetVacationSettingsAsync(userId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update vacation auto-reply settings</summary>
    [HttpPut("{userId}/vacation")]
    public async Task<IActionResult> UpdateVacationSettings(string userId, [FromBody] UpdateVacationRequest request)
    {
        var result = await _service.UpdateVacationSettingsAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List filters</summary>
    [HttpGet("{userId}/filters")]
    public async Task<IActionResult> ListFilters(string userId)
    {
        var result = await _service.ListFiltersAsync(userId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a filter</summary>
    [HttpPost("{userId}/filters")]
    public async Task<IActionResult> CreateFilter(string userId, [FromBody] CreateFilterRequest request)
    {
        var result = await _service.CreateFilterAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a filter</summary>
    [HttpDelete("{userId}/filters/{filterId}")]
    public async Task<IActionResult> DeleteFilter(string userId, string filterId)
    {
        var result = await _service.DeleteFilterAsync(userId, filterId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List forwarding addresses</summary>
    [HttpGet("{userId}/forwarding")]
    public async Task<IActionResult> ListForwardingAddresses(string userId)
    {
        var result = await _service.ListForwardingAddressesAsync(userId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a forwarding address</summary>
    [HttpPost("{userId}/forwarding")]
    public async Task<IActionResult> CreateForwardingAddress(string userId, [FromBody] CreateForwardingRequest request)
    {
        var result = await _service.CreateForwardingAddressAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get IMAP settings</summary>
    [HttpGet("{userId}/imap")]
    public async Task<IActionResult> GetImapSettings(string userId)
    {
        var result = await _service.GetImapSettingsAsync(userId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update IMAP settings</summary>
    [HttpPut("{userId}/imap")]
    public async Task<IActionResult> UpdateImapSettings(string userId, [FromBody] ImapSettings settings)
    {
        var result = await _service.UpdateImapSettingsAsync(userId, settings);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get POP settings</summary>
    [HttpGet("{userId}/pop")]
    public async Task<IActionResult> GetPopSettings(string userId)
    {
        var result = await _service.GetPopSettingsAsync(userId);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
