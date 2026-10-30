using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.BusinessProfile.Verification;

/// <summary>List verification methods available for a location.</summary>
public class FetchVerificationOptionsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>e.g. en</summary>
    [Required] public string LanguageCode { get; set; } = string.Empty;
    /// <summary>ServiceBusinessContext for service-area businesses</summary>
    public object? Context { get; set; }
}

/// <summary>Start verification of a location.</summary>
public class VerifyLocationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>ADDRESS, EMAIL, PHONE_CALL, SMS, AUTO, VETTED_PARTNER</summary>
    [Required] public string Method { get; set; } = string.Empty;
    /// <summary>e.g. en</summary>
    public string? LanguageCode { get; set; }
    /// <summary>For EMAIL</summary>
    public string? EmailAddress { get; set; }
    /// <summary>For PHONE_CALL/SMS</summary>
    public string? PhoneNumber { get; set; }
    /// <summary>Contact name for ADDRESS</summary>
    public string? MailerContact { get; set; }
    /// <summary>VerificationToken for VETTED_PARTNER</summary>
    public object? Token { get; set; }
}

/// <summary>List verifications of a location.</summary>
public class ListVerificationsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Maximum results per page</summary>
    public int? PageSize { get; set; }
    /// <summary>Token for the next page</summary>
    public string? PageToken { get; set; }
}

/// <summary>Complete a pending verification with the PIN.</summary>
public class CompleteVerificationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Verification id</summary>
    [Required] public string VerificationId { get; set; } = string.Empty;
    /// <summary>PIN received</summary>
    [Required] public string Pin { get; set; } = string.Empty;
}

/// <summary>Check whether the location is verified and can be edited.</summary>
public class GetVoiceOfMerchantStateRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
}
