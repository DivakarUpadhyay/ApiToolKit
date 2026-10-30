using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.LfpLfpSale;

/// <summary>Inserts a `LfpSale` for the given merchant.</summary>
public class InsertLfpSaleRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>LfpSale JSON object as documented at https://developers.google.com/merchant/api/reference/rest/lfp_v1</summary>
    public object? Body { get; set; }
}
