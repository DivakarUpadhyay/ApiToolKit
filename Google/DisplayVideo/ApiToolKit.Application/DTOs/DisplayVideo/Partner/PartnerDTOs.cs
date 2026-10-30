using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.Partner;

/// <summary>Edits targeting options under a single partner.</summary>
public class EditAssignedTargetingOptionsPartnerRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>PartnerId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string PartnerId { get; set; } = string.Empty;
    /// <summary>BulkEditPartnerAssignedTargetingOptionsRequest JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Lists partners that are accessible to the current user.</summary>
public class ListPartnersRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Allows filtering by partner fields.</summary>
    public string? Filter { get; set; }
    /// <summary>Requested page size.</summary>
    public int? PageSize { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? OrderBy { get; set; }
    /// <summary>A token identifying a page of results the server should return.</summary>
    public string? PageToken { get; set; }
}

/// <summary>Gets a partner.</summary>
public class GetPartnerRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>PartnerId (Required.)</summary>
    [Required] public string PartnerId { get; set; } = string.Empty;
}
