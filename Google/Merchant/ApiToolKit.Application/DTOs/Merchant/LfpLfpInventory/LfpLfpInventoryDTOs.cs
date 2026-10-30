using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.LfpLfpInventory;

/// <summary>Inserts a `LfpInventory` resource for the given target merchant account.</summary>
public class InsertLfpInventoryRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>LfpInventory JSON object as documented at https://developers.google.com/merchant/api/reference/rest/lfp_v1</summary>
    public object? Body { get; set; }
}
