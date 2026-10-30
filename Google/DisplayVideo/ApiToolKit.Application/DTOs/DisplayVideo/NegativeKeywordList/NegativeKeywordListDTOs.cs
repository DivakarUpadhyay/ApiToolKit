using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.NegativeKeywordList;

/// <summary>Deletes a negative keyword list given an advertiser ID and a negative keyword list ID.</summary>
public class DeleteNegativeKeywordListRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>NegativeKeywordListId (Required.)</summary>
    [Required] public string NegativeKeywordListId { get; set; } = string.Empty;
}

/// <summary>Lists negative keyword lists based on a given advertiser id.</summary>
public class ListNegativeKeywordListsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>Requested page size.</summary>
    public int? PageSize { get; set; }
    /// <summary>A token identifying a page of results the server should return.</summary>
    public string? PageToken { get; set; }
}

/// <summary>Updates a negative keyword list.</summary>
public class UpdateNegativeKeywordListRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>NegativeKeywordListId (Output only.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string NegativeKeywordListId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    public string? UpdateMask { get; set; }
    /// <summary>NegativeKeywordList JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Gets a negative keyword list given an advertiser ID and a negative keyword list ID.</summary>
public class GetNegativeKeywordListRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>NegativeKeywordListId (Required.)</summary>
    [Required] public string NegativeKeywordListId { get; set; } = string.Empty;
}

/// <summary>Creates a new negative keyword list.</summary>
public class CreateNegativeKeywordListRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>NegativeKeywordList JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}
