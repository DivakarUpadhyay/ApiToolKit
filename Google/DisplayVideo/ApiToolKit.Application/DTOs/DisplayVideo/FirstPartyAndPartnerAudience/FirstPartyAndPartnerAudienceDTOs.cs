using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.FirstPartyAndPartnerAudience;

/// <summary>Updates the member list of a Customer Match audience.</summary>
public class EditCustomerMatchMembersFirstPartyAndPartnerAudienceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>FirstPartyAndPartnerAudienceId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string FirstPartyAndPartnerAudienceId { get; set; } = string.Empty;
    /// <summary>EditCustomerMatchMembersRequest JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Lists first party and partner audiences.</summary>
public class ListFirstPartyAndPartnerAudiencesRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Optional.</summary>
    public string? OrderBy { get; set; }
    /// <summary>Optional.</summary>
    public string? Filter { get; set; }
    /// <summary>Optional.</summary>
    public int? PageSize { get; set; }
    /// <summary>The ID of the advertiser that has access to the fetched first party and partner audiences.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>The ID of the partner that has access to the fetched first party and partner audiences.</summary>
    public string? PartnerId { get; set; }
    /// <summary>Optional.</summary>
    public string? PageToken { get; set; }
}

/// <summary>Gets a first party or partner audience.</summary>
public class GetFirstPartyAndPartnerAudienceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>FirstPartyAndPartnerAudienceId (Required.)</summary>
    [Required] public string FirstPartyAndPartnerAudienceId { get; set; } = string.Empty;
    /// <summary>The ID of the partner that has access to the fetched first party and partner audience.</summary>
    public string? PartnerId { get; set; }
    /// <summary>The ID of the advertiser that has access to the fetched first party and partner audience.</summary>
    public string? AdvertiserId { get; set; }
}

/// <summary>Updates an existing FirstPartyAndPartnerAudience.</summary>
public class UpdateFirstPartyAndPartnerAudienceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>FirstPartyAndPartnerAudienceId (Identifier.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string FirstPartyAndPartnerAudienceId { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>Required.</summary>
    public string? UpdateMask { get; set; }
    /// <summary>FirstPartyAndPartnerAudience JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Creates a FirstPartyAndPartnerAudience.</summary>
public class CreateFirstPartyAndPartnerAudienceRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Required.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>FirstPartyAndPartnerAudience JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}
