using ApiToolKit.Application.DTOs.TagManager.Client;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.TagManager;

/// <summary>
/// Workspace clients.
/// </summary>
[ApiController]
[Route("api/tagmanager/clients")]
[Produces("application/json")]
public class TagManagerClientController : ControllerBase
{
    private readonly ITagManagerClientService _clientService;

    public TagManagerClientController(ITagManagerClientService clientService)
    {
        _clientService = clientService;
    }

    /// <summary>List clients in a workspace.</summary>
    [HttpGet]
    public async Task<IActionResult> ListClientsAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new ListClientsRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, PageToken = pageToken };
        var result = await _clientService.ListClientsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get one client.</summary>
    [HttpGet("{clientId}")]
    public async Task<IActionResult> GetClientAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromRoute] string clientId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new GetClientRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, ClientId = clientId };
        var result = await _clientService.GetClientAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a client.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateClientAsync([FromBody] CreateClientRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _clientService.CreateClientAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a client.</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateClientAsync([FromBody] UpdateClientRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _clientService.UpdateClientAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a client.</summary>
    [HttpDelete("{clientId}")]
    public async Task<IActionResult> DeleteClientAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromRoute] string clientId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new DeleteClientRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, ClientId = clientId };
        var result = await _clientService.DeleteClientAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Revert workspace changes to a client.</summary>
    [HttpPost("revert")]
    public async Task<IActionResult> RevertClientAsync([FromBody] RevertClientRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _clientService.RevertClientAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
