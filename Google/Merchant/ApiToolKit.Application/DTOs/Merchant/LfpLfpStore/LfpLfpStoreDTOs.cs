using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.LfpLfpStore;

/// <summary>Retrieves information about a store.</summary>
public class GetLfpStoreRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>LfpStoreId (Required.)</summary>
    [Required] public string LfpStoreId { get; set; } = string.Empty;
}

/// <summary>Deletes a store for a target merchant.</summary>
public class DeleteLfpStoreRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>LfpStoreId (Required.)</summary>
    [Required] public string LfpStoreId { get; set; } = string.Empty;
}

/// <summary>Inserts a store for the target merchant.</summary>
public class InsertLfpStoreRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>LfpStore JSON object as documented at https://developers.google.com/merchant/api/reference/rest/lfp_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Lists the stores of the target merchant, specified by the filter in `ListLfpStoresRequest`.</summary>
public class ListLfpStoresRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public int? PageSize { get; set; }
    /// <summary>Optional.</summary>
    public string? PageToken { get; set; }
    /// <summary>Required.</summary>
    public string? TargetAccount { get; set; }
}
