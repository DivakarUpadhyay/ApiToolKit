using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.CustomBiddingAlgorithmRule;

/// <summary>Creates a new rules resource.</summary>
public class CreateRuleRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>CustomBiddingAlgorithmId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string CustomBiddingAlgorithmId { get; set; } = string.Empty;
    /// <summary>The ID of the advertiser that owns the parent custom bidding algorithm.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>The ID of the partner that owns the parent custom bidding algorithm.</summary>
    public string? PartnerId { get; set; }
    /// <summary>CustomBiddingAlgorithmRules JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Retrieves a rules resource.</summary>
public class GetRuleRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>CustomBiddingAlgorithmId (Required.)</summary>
    [Required] public string CustomBiddingAlgorithmId { get; set; } = string.Empty;
    /// <summary>RuleId (Required.)</summary>
    [Required] public string RuleId { get; set; } = string.Empty;
    /// <summary>The ID of the partner that owns the parent custom bidding algorithm.</summary>
    public string? PartnerId { get; set; }
    /// <summary>The ID of the advertiser that owns the parent custom bidding algorithm.</summary>
    public string? AdvertiserId { get; set; }
}

/// <summary>Lists rules resources that belong to the given algorithm.</summary>
public class ListRulesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>CustomBiddingAlgorithmId (Required.)</summary>
    [Required] public string CustomBiddingAlgorithmId { get; set; } = string.Empty;
    /// <summary>Requested page size.</summary>
    public int? PageSize { get; set; }
    /// <summary>The ID of the advertiser that owns the parent custom bidding algorithm.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? OrderBy { get; set; }
    /// <summary>The ID of the partner that owns the parent custom bidding algorithm.</summary>
    public string? PartnerId { get; set; }
    /// <summary>A token identifying a page of results the server should return.</summary>
    public string? PageToken { get; set; }
}
