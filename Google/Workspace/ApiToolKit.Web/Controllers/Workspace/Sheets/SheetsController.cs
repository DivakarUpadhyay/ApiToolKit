using ApiToolKit.Application.DTOs.Workspace.Sheets;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Sheets;

/// <summary>Google Sheets API</summary>
[ApiController]
[Route("api/workspace/sheets")]
public class SheetsController : ControllerBase
{
    private readonly ISheetsService _service;

    public SheetsController(ISheetsService service)
    {
        _service = service;
    }

    /// <summary>Create a new spreadsheet</summary>
    [HttpPost("{userId}")]
    public async Task<IActionResult> CreateSpreadsheet(string userId, [FromBody] CreateSpreadsheetRequest request)
    {
        var result = await _service.CreateSpreadsheetAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a spreadsheet</summary>
    [HttpGet("{userId}/{spreadsheetId}")]
    public async Task<IActionResult> GetSpreadsheet(string userId, string spreadsheetId, [FromQuery] bool includeGridData = false)
    {
        var result = await _service.GetSpreadsheetAsync(userId, new GetSpreadsheetRequest { SpreadsheetId = spreadsheetId, IncludeGridData = includeGridData });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get values from a range</summary>
    [HttpGet("{userId}/{spreadsheetId}/values")]
    public async Task<IActionResult> GetValues(string userId, string spreadsheetId, [FromQuery] string range)
    {
        var result = await _service.GetValuesAsync(userId, new GetValuesRequest { SpreadsheetId = spreadsheetId, Range = range });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update values in a range</summary>
    [HttpPut("{userId}/values")]
    public async Task<IActionResult> UpdateValues(string userId, [FromBody] UpdateValuesRequest request)
    {
        var result = await _service.UpdateValuesAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Append values to a range</summary>
    [HttpPost("{userId}/values/append")]
    public async Task<IActionResult> AppendValues(string userId, [FromBody] AppendValuesRequest request)
    {
        var result = await _service.AppendValuesAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Clear values from a range</summary>
    [HttpPost("{userId}/values/clear")]
    public async Task<IActionResult> ClearValues(string userId, [FromBody] ClearValuesRequest request)
    {
        var result = await _service.ClearValuesAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Batch get values from multiple ranges</summary>
    [HttpPost("{userId}/values/batchGet")]
    public async Task<IActionResult> BatchGetValues(string userId, [FromBody] BatchGetValuesRequest request)
    {
        var result = await _service.BatchGetValuesAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Batch update values in multiple ranges</summary>
    [HttpPost("{userId}/values/batchUpdate")]
    public async Task<IActionResult> BatchUpdateValues(string userId, [FromBody] BatchUpdateValuesRequest request)
    {
        var result = await _service.BatchUpdateValuesAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Add a new sheet tab</summary>
    [HttpPost("{userId}/addSheet")]
    public async Task<IActionResult> AddSheet(string userId, [FromBody] AddSheetRequest request)
    {
        var result = await _service.AddSheetAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a sheet tab</summary>
    [HttpPost("{userId}/deleteSheet")]
    public async Task<IActionResult> DeleteSheet(string userId, [FromBody] DeleteSheetRequest request)
    {
        var result = await _service.DeleteSheetAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Rename a sheet tab</summary>
    [HttpPost("{userId}/renameSheet")]
    public async Task<IActionResult> RenameSheet(string userId, [FromBody] RenameSheetRequest request)
    {
        var result = await _service.RenameSheetAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Copy a sheet to another spreadsheet</summary>
    [HttpPost("{userId}/copyTo")]
    public async Task<IActionResult> CopySheetTo(string userId, [FromBody] CopyToRequest request)
    {
        var result = await _service.CopySheetToAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
