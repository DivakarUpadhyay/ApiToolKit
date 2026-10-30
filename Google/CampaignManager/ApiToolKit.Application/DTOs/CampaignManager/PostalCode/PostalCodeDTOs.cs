using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.CampaignManager.PostalCode;

/// <summary>Retrieves a list of postal codes.</summary>
public class ListPostalCodesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (User profile ID associated with this request.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
}

/// <summary>Gets one postal code by ID.</summary>
public class GetPostalCodeRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>UserprofileId (Postal code ID.)</summary>
    [Required] public string UserprofileId { get; set; } = string.Empty;
    /// <summary>PostalCodeId (Postal code ID.)</summary>
    [Required] public string PostalCodeId { get; set; } = string.Empty;
}
