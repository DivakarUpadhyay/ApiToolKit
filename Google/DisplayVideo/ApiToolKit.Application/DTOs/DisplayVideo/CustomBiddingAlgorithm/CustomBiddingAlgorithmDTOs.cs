using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.CustomBiddingAlgorithm;

/// <summary>Creates a rules reference object for an AlgorithmRules file.</summary>
public class UploadRulesCustomBiddingAlgorithmRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>CustomBiddingAlgorithmId (Required.)</summary>
    [Required] public string CustomBiddingAlgorithmId { get; set; } = string.Empty;
    /// <summary>The ID of the advertiser that owns the parent custom bidding algorithm.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>The ID of the partner that owns the parent custom bidding algorithm.</summary>
    public string? PartnerId { get; set; }
}

/// <summary>Lists custom bidding algorithms that are accessible to the current user and can be used in bidding stratgies.</summary>
public class ListCustomBiddingAlgorithmsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Allows filtering by custom bidding algorithm fields.</summary>
    public string? Filter { get; set; }
    /// <summary>The ID of the DV360 partner that has access to the custom bidding algorithm.</summary>
    public string? PartnerId { get; set; }
    /// <summary>A token identifying a page of results the server should return.</summary>
    public string? PageToken { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? OrderBy { get; set; }
    /// <summary>Requested page size.</summary>
    public int? PageSize { get; set; }
    /// <summary>The ID of the DV360 advertiser that has access to the custom bidding algorithm.</summary>
    public string? AdvertiserId { get; set; }
}

/// <summary>Gets a custom bidding algorithm.</summary>
public class GetCustomBiddingAlgorithmRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>CustomBiddingAlgorithmId (Required.)</summary>
    [Required] public string CustomBiddingAlgorithmId { get; set; } = string.Empty;
    /// <summary>The ID of the DV360 partner that has access to the custom bidding algorithm.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>The ID of the DV360 partner that has access to the custom bidding algorithm.</summary>
    public string? PartnerId { get; set; }
}

/// <summary>Creates a custom bidding script reference object for a script file.</summary>
public class UploadScriptCustomBiddingAlgorithmRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>CustomBiddingAlgorithmId (Required.)</summary>
    [Required] public string CustomBiddingAlgorithmId { get; set; } = string.Empty;
    /// <summary>The ID of the advertiser that owns the parent custom bidding algorithm.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>The ID of the partner that owns the parent custom bidding algorithm.</summary>
    public string? PartnerId { get; set; }
}

/// <summary>Creates a new custom bidding algorithm.</summary>
public class CreateCustomBiddingAlgorithmRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>CustomBiddingAlgorithm JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Updates an existing custom bidding algorithm.</summary>
public class UpdateCustomBiddingAlgorithmRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>CustomBiddingAlgorithmId (Output only.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string CustomBiddingAlgorithmId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    public string? UpdateMask { get; set; }
    /// <summary>CustomBiddingAlgorithm JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}
