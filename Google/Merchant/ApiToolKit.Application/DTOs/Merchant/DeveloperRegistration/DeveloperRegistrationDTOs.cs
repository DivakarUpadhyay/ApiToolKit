using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.DeveloperRegistration;

/// <summary>Retrieves a developer registration for a merchant.</summary>
public class GetDeveloperRegistrationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
}

/// <summary>Retrieves the merchant account that the calling GCP is registered with.</summary>
public class GetAccountForGcpRegistrationDeveloperRegistrationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
}

/// <summary>Unregister the calling GCP from the calling shopping account.</summary>
public class UnregisterGcpDeveloperRegistrationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>UnregisterGcpRequest JSON object as documented at https://developers.google.com/merchant/api/reference/rest/accounts_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Registers the GCP used for the API call to the shopping account passed in the request.</summary>
public class RegisterGcpDeveloperRegistrationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>RegisterGcpRequest JSON object as documented at https://developers.google.com/merchant/api/reference/rest/accounts_v1</summary>
    public object? Body { get; set; }
}
