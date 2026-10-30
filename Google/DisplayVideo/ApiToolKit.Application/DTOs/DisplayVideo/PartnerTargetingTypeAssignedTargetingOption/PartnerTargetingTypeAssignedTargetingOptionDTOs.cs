using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.PartnerTargetingTypeAssignedTargetingOption;

/// <summary>Assigns a targeting option to a partner.</summary>
public class CreateAssignedTargetingOptionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>PartnerId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string PartnerId { get; set; } = string.Empty;
    /// <summary>TargetingTypeId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string TargetingTypeId { get; set; } = string.Empty;
    /// <summary>AssignedTargetingOption JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Deletes an assigned targeting option from a partner.</summary>
public class DeleteAssignedTargetingOptionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>PartnerId (Required.)</summary>
    [Required] public string PartnerId { get; set; } = string.Empty;
    /// <summary>TargetingTypeId (Required.)</summary>
    [Required] public string TargetingTypeId { get; set; } = string.Empty;
    /// <summary>AssignedTargetingOptionId (Required.)</summary>
    [Required] public string AssignedTargetingOptionId { get; set; } = string.Empty;
}

/// <summary>Gets a single targeting option assigned to a partner.</summary>
public class GetAssignedTargetingOptionRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>PartnerId (Required.)</summary>
    [Required] public string PartnerId { get; set; } = string.Empty;
    /// <summary>TargetingTypeId (Required.)</summary>
    [Required] public string TargetingTypeId { get; set; } = string.Empty;
    /// <summary>AssignedTargetingOptionId (Required.)</summary>
    [Required] public string AssignedTargetingOptionId { get; set; } = string.Empty;
}

/// <summary>Lists the targeting options assigned to a partner.</summary>
public class ListAssignedTargetingOptionsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>PartnerId (Required.)</summary>
    [Required] public string PartnerId { get; set; } = string.Empty;
    /// <summary>TargetingTypeId (Required.)</summary>
    [Required] public string TargetingTypeId { get; set; } = string.Empty;
    /// <summary>Field by which to sort the list.</summary>
    public string? OrderBy { get; set; }
    /// <summary>Allows filtering by assigned targeting option fields.</summary>
    public string? Filter { get; set; }
    /// <summary>Requested page size.</summary>
    public int? PageSize { get; set; }
    /// <summary>A token identifying a page of results the server should return.</summary>
    public string? PageToken { get; set; }
}
