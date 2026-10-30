using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.OperatingSystemVersion;

/// <summary>Gets one operating system version by ID.</summary>
public class GetOperatingSystemVersionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (Operating system version ID.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>OperatingSystemVersionId (Operating system version ID.)</summary>
    [Required] public string OperatingSystemVersionId { get; set; } = string.Empty;
}

/// <summary>Retrieves a list of operating system versions.</summary>
public class ListOperatingSystemVersionsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
}
