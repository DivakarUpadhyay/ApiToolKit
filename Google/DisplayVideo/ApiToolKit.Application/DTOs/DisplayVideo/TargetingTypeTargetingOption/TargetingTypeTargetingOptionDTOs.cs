using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.TargetingTypeTargetingOption;

/// <summary>Gets a single targeting option.</summary>
public class GetTargetingOptionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>TargetingTypeId (Required.)</summary>
    [Required] public string TargetingTypeId { get; set; } = string.Empty;
    /// <summary>TargetingOptionId (Required.)</summary>
    [Required] public string TargetingOptionId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    public string? AdvertiserId { get; set; }
}

/// <summary>Searches for targeting options of a given type based on the given search terms.</summary>
public class SearchTargetingOptionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>TargetingTypeId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string TargetingTypeId { get; set; } = string.Empty;
    /// <summary>SearchTargetingOptionsRequest JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Lists targeting options of a given type.</summary>
public class ListTargetingOptionsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>TargetingTypeId (Required.)</summary>
    [Required] public string TargetingTypeId { get; set; } = string.Empty;
    /// <summary>A token identifying a page of results the server should return.</summary>
    public string? PageToken { get; set; }
    /// <summary>Requested page size.</summary>
    public int? PageSize { get; set; }
    /// <summary>Required.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>Allows filtering by targeting option fields.</summary>
    public string? Filter { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? OrderBy { get; set; }
}
