using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.CustomList;

/// <summary>Lists custom lists.</summary>
public class ListCustomListsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Allows filtering by custom list fields.</summary>
    public string? Filter { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? OrderBy { get; set; }
    /// <summary>Requested page size.</summary>
    public int? PageSize { get; set; }
    /// <summary>A token identifying a page of results the server should return.</summary>
    public string? PageToken { get; set; }
    /// <summary>The ID of the DV360 advertiser that has access to the fetched custom lists.</summary>
    public string? AdvertiserId { get; set; }
}

/// <summary>Gets a custom list.</summary>
public class GetCustomListRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>CustomListId (Required.)</summary>
    [Required] public string CustomListId { get; set; } = string.Empty;
    /// <summary>The ID of the DV360 advertiser that has access to the fetched custom lists.</summary>
    public string? AdvertiserId { get; set; }
}
