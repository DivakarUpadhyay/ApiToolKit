using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.OnlineReturnPolicy;

/// <summary>Lists all existing return policies for a given business.</summary>
public class ListOnlineReturnPoliciesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public string? PageToken { get; set; }
    /// <summary>Optional.</summary>
    public int? PageSize { get; set; }
}

/// <summary>Creates a new return policy for a given business.</summary>
public class CreateOnlineReturnPolicyRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>OnlineReturnPolicy JSON object as documented at https://developers.google.com/merchant/api/reference/rest/accounts_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Gets an existing return policy for a given business.</summary>
public class GetOnlineReturnPolicyRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>OnlineReturnPolicyId (Required.)</summary>
    [Required] public string OnlineReturnPolicyId { get; set; } = string.Empty;
}

/// <summary>Deletes an existing return policy.</summary>
public class DeleteOnlineReturnPolicyRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>OnlineReturnPolicyId (Required.)</summary>
    [Required] public string OnlineReturnPolicyId { get; set; } = string.Empty;
}
