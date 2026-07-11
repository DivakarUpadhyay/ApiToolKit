using ApiToolKit.Application.DTOs.BigQuery.Dataset;
using ApiToolKit.Application.Interfaces.Services.BigQuery;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.BigQuery;

[ApiController]
[Route("api/bigquery/datasets")]
public class BigQueryDatasetController : ControllerBase
{
    private readonly IBigQueryDatasetService _datasetService;

    public BigQueryDatasetController(IBigQueryDatasetService datasetService)
    {
        _datasetService = datasetService;
    }

    private string? GetBearerToken()
    {
        var authHeader = Request.Headers.Authorization.ToString();
        if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            return authHeader["Bearer ".Length..].Trim();
        return null;
    }

    /// <summary>List all datasets in a project.</summary>
    [HttpGet]
    public async Task<IActionResult> ListDatasets([FromQuery] ListDatasetsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var token = GetBearerToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized("Bearer token required.");
        var result = await _datasetService.ListDatasetsAsync(request, token);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific dataset.</summary>
    [HttpGet("{projectId}/{datasetId}")]
    public async Task<IActionResult> GetDataset(string projectId, string datasetId)
    {
        var token = GetBearerToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized("Bearer token required.");
        var result = await _datasetService.GetDatasetAsync(projectId, datasetId, token);
        return result.Success ? Ok(result) : NotFound(result);
    }

    /// <summary>Create a new dataset.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateDataset([FromBody] CreateDatasetRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var token = GetBearerToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized("Bearer token required.");
        var result = await _datasetService.CreateDatasetAsync(request, token);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing dataset.</summary>
    [HttpPut]
    public async Task<IActionResult> UpdateDataset([FromBody] UpdateDatasetRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var token = GetBearerToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized("Bearer token required.");
        var result = await _datasetService.UpdateDatasetAsync(request, token);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a dataset.</summary>
    [HttpDelete("{projectId}/{datasetId}")]
    public async Task<IActionResult> DeleteDataset(string projectId, string datasetId, [FromQuery] bool deleteContents = false)
    {
        var token = GetBearerToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized("Bearer token required.");
        var request = new DeleteDatasetRequest
        {
            ProjectId = projectId,
            DatasetId = datasetId,
            DeleteContents = deleteContents
        };
        var result = await _datasetService.DeleteDatasetAsync(request, token);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
