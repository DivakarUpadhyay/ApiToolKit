using ApiToolKit.Application.DTOs.Merchant.DatasourcesDataSource;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// dataSources (v1)
/// </summary>
[ApiController]
[Route("api/merchant/datasources")]
[Produces("application/json")]
public class MerchantDatasourcesDataSourceController : ControllerBase
{
    private readonly IMerchantDatasourcesDataSourceService _datasourcesDataSourceService;

    public MerchantDatasourcesDataSourceController(IMerchantDatasourcesDataSourceService datasourcesDataSourceService)
    {
        _datasourcesDataSourceService = datasourcesDataSourceService;
    }

    /// <summary>Performs the data fetch immediately (even outside fetch schedule) on a data source from your Merchant Center Account.</summary>
    [HttpPost("accounts/{accountId}/dataSources/{dataSourceId}/fetch")]
    public async Task<IActionResult> FetchDataSourceAsync([FromRoute] string accountId, [FromRoute] string dataSourceId, [FromBody] FetchDataSourceRequest request)
    {
        request.AccountId = accountId;
        request.DataSourceId = dataSourceId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _datasourcesDataSourceService.FetchDataSourceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes a data source from your Merchant Center account.</summary>
    [HttpDelete("accounts/{accountId}/dataSources/{dataSourceId}")]
    public async Task<IActionResult> DeleteDataSourceAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string dataSourceId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteDataSourceRequest { AccountKey = accountKey, AccountId = accountId, DataSourceId = dataSourceId };
        var result = await _datasourcesDataSourceService.DeleteDataSourceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Creates the new data source configuration for the given account.</summary>
    [HttpPost("accounts/{accountId}/dataSources")]
    public async Task<IActionResult> CreateDataSourceAsync([FromRoute] string accountId, [FromBody] CreateDataSourceRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _datasourcesDataSourceService.CreateDataSourceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Lists the configurations for data sources for the given account.</summary>
    [HttpGet("accounts/{accountId}/dataSources")]
    public async Task<IActionResult> ListDataSourcesAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListDataSourcesRequest { AccountKey = accountKey, AccountId = accountId, PageSize = pageSize, PageToken = pageToken };
        var result = await _datasourcesDataSourceService.ListDataSourcesAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates the existing data source configuration.</summary>
    [HttpPatch("accounts/{accountId}/dataSources/{dataSourceId}")]
    public async Task<IActionResult> UpdateDataSourceAsync([FromRoute] string accountId, [FromRoute] string dataSourceId, [FromBody] UpdateDataSourceRequest request)
    {
        request.AccountId = accountId;
        request.DataSourceId = dataSourceId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _datasourcesDataSourceService.UpdateDataSourceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves the data source configuration for the given account.</summary>
    [HttpGet("accounts/{accountId}/dataSources/{dataSourceId}")]
    public async Task<IActionResult> GetDataSourceAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string dataSourceId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetDataSourceRequest { AccountKey = accountKey, AccountId = accountId, DataSourceId = dataSourceId };
        var result = await _datasourcesDataSourceService.GetDataSourceAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
