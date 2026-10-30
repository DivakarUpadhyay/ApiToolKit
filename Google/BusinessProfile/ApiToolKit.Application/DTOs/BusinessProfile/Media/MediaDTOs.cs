using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.BusinessProfile.Media;

/// <summary>List owner media of a location.</summary>
public class ListMediaRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Maximum results per page</summary>
    public int? PageSize { get; set; }
    /// <summary>Token for the next page</summary>
    public string? PageToken { get; set; }
}

/// <summary>Get one media item.</summary>
public class GetMediaRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Media id</summary>
    [Required] public string MediaId { get; set; } = string.Empty;
}

/// <summary>Add a photo or video to a location from a public URL.</summary>
public class CreateMediaRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>PHOTO or VIDEO</summary>
    [Required] public string MediaFormat { get; set; } = string.Empty;
    /// <summary>Public URL of the file</summary>
    [Required] public string SourceUrl { get; set; } = string.Empty;
    /// <summary>COVER, PROFILE, LOGO, EXTERIOR, INTERIOR, PRODUCT, AT_WORK, FOOD_AND_DRINK, MENU, TEAMS, ADDITIONAL</summary>
    public string? Category { get; set; }
    /// <summary>Description</summary>
    public string? Description { get; set; }
}

/// <summary>Update media metadata (fields in UpdateMask).</summary>
public class UpdateMediaRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Media id</summary>
    [Required] public string MediaId { get; set; } = string.Empty;
    /// <summary>Comma-separated fields to update</summary>
    [Required] public string UpdateMask { get; set; } = string.Empty;
    /// <summary>Description</summary>
    public string? Description { get; set; }
    /// <summary>Category</summary>
    public string? Category { get; set; }
}

/// <summary>Delete a media item.</summary>
public class DeleteMediaRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Media id</summary>
    [Required] public string MediaId { get; set; } = string.Empty;
}

/// <summary>Start a byte upload; returns a resource name to upload bytes to.</summary>
public class StartMediaUploadRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
}

/// <summary>List media uploaded by customers.</summary>
public class ListCustomerMediaRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Maximum results per page</summary>
    public int? PageSize { get; set; }
    /// <summary>Token for the next page</summary>
    public string? PageToken { get; set; }
}

/// <summary>Get one customer media item.</summary>
public class GetCustomerMediaRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>Business Profile account id (number only)</summary>
    [Required] public string AccountId { get; set; } = string.Empty;
    /// <summary>Location id (number only)</summary>
    [Required] public string LocationId { get; set; } = string.Empty;
    /// <summary>Media id</summary>
    [Required] public string MediaId { get; set; } = string.Empty;
}
