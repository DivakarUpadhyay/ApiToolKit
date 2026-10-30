using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Merchant.IssueresolutionIssueresolution;

/// <summary>Start an action.</summary>
public class TriggeractionIssueresolutionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public string? LanguageCode { get; set; }
    /// <summary>TriggerActionPayload JSON object as documented at https://developers.google.com/merchant/api/reference/rest/issueresolution_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Provide a list of business's account issues with an issue resolution content and available actions.</summary>
public class RenderaccountissuesIssueresolutionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public string? TimeZone { get; set; }
    /// <summary>Optional.</summary>
    public string? LanguageCode { get; set; }
    /// <summary>RenderIssuesRequestPayload JSON object as documented at https://developers.google.com/merchant/api/reference/rest/issueresolution_v1</summary>
    public object? Body { get; set; }
}

/// <summary>Provide a list of issues for business's product with an issue resolution content and available actions.</summary>
public class RenderproductissuesIssueresolutionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AccountId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AccountId { get; set; } = string.Empty;
    /// <summary>ProductId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string ProductId { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public string? TimeZone { get; set; }
    /// <summary>Optional.</summary>
    public string? LanguageCode { get; set; }
    /// <summary>RenderIssuesRequestPayload JSON object as documented at https://developers.google.com/merchant/api/reference/rest/issueresolution_v1</summary>
    public object? Body { get; set; }
}
