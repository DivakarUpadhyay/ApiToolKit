using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.TermsOfService;

/// <summary>Retrieves the latest version of the `TermsOfService` for a given `kind` and `region_code`.</summary>
public class RetrieveLatestTermsOfServiceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    public string? RegionCode { get; set; }
    /// <summary>Required.</summary>
    public string? Kind { get; set; }
}

/// <summary>Accepts a `TermsOfService`.</summary>
public class AcceptTermsOfServiceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>TermsOfServiceId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string TermsOfServiceId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    public string? Account { get; set; }
    /// <summary>Required.</summary>
    public string? RegionCode { get; set; }
}

/// <summary>Retrieves the `TermsOfService` associated with the provided version.</summary>
public class GetTermsOfServiceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>TermsOfServiceId (Required.)</summary>
    [Required] public string TermsOfServiceId { get; set; } = string.Empty;
}
