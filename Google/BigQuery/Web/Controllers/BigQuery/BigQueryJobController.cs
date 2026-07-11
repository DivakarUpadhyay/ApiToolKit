using ApiToolKit.Application.DTOs.BigQuery.Job;
using ApiToolKit.Application.Interfaces.Services.BigQuery;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.BigQuery;

[ApiController]
[Route("api/bigquery/jobs")]
public class BigQueryJobController : ControllerBase
{
    private readonly IBigQueryJobService _jobService;

    public BigQueryJobController(IBigQueryJobService jobService)
    {
        _jobService = jobService;
    }

    private string? GetBearerToken()
    {
        var authHeader = Request.Headers.Authorization.ToString();
        if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            return authHeader["Bearer ".Length..].Trim();
        return null;
    }

    /// <summary>Run a synchronous query against BigQuery.</summary>
    [HttpPost("query")]
    public async Task<IActionResult> Query([FromBody] QueryRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _jobService.QueryAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get query results for a completed job.</summary>
    [HttpGet("results")]
    public async Task<IActionResult> GetQueryResults([FromQuery] string projectId, [FromQuery] string jobId)
    {
        var token = GetBearerToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized("Bearer token required.");
        var result = await _jobService.GetQueryResultsAsync(projectId, jobId, token);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Stream rows into a table using the insertAll API.</summary>
    [HttpPost("insert-all")]
    public async Task<IActionResult> InsertAll([FromBody] InsertAllRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _jobService.InsertAllAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>List rows from a table.</summary>
    [HttpGet("rows")]
    public async Task<IActionResult> ListRows([FromQuery] ListRowsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _jobService.ListRowsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new asynchronous job (query, load, or extract).</summary>
    [HttpPost("create")]
    public async Task<IActionResult> CreateJob([FromBody] CreateJobRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var token = GetBearerToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized("Bearer token required.");
        var result = await _jobService.CreateJobAsync(request, token);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get the status and details of a job.</summary>
    [HttpGet("{projectId}/{jobId}")]
    public async Task<IActionResult> GetJob(string projectId, string jobId, [FromQuery] string? location = null)
    {
        var token = GetBearerToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized("Bearer token required.");
        var request = new GetJobRequest { ProjectId = projectId, JobId = jobId, Location = location };
        var result = await _jobService.GetJobAsync(request, token);
        return result.Success ? Ok(result) : NotFound(result);
    }

    /// <summary>List jobs in a project.</summary>
    [HttpGet("list")]
    public async Task<IActionResult> ListJobs([FromQuery] ListJobsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var token = GetBearerToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized("Bearer token required.");
        var result = await _jobService.ListJobsAsync(request, token);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Cancel a running job.</summary>
    [HttpDelete("{projectId}/{jobId}")]
    public async Task<IActionResult> CancelJob(string projectId, string jobId, [FromQuery] string? location = null)
    {
        var token = GetBearerToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized("Bearer token required.");
        var request = new CancelJobRequest { ProjectId = projectId, JobId = jobId, Location = location };
        var result = await _jobService.CancelJobAsync(request, token);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
