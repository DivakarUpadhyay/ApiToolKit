using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.PlatformType;

/// <summary>Gets one platform type by ID.</summary>
public class GetPlatformTypeRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (Platform type ID.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>PlatformTypeId (Platform type ID.)</summary>
    [Required] public string PlatformTypeId { get; set; } = string.Empty;
}

/// <summary>Retrieves a list of platform types.</summary>
public class ListPlatformTypesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
}
