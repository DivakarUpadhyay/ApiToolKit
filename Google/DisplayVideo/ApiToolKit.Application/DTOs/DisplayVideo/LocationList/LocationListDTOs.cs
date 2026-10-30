using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.LocationList;

/// <summary>Updates a location list.</summary>
public class UpdateLocationListRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>LocationListId (Output only.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string LocationListId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    public string? UpdateMask { get; set; }
    /// <summary>LocationList JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Gets a location list.</summary>
public class GetLocationListRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>LocationListId (Required.)</summary>
    [Required] public string LocationListId { get; set; } = string.Empty;
}

/// <summary>Lists location lists based on a given advertiser id.</summary>
public class ListLocationListsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>Allows filtering by location list fields.</summary>
    public string? Filter { get; set; }
    /// <summary>Requested page size.</summary>
    public int? PageSize { get; set; }
    /// <summary>A token identifying a page of results the server should return.</summary>
    public string? PageToken { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? OrderBy { get; set; }
}

/// <summary>Creates a new location list.</summary>
public class CreateLocationListRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>LocationList JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}
