using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.InventoriesProductRegionalInventory;

/// <summary>Deletes the specified `RegionalInventory` resource from the given product in your merchant account.</summary>
public class DeleteRegionalInventoryRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>ProductId (Required.)</summary>
    [Required] public string ProductId { get; set; } = string.Empty;
    /// <summary>RegionalInventoryId (Required.)</summary>
    [Required] public string RegionalInventoryId { get; set; } = string.Empty;
}

/// <summary>Lists the `RegionalInventory` resources for the given product in your merchant account.</summary>
public class ListRegionalInventoriesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>ProductId (Required.)</summary>
    [Required] public string ProductId { get; set; } = string.Empty;
    /// <summary>A page token, received from a previous `ListRegionalInventories` call.</summary>
    public string? PageToken { get; set; }
    /// <summary>The maximum number of `RegionalInventory` resources for the given product to return.</summary>
    public int? PageSize { get; set; }
}

/// <summary>Inserts a `RegionalInventory` to a given product in your merchant account.</summary>
public class InsertRegionalInventoryRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>ProductId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string ProductId { get; set; } = string.Empty;
    /// <summary>RegionalInventory JSON object as documented at https://developers.google.com/merchant/api/reference/rest/inventories_v1</summary>
    public object? Body { get; set; }
}
