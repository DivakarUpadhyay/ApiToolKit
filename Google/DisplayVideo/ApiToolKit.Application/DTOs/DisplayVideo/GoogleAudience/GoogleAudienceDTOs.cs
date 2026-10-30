using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.GoogleAudience;

/// <summary>Lists Google audiences.</summary>
public class ListGoogleAudiencesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Field by which to sort the list.</summary>
    public string? OrderBy { get; set; }
    /// <summary>The ID of the partner that has access to the fetched Google audiences.</summary>
    public string? PartnerId { get; set; }
    /// <summary>A token identifying a page of results the server should return.</summary>
    public string? PageToken { get; set; }
    /// <summary>Requested page size.</summary>
    public int? PageSize { get; set; }
    /// <summary>Allows filtering by Google audience fields.</summary>
    public string? Filter { get; set; }
    /// <summary>The ID of the advertiser that has access to the fetched Google audiences.</summary>
    public string? AdvertiserId { get; set; }
}

/// <summary>Gets a Google audience.</summary>
public class GetGoogleAudienceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>GoogleAudienceId (Required.)</summary>
    [Required] public string GoogleAudienceId { get; set; } = string.Empty;
    /// <summary>The ID of the advertiser that has access to the fetched Google audience.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>The ID of the partner that has access to the fetched Google audience.</summary>
    public string? PartnerId { get; set; }
}
