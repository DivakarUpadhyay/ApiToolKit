using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.NegativeKeywordListNegativeKeyword;

/// <summary>Bulk edits negative keywords in a single negative keyword list.</summary>
public class BulkEditNegativeKeywordRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>NegativeKeywordListId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string NegativeKeywordListId { get; set; } = string.Empty;
    /// <summary>BulkEditNegativeKeywordsRequest JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Deletes a negative keyword from a negative keyword list.</summary>
public class DeleteNegativeKeywordRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>NegativeKeywordListId (Required.)</summary>
    [Required] public string NegativeKeywordListId { get; set; } = string.Empty;
    /// <summary>NegativeKeywordId (Required.)</summary>
    [Required] public string NegativeKeywordId { get; set; } = string.Empty;
}

/// <summary>Lists negative keywords in a negative keyword list.</summary>
public class ListNegativeKeywordsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>NegativeKeywordListId (Required.)</summary>
    [Required] public string NegativeKeywordListId { get; set; } = string.Empty;
    /// <summary>A token identifying a page of results the server should return.</summary>
    public string? PageToken { get; set; }
    /// <summary>Requested page size.</summary>
    public int? PageSize { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? OrderBy { get; set; }
    /// <summary>Allows filtering by negative keyword fields.</summary>
    public string? Filter { get; set; }
}

/// <summary>Replaces all negative keywords in a single negative keyword list.</summary>
public class ReplaceNegativeKeywordRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>NegativeKeywordListId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string NegativeKeywordListId { get; set; } = string.Empty;
    /// <summary>ReplaceNegativeKeywordsRequest JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Creates a negative keyword in a negative keyword list.</summary>
public class CreateNegativeKeywordRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>NegativeKeywordListId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string NegativeKeywordListId { get; set; } = string.Empty;
    /// <summary>NegativeKeyword JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}
