using ApiToolKit.Application.DTOs.Workspace.Forms;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Forms;

/// <summary>Google Forms API</summary>
[ApiController]
[Route("api/workspace/forms")]
public class FormsController : ControllerBase
{
    private readonly IFormsService _service;

    public FormsController(IFormsService service)
    {
        _service = service;
    }

    /// <summary>Create a new form</summary>
    [HttpPost("{userId}")]
    public async Task<IActionResult> CreateForm(string userId, [FromBody] CreateFormRequest request)
    {
        var result = await _service.CreateFormAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a form</summary>
    [HttpGet("{userId}/{formId}")]
    public async Task<IActionResult> GetForm(string userId, string formId)
    {
        var result = await _service.GetFormAsync(userId, new GetFormRequest { FormId = formId });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update form info and settings</summary>
    [HttpPatch("{userId}")]
    public async Task<IActionResult> UpdateForm(string userId, [FromBody] UpdateFormRequest request)
    {
        var result = await _service.UpdateFormAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Add an item/question to a form</summary>
    [HttpPost("{userId}/items")]
    public async Task<IActionResult> AddItem(string userId, [FromBody] AddItemRequest2 request)
    {
        var result = await _service.AddItemAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete an item from a form</summary>
    [HttpDelete("{userId}/items")]
    public async Task<IActionResult> DeleteItem(string userId, [FromBody] DeleteItemRequest2 request)
    {
        var result = await _service.DeleteItemAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
