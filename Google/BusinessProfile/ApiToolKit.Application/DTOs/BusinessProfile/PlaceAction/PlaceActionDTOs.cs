using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.BusinessProfile.PlaceAction;

/// <summary>List place action links of a location.</summary>
public class ListPlaceActionLinksRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>e.g. placeActionType=APPOINTMENT</summary>
    public string? Filter { get; set; }
    /// <summary>Maximum results per page</summary>
    public int? PageSize { get; set; }
    /// <summary>Token for the next page</summary>
    public string? PageToken { get; set; }
}

/// <summary>Get one place action link.</summary>
public class GetPlaceActionLinkRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Place action link id</summary>
    [Required] public string LinkId { get; set; } = string.Empty;
}

/// <summary>Create a place action link.</summary>
public class CreatePlaceActionLinkRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Action URL</summary>
    [Required] public string Uri { get; set; } = string.Empty;
    /// <summary>APPOINTMENT, ONLINE_APPOINTMENT, DINING_RESERVATION, FOOD_ORDERING, FOOD_DELIVERY, FOOD_TAKEOUT, SHOP_ONLINE</summary>
    [Required] public string PlaceActionType { get; set; } = string.Empty;
    /// <summary>Preferred link for this type</summary>
    public bool? IsPreferred { get; set; }
}

/// <summary>Update a place action link (fields in UpdateMask).</summary>
public class UpdatePlaceActionLinkRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Place action link id</summary>
    [Required] public string LinkId { get; set; } = string.Empty;
    /// <summary>Comma-separated fields to update</summary>
    [Required] public string UpdateMask { get; set; } = string.Empty;
    /// <summary>Action URL</summary>
    public string? Uri { get; set; }
    /// <summary>Type</summary>
    public string? PlaceActionType { get; set; }
    /// <summary>Preferred link</summary>
    public bool? IsPreferred { get; set; }
}

/// <summary>Delete a place action link.</summary>
public class DeletePlaceActionLinkRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Place action link id</summary>
    [Required] public string LinkId { get; set; } = string.Empty;
}

/// <summary>List place action types available for a location or region.</summary>
public class ListPlaceActionTypesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>e.g. en</summary>
    public string? LanguageCode { get; set; }
    /// <summary>e.g. location=locations/123 or regionCode=US</summary>
    public string? Filter { get; set; }
    /// <summary>Maximum results per page</summary>
    public int? PageSize { get; set; }
    /// <summary>Token for the next page</summary>
    public string? PageToken { get; set; }
}
