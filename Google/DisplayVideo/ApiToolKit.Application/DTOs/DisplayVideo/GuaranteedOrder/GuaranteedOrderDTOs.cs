using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.GuaranteedOrder;

/// <summary>Gets a guaranteed order.</summary>
public class GetGuaranteedOrderRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>GuaranteedOrderId (Required.)</summary>
    [Required] public string GuaranteedOrderId { get; set; } = string.Empty;
    /// <summary>The ID of the partner that has access to the guaranteed order.</summary>
    public string? PartnerId { get; set; }
    /// <summary>The ID of the advertiser that has access to the guaranteed order.</summary>
    public string? AdvertiserId { get; set; }
}

/// <summary>Edits read advertisers of a guaranteed order.</summary>
public class EditGuaranteedOrderReadAccessorsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>GuaranteedOrderId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string GuaranteedOrderId { get; set; } = string.Empty;
    /// <summary>EditGuaranteedOrderReadAccessorsRequest JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Updates an existing guaranteed order.</summary>
public class UpdateGuaranteedOrderRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>GuaranteedOrderId (Output only.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string GuaranteedOrderId { get; set; } = string.Empty;
    /// <summary>The ID of the partner that the request is being made within.</summary>
    public string? PartnerId { get; set; }
    /// <summary>The ID of the advertiser that the request is being made within.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>Required.</summary>
    public string? UpdateMask { get; set; }
    /// <summary>GuaranteedOrder JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Lists guaranteed orders that are accessible to the current user.</summary>
public class ListGuaranteedOrdersRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>The ID of the advertiser that has access to the guaranteed order.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>Allows filtering by guaranteed order fields.</summary>
    public string? Filter { get; set; }
    /// <summary>The ID of the partner that has access to the guaranteed order.</summary>
    public string? PartnerId { get; set; }
    /// <summary>A token identifying a page of results the server should return.</summary>
    public string? PageToken { get; set; }
    /// <summary>Requested page size.</summary>
    public int? PageSize { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? OrderBy { get; set; }
}

/// <summary>Creates a new guaranteed order.</summary>
public class CreateGuaranteedOrderRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>The ID of the advertiser that the request is being made within.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>The ID of the partner that the request is being made within.</summary>
    public string? PartnerId { get; set; }
    /// <summary>GuaranteedOrder JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}
