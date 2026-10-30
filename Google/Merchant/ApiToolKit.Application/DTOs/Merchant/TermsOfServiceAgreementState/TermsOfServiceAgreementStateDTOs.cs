using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.TermsOfServiceAgreementState;

/// <summary>Retrieves the state of the agreement for the application terms of service.</summary>
public class RetrieveForApplicationTermsOfServiceAgreementStateRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
}

/// <summary>Returns the state of a terms of service agreement.</summary>
public class GetTermsOfServiceAgreementStateRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>TermsOfServiceAgreementStateId (Required.)</summary>
    [Required] public string TermsOfServiceAgreementStateId { get; set; } = string.Empty;
}
