using ApiToolKit.Application.DTOs.TagManager.Template;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.TagManager;

/// <summary>
/// Workspace templates.
/// </summary>
[ApiController]
[Route("api/tagmanager/templates")]
[Produces("application/json")]
public class TagManagerTemplateController : ControllerBase
{
    private readonly ITagManagerTemplateService _templateService;

    public TagManagerTemplateController(ITagManagerTemplateService templateService)
    {
        _templateService = templateService;
    }

    /// <summary>List templates in a workspace.</summary>
    [HttpGet]
    public async Task<IActionResult> ListTemplatesAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new ListTemplatesRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, PageToken = pageToken };
        var result = await _templateService.ListTemplatesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get one template.</summary>
    [HttpGet("{templateId}")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromRoute] string templateId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new GetTemplateRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, TemplateId = templateId };
        var result = await _templateService.GetTemplateAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a template.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateTemplateAsync([FromBody] CreateTemplateRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _templateService.CreateTemplateAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a template.</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateTemplateAsync([FromBody] UpdateTemplateRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _templateService.UpdateTemplateAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a template.</summary>
    [HttpDelete("{templateId}")]
    public async Task<IActionResult> DeleteTemplateAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromRoute] string templateId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new DeleteTemplateRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, TemplateId = templateId };
        var result = await _templateService.DeleteTemplateAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Revert workspace changes to a template.</summary>
    [HttpPost("revert")]
    public async Task<IActionResult> RevertTemplateAsync([FromBody] RevertTemplateRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _templateService.RevertTemplateAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Import a custom template from the Community Template Gallery.</summary>
    [HttpPost("import-from-gallery")]
    public async Task<IActionResult> ImportTemplateFromGalleryAsync([FromBody] ImportTemplateFromGalleryRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _templateService.ImportTemplateFromGalleryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
