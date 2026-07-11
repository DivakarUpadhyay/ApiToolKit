using ApiToolKit.Application.DTOs.Workspace.Docs;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Docs;

/// <summary>Google Docs API</summary>
[ApiController]
[Route("api/workspace/docs")]
public class DocsController : ControllerBase
{
    private readonly IDocsService _service;

    public DocsController(IDocsService service)
    {
        _service = service;
    }

    /// <summary>Create a new document</summary>
    [HttpPost("{userId}")]
    public async Task<IActionResult> CreateDocument(string userId, [FromBody] CreateDocumentRequest request)
    {
        var result = await _service.CreateDocumentAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a document</summary>
    [HttpGet("{userId}/{documentId}")]
    public async Task<IActionResult> GetDocument(string userId, string documentId)
    {
        var result = await _service.GetDocumentAsync(userId, new GetDocumentRequest { DocumentId = documentId });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Insert text into a document</summary>
    [HttpPost("{userId}/insertText")]
    public async Task<IActionResult> InsertText(string userId, [FromBody] InsertTextRequest request)
    {
        var result = await _service.InsertTextAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Replace text in a document</summary>
    [HttpPost("{userId}/replaceText")]
    public async Task<IActionResult> ReplaceText(string userId, [FromBody] ReplaceTextRequest request)
    {
        var result = await _service.ReplaceTextAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Insert a table into a document</summary>
    [HttpPost("{userId}/insertTable")]
    public async Task<IActionResult> InsertTable(string userId, [FromBody] InsertTableRequest request)
    {
        var result = await _service.InsertTableAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete content range in a document</summary>
    [HttpPost("{userId}/deleteRange")]
    public async Task<IActionResult> DeleteContentRange(string userId, [FromBody] DeleteContentRangeRequest request)
    {
        var result = await _service.DeleteContentRangeAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Insert a page break</summary>
    [HttpPost("{userId}/insertPageBreak")]
    public async Task<IActionResult> InsertPageBreak(string userId, [FromBody] InsertPageBreakRequest request)
    {
        var result = await _service.InsertPageBreakAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Apply paragraph style (heading, normal text)</summary>
    [HttpPost("{userId}/paragraphStyle")]
    public async Task<IActionResult> ApplyParagraphStyle(string userId, [FromBody] ParagraphStyleRequest request)
    {
        var result = await _service.ApplyParagraphStyleAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
