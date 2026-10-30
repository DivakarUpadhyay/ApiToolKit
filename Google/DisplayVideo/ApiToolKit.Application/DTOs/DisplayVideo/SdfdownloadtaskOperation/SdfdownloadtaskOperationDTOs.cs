using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.SdfdownloadtaskOperation;

/// <summary>Gets the latest state of an asynchronous SDF download task operation.</summary>
public class GetOperationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>OperationId (The name of the operation resource.)</summary>
    [Required] public string OperationId { get; set; } = string.Empty;
}
