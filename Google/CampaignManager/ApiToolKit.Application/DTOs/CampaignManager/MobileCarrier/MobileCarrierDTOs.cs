using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.MobileCarrier;

/// <summary>Gets one mobile carrier by ID.</summary>
public class GetMobileCarrierRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (Mobile carrier ID.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>MobileCarrierId (Mobile carrier ID.)</summary>
    [Required] public string MobileCarrierId { get; set; } = string.Empty;
}

/// <summary>Retrieves a list of mobile carriers.</summary>
public class ListMobileCarriersRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
}
