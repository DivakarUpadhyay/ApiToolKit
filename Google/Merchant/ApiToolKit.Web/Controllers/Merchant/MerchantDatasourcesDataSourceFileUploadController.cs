using ApiToolKit.Application.DTOs.Merchant.DatasourcesDataSourceFileUpload;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// dataSources / fileUploads (v1)
/// </summary>
[ApiController]
[Route("api/merchant/datasources")]
[Produces("application/json")]
public class MerchantDatasourcesDataSourceFileUploadController : ControllerBase
{
    private readonly IMerchantDatasourcesDataSourceFileUploadService _datasourcesDataSourceFileUploadService;

    public MerchantDatasourcesDataSourceFileUploadController(IMerchantDatasourcesDataSourceFileUploadService datasourcesDataSourceFileUploadService)
    {
        _datasourcesDataSourceFileUploadService = datasourcesDataSourceFileUploadService;
    }

    /// <summary>Gets the latest data source file upload.</summary>
    [HttpGet("accounts/{accountId}/dataSources/{dataSourceId}/fileUploads/{fileUploadId}")]
    public async Task<IActionResult> GetFileUploadAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string dataSourceId, [FromRoute] string fileUploadId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetFileUploadRequest { AccountKey = accountKey, AccountId = accountId, DataSourceId = dataSourceId, FileUploadId = fileUploadId };
        var result = await _datasourcesDataSourceFileUploadService.GetFileUploadAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
