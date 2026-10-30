using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.City;

/// <summary>Retrieves a list of cities, possibly filtered.</summary>
public class ListCitiesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Select only cities from these regions.</summary>
    public List<string>? RegionDartIds { get; set; }
    /// <summary>Select only cities with names starting with this prefix.</summary>
    public string? NamePrefix { get; set; }
    /// <summary>Select only cities with these DART IDs.</summary>
    public List<string>? DartIds { get; set; }
    /// <summary>Select only cities from these countries.</summary>
    public List<string>? CountryDartIds { get; set; }
}
