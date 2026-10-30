using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.OperatingSystem;

/// <summary>Gets one operating system by DART ID.</summary>
public class GetOperatingSystemRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (Operating system DART ID.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>OperatingSystemId (Operating system DART ID.)</summary>
    [Required] public string OperatingSystemId { get; set; } = string.Empty;
}

/// <summary>Retrieves a list of operating systems.</summary>
public class ListOperatingSystemsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
}
