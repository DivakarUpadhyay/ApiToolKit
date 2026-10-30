using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.CombinedAudience;

/// <summary>Lists combined audiences.</summary>
public class ListCombinedAudiencesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Requested page size.</summary>
    public int? PageSize { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? OrderBy { get; set; }
    /// <summary>Allows filtering by combined audience fields.</summary>
    public string? Filter { get; set; }
    /// <summary>The ID of the partner that has access to the fetched combined audiences.</summary>
    public string? PartnerId { get; set; }
    /// <summary>A token identifying a page of results the server should return.</summary>
    public string? PageToken { get; set; }
    /// <summary>The ID of the advertiser that has access to the fetched combined audiences.</summary>
    public string? AdvertiserId { get; set; }
}

/// <summary>Gets a combined audience.</summary>
public class GetCombinedAudienceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>CombinedAudienceId (Required.)</summary>
    [Required] public string CombinedAudienceId { get; set; } = string.Empty;
    /// <summary>The ID of the advertiser that has access to the fetched combined audience.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>The ID of the partner that has access to the fetched combined audience.</summary>
    public string? PartnerId { get; set; }
}
