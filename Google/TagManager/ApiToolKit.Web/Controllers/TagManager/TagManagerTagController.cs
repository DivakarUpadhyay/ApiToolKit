using ApiToolKit.Application.DTOs.TagManager.Tag;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.TagManager;

/// <summary>
/// Workspace tags.
/// </summary>
[ApiController]
[Route("api/tagmanager/tags")]
[Produces("application/json")]
public class TagManagerTagController : ControllerBase
{
    private readonly ITagManagerTagService _tagService;

    public TagManagerTagController(ITagManagerTagService tagService)
    {
        _tagService = tagService;
    }

    /// <summary>List tags in a workspace.</summary>
    [HttpGet]
    public async Task<IActionResult> ListTagsAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new ListTagsRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId };
        var result = await _tagService.ListTagsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get one tag.</summary>
    [HttpGet("{tagId}")]
    public async Task<IActionResult> GetTagAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromRoute] string tagId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new GetTagRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, TagId = tagId };
        var result = await _tagService.GetTagAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a tag in a workspace.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateTagAsync([FromBody] CreateTagRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _tagService.CreateTagAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a tag (full replace).</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateTagAsync([FromBody] UpdateTagRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _tagService.UpdateTagAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a tag.</summary>
    [HttpDelete("{tagId}")]
    public async Task<IActionResult> DeleteTagAsync([FromQuery] string accountKey, [FromQuery] string accountId, [FromQuery] string containerId, [FromQuery] string workspaceId, [FromRoute] string tagId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        if (string.IsNullOrEmpty(accountId)) return BadRequest("accountId is required.");
        if (string.IsNullOrEmpty(containerId)) return BadRequest("containerId is required.");
        if (string.IsNullOrEmpty(workspaceId)) return BadRequest("workspaceId is required.");
        var request = new DeleteTagRequest { AccountKey = accountKey, AccountId = accountId, ContainerId = containerId, WorkspaceId = workspaceId, TagId = tagId };
        var result = await _tagService.DeleteTagAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Revert workspace changes to a tag.</summary>
    [HttpPost("revert")]
    public async Task<IActionResult> RevertTagAsync([FromBody] RevertTagRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _tagService.RevertTagAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
