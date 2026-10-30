using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.Creative;

/// <summary>Updates an existing creative.</summary>
public class UpdateCreativeRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Output only.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>CreativeId (Output only.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string CreativeId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    public string? UpdateMask { get; set; }
    /// <summary>Creative JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Creates a new creative.</summary>
public class CreateCreativeRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Output only.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>Creative JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Gets a creative.</summary>
public class GetCreativeRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>CreativeId (Required.)</summary>
    [Required] public string CreativeId { get; set; } = string.Empty;
}

/// <summary>Lists creatives in an advertiser.</summary>
public class ListCreativesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>Field by which to sort the list.</summary>
    public string? OrderBy { get; set; }
    /// <summary>Requested page size.</summary>
    public int? PageSize { get; set; }
    /// <summary>A token identifying a page of results the server should return.</summary>
    public string? PageToken { get; set; }
    /// <summary>Allows filtering by creative fields.</summary>
    public string? Filter { get; set; }
}

/// <summary>Deletes a creative.</summary>
public class DeleteCreativeRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (The ID of the creative to be deleted.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>CreativeId (The ID of the creative to be deleted.)</summary>
    [Required] public string CreativeId { get; set; } = string.Empty;
}
