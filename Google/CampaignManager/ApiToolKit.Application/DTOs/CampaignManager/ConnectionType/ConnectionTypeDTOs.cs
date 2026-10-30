using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.ConnectionType;

/// <summary>Retrieves a list of connection types.</summary>
public class ListConnectionTypesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
}

/// <summary>Gets one connection type by ID.</summary>
public class GetConnectionTypeRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (Connection type ID.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>ConnectionTypeId (Connection type ID.)</summary>
    [Required] public string ConnectionTypeId { get; set; } = string.Empty;
}
