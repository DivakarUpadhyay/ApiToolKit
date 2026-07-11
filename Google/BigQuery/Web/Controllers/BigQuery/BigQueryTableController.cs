using ApiToolKit.Application.DTOs.BigQuery.Table;
using ApiToolKit.Application.Interfaces.Services.BigQuery;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.BigQuery;

[ApiController]
[Route("api/bigquery/tables")]
public class BigQueryTableController : ControllerBase
{
    private readonly IBigQueryTableService _tableService;

    public BigQueryTableController(IBigQueryTableService tableService)
    {
        _tableService = tableService;
    }

    private string? GetBearerToken()
    {
        var authHeader = Request.Headers.Authorization.ToString();
        if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            return authHeader["Bearer ".Length..].Trim();
        return null;
    }

    /// <summary>List all tables in a dataset.</summary>
    [HttpGet]
    public async Task<IActionResult> ListTables([FromQuery] ListTablesRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var token = GetBearerToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized("Bearer token required.");
        var result = await _tableService.ListTablesAsync(request, token);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific table.</summary>
    [HttpGet("{projectId}/{datasetId}/{tableId}")]
    public async Task<IActionResult> GetTable(string projectId, string datasetId, string tableId)
    {
        var token = GetBearerToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized("Bearer token required.");
        var result = await _tableService.GetTableAsync(projectId, datasetId, tableId, token);
        return result.Success ? Ok(result) : NotFound(result);
    }

    /// <summary>Create a new table.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateTable([FromBody] CreateTableRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var token = GetBearerToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized("Bearer token required.");
        var result = await _tableService.CreateTableAsync(request, token);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing table.</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateTable([FromBody] UpdateTableRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var token = GetBearerToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized("Bearer token required.");
        var result = await _tableService.UpdateTableAsync(request, token);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a table.</summary>
    [HttpDelete]
    public async Task<IActionResult> DeleteTable([FromBody] DeleteTableRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var token = GetBearerToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized("Bearer token required.");
        var result = await _tableService.DeleteTableAsync(request, token);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Copy a table to another dataset or project.</summary>
    [HttpPost("copy")]
    public async Task<IActionResult> CopyTable([FromBody] CopyTableRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var token = GetBearerToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized("Bearer token required.");
        var result = await _tableService.CopyTableAsync(request, token);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
