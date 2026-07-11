using ApiToolKit.Application.DTOs.BigQuery.Routine;
using ApiToolKit.Application.Interfaces.Services.BigQuery;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.BigQuery;

[ApiController]
[Route("api/bigquery/routines")]
public class BigQueryRoutineController : ControllerBase
{
    private readonly IBigQueryRoutineService _routineService;

    public BigQueryRoutineController(IBigQueryRoutineService routineService)
    {
        _routineService = routineService;
    }

    private string? GetBearerToken()
    {
        var authHeader = Request.Headers.Authorization.ToString();
        if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            return authHeader["Bearer ".Length..].Trim();
        return null;
    }

    /// <summary>List routines in a dataset.</summary>
    [HttpGet]
    public async Task<IActionResult> ListRoutines([FromQuery] ListRoutinesRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var token = GetBearerToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized("Bearer token required.");
        var result = await _routineService.ListRoutinesAsync(request, token);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new routine (UDF, stored procedure, or table function).</summary>
    [HttpPost]
    public async Task<IActionResult> CreateRoutine([FromBody] CreateRoutineRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var token = GetBearerToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized("Bearer token required.");
        var result = await _routineService.CreateRoutineAsync(request, token);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific routine.</summary>
    [HttpGet("{projectId}/{datasetId}/{routineId}")]
    public async Task<IActionResult> GetRoutine(string projectId, string datasetId, string routineId)
    {
        var token = GetBearerToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized("Bearer token required.");
        var result = await _routineService.GetRoutineAsync(projectId, datasetId, routineId, token);
        return result.Success ? Ok(result) : NotFound(result);
    }

    /// <summary>Delete a routine.</summary>
    [HttpDelete("{projectId}/{datasetId}/{routineId}")]
    public async Task<IActionResult> DeleteRoutine(string projectId, string datasetId, string routineId)
    {
        var token = GetBearerToken();
        if (string.IsNullOrEmpty(token)) return Unauthorized("Bearer token required.");
        var request = new DeleteRoutineRequest
        {
            ProjectId = projectId,
            DatasetId = datasetId,
            RoutineId = routineId
        };
        var result = await _routineService.DeleteRoutineAsync(request, token);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
