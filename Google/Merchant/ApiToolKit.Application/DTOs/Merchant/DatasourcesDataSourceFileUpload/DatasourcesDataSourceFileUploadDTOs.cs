using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.DatasourcesDataSourceFileUpload;

/// <summary>Gets the latest data source file upload.</summary>
public class GetFileUploadRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>DataSourceId (Required.)</summary>
    [Required] public string DataSourceId { get; set; } = string.Empty;
    /// <summary>FileUploadId (Required.)</summary>
    [Required] public string FileUploadId { get; set; } = string.Empty;
}
