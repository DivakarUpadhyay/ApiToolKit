using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.InventorySource;

/// <summary>Updates an existing inventory source.</summary>
public class UpdateInventorySourceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>InventorySourceId (Output only.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string InventorySourceId { get; set; } = string.Empty;
    /// <summary>The ID of the advertiser that the request is being made within.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>Required.</summary>
    public string? UpdateMask { get; set; }
    /// <summary>The ID of the partner that the request is being made within.</summary>
    public string? PartnerId { get; set; }
    /// <summary>InventorySource JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Gets an inventory source.</summary>
public class GetInventorySourceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>InventorySourceId (Required.)</summary>
    [Required] public string InventorySourceId { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>Required.</summary>
    public string? PartnerId { get; set; }
}

/// <summary>Creates a new inventory source.</summary>
public class CreateInventorySourceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>The ID of the advertiser that the request is being made within.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>The ID of the partner that the request is being made within.</summary>
    public string? PartnerId { get; set; }
    /// <summary>InventorySource JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Edits read/write accessors of an inventory source.</summary>
public class EditInventorySourceReadWriteAccessorsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>InventorySourceId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string InventorySourceId { get; set; } = string.Empty;
    /// <summary>EditInventorySourceReadWriteAccessorsRequest JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Lists inventory sources that are accessible to the current user.</summary>
public class ListInventorySourcesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Requested page size.</summary>
    public int? PageSize { get; set; }
    /// <summary>Allows filtering by inventory source fields.</summary>
    public string? Filter { get; set; }
    /// <summary>The ID of the partner that has access to the inventory source.</summary>
    public string? PartnerId { get; set; }
    /// <summary>A token identifying a page of results the server should return.</summary>
    public string? PageToken { get; set; }
    /// <summary>The ID of the advertiser that has access to the inventory source.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? OrderBy { get; set; }
}
