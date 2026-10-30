using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.MobileApp;

/// <summary>Gets one mobile app by ID.</summary>
public class GetMobileAppRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>MobileAppId (User profile ID associated with this request.)</summary>
    [Required] public string MobileAppId { get; set; } = string.Empty;
}

/// <summary>Retrieves list of available mobile apps.</summary>
public class ListMobileAppsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>Select only apps from these directories.</summary>
    public List<string>? Directories { get; set; }
    /// <summary>Maximum number of results to return.</summary>
    public int? MaxResults { get; set; }
    /// <summary>Allows searching for objects by name or ID.</summary>
    public string? SearchString { get; set; }
    /// <summary>Value of the nextPageToken from the previous result page.</summary>
    public string? PageToken { get; set; }
    /// <summary>Select only apps with these IDs.</summary>
    public List<string>? Ids { get; set; }
}
