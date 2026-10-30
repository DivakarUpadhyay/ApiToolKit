using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.InventoriesProductLocalInventory;

/// <summary>Lists the `LocalInventory` resources for the given product in your merchant account.</summary>
public class ListLocalInventoriesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>ProductId (Required.)</summary>
    [Required] public string ProductId { get; set; } = string.Empty;
    /// <summary>A page token, received from a previous `ListLocalInventories` call.</summary>
    public string? PageToken { get; set; }
    /// <summary>The maximum number of `LocalInventory` resources for the given product to return.</summary>
    public int? PageSize { get; set; }
}

/// <summary>Inserts a `LocalInventory` resource to a product in your merchant account.</summary>
public class InsertLocalInventoryRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>ProductId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string ProductId { get; set; } = string.Empty;
    /// <summary>LocalInventory JSON object as documented at https://developers.google.com/merchant/api/reference/rest/inventories_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Deletes the specified `LocalInventory` from the given product in your merchant account.</summary>
public class DeleteLocalInventoryRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>ProductId (Required.)</summary>
    [Required] public string ProductId { get; set; } = string.Empty;
    /// <summary>LocalInventoryId (Required.)</summary>
    [Required] public string LocalInventoryId { get; set; } = string.Empty;
}
