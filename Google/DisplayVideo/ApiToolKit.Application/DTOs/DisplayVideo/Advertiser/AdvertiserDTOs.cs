using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.Advertiser;

/// <summary>Gets an advertiser.</summary>
public class GetAdvertiserRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
}

/// <summary>Lists assigned targeting options of an advertiser across targeting types.</summary>
public class ListAssignedTargetingOptionsAdvertisersRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>Allows filtering by assigned targeting option fields.</summary>
    public string? Filter { get; set; }
    /// <summary>A token that lets the client fetch the next page of results.</summary>
    public string? PageToken { get; set; }
    /// <summary>Requested page size.</summary>
    public int? PageSize { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? OrderBy { get; set; }
}

/// <summary>Edits targeting options under a single advertiser.</summary>
public class EditAssignedTargetingOptionsAdvertiserRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>BulkEditAdvertiserAssignedTargetingOptionsRequest JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Creates a new advertiser.</summary>
public class CreateAdvertiserRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Advertiser JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Updates an existing advertiser.</summary>
public class UpdateAdvertiserRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Output only.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    public string? UpdateMask { get; set; }
    /// <summary>Advertiser JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Audits an advertiser.</summary>
public class AuditAdvertiserRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public string? ReadMask { get; set; }
}

/// <summary>Lists advertisers that are accessible to the current user.</summary>
public class ListAdvertisersRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Allows filtering by advertiser fields.</summary>
    public string? Filter { get; set; }
    /// <summary>Requested page size.</summary>
    public int? PageSize { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? OrderBy { get; set; }
    /// <summary>Required.</summary>
    public string? PartnerId { get; set; }
    /// <summary>A token identifying a page of results the server should return.</summary>
    public string? PageToken { get; set; }
}

/// <summary>Deletes an advertiser.</summary>
public class DeleteAdvertiserRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (The ID of the advertiser we need to delete.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
}
