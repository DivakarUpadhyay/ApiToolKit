using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.InventorySourceGroup;

/// <summary>Gets an inventory source group.</summary>
public class GetInventorySourceGroupRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>InventorySourceGroupId (Required.)</summary>
    [Required] public string InventorySourceGroupId { get; set; } = string.Empty;
    /// <summary>The ID of the partner that has access to the inventory source group.</summary>
    public string? PartnerId { get; set; }
    /// <summary>The ID of the advertiser that has access to the inventory source group.</summary>
    public string? AdvertiserId { get; set; }
}

/// <summary>Deletes an inventory source group.</summary>
public class DeleteInventorySourceGroupRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>InventorySourceGroupId (Required.)</summary>
    [Required] public string InventorySourceGroupId { get; set; } = string.Empty;
    /// <summary>The ID of the partner that owns the inventory source group.</summary>
    public string? PartnerId { get; set; }
    /// <summary>The ID of the advertiser that owns the inventory source group.</summary>
    public string? AdvertiserId { get; set; }
}

/// <summary>Lists inventory source groups that are accessible to the current user.</summary>
public class ListInventorySourceGroupsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>The ID of the advertiser that has access to the inventory source group.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>Allows filtering by inventory source group fields.</summary>
    public string? Filter { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? OrderBy { get; set; }
    /// <summary>The ID of the partner that has access to the inventory source group.</summary>
    public string? PartnerId { get; set; }
    /// <summary>A token identifying a page of results the server should return.</summary>
    public string? PageToken { get; set; }
    /// <summary>Requested page size.</summary>
    public int? PageSize { get; set; }
}

/// <summary>Updates an inventory source group.</summary>
public class UpdateInventorySourceGroupRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>InventorySourceGroupId (Output only.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string InventorySourceGroupId { get; set; } = string.Empty;
    /// <summary>The ID of the partner that owns the inventory source group.</summary>
    public string? PartnerId { get; set; }
    /// <summary>Required.</summary>
    public string? UpdateMask { get; set; }
    /// <summary>The ID of the advertiser that owns the inventory source group.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>InventorySourceGroup JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Creates a new inventory source group.</summary>
public class CreateInventorySourceGroupRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>The ID of the partner that owns the inventory source group.</summary>
    public string? PartnerId { get; set; }
    /// <summary>The ID of the advertiser that owns the inventory source group.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>InventorySourceGroup JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}
