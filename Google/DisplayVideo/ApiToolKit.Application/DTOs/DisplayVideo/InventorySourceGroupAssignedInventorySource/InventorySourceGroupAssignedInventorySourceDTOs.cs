using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.InventorySourceGroupAssignedInventorySource;

/// <summary>Lists inventory sources assigned to an inventory source group.</summary>
public class ListAssignedInventorySourcesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>InventorySourceGroupId (Required.)</summary>
    [Required] public string InventorySourceGroupId { get; set; } = string.Empty;
    /// <summary>Field by which to sort the list.</summary>
    public string? OrderBy { get; set; }
    /// <summary>Allows filtering by assigned inventory source fields.</summary>
    public string? Filter { get; set; }
    /// <summary>Requested page size.</summary>
    public int? PageSize { get; set; }
    /// <summary>The ID of the advertiser that has access to the assignment.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>The ID of the partner that has access to the assignment.</summary>
    public string? PartnerId { get; set; }
    /// <summary>A token identifying a page of results the server should return.</summary>
    public string? PageToken { get; set; }
}

/// <summary>Bulk edits multiple assignments between inventory sources and a single inventory source group.</summary>
public class BulkEditAssignedInventorySourceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>InventorySourceGroupId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string InventorySourceGroupId { get; set; } = string.Empty;
    /// <summary>BulkEditAssignedInventorySourcesRequest JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Creates an assignment between an inventory source and an inventory source group.</summary>
public class CreateAssignedInventorySourceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>InventorySourceGroupId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string InventorySourceGroupId { get; set; } = string.Empty;
    /// <summary>The ID of the partner that owns the parent inventory source group.</summary>
    public string? PartnerId { get; set; }
    /// <summary>The ID of the advertiser that owns the parent inventory source group.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>AssignedInventorySource JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Deletes the assignment between an inventory source and an inventory source group.</summary>
public class DeleteAssignedInventorySourceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>InventorySourceGroupId (Required.)</summary>
    [Required] public string InventorySourceGroupId { get; set; } = string.Empty;
    /// <summary>AssignedInventorySourceId (Required.)</summary>
    [Required] public string AssignedInventorySourceId { get; set; } = string.Empty;
    /// <summary>The ID of the partner that owns the parent inventory source group.</summary>
    public string? PartnerId { get; set; }
    /// <summary>The ID of the advertiser that owns the parent inventory source group.</summary>
    public string? AdvertiserId { get; set; }
}
