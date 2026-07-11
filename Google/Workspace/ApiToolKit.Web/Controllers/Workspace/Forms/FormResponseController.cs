using ApiToolKit.Application.DTOs.Workspace.Forms;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Forms;

/// <summary>Google Forms Responses API</summary>
[ApiController]
[Route("api/workspace/forms/responses")]
public class FormResponseController : ControllerBase
{
    private readonly IFormResponseService _service;

    public FormResponseController(IFormResponseService service)
    {
        _service = service;
    }

    /// <summary>List responses for a form</summary>
    [HttpGet("{userId}")]
    public async Task<IActionResult> ListResponses(string userId, [FromQuery] ListFormResponsesRequest request)
    {
        var result = await _service.ListResponsesAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific form response</summary>
    [HttpGet("{userId}/{formId}/{responseId}")]
    public async Task<IActionResult> GetResponse(string userId, string formId, string responseId)
    {
        var result = await _service.GetResponseAsync(userId, formId, responseId);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
