using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.LineItemYoutubeAssetTypeYoutubeAssetAssociation;

/// <summary>Lists the YouTube asset associations linked to the given resource.</summary>
public class ListYoutubeAssetAssociationsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>LineItemId (Required.)</summary>
    [Required] public string LineItemId { get; set; } = string.Empty;
    /// <summary>YoutubeAssetTypeId (Required.)</summary>
    [Required] public string YoutubeAssetTypeId { get; set; } = string.Empty;
    /// <summary>The ID of an ad group.</summary>
    public string? LinkedEntityAdGroupId { get; set; }
    /// <summary>Optional.</summary>
    public string? OrderBy { get; set; }
    /// <summary>Optional.</summary>
    public int? PageSize { get; set; }
    /// <summary>Optional.</summary>
    public string? PageToken { get; set; }
}

/// <summary>Creates a new association between the identified resource and a YouTube asset.</summary>
public class CreateYoutubeAssetAssociationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>LineItemId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string LineItemId { get; set; } = string.Empty;
    /// <summary>YoutubeAssetTypeId (Required.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string YoutubeAssetTypeId { get; set; } = string.Empty;
    /// <summary>The ID of an ad group.</summary>
    public string? LinkedEntityAdGroupId { get; set; }
    /// <summary>YoutubeAssetAssociation JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Deletes an existing association between the identified resource and a YouTube asset.</summary>
public class DeleteYoutubeAssetAssociationRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>AdvertiserId (The ID of a line item.)</summary>
    [Required] public string AdvertiserId { get; set; } = string.Empty;
    /// <summary>LineItemId (The ID of a line item.)</summary>
    [Required] public string LineItemId { get; set; } = string.Empty;
    /// <summary>YoutubeAssetTypeId (The ID of a line item.)</summary>
    [Required] public string YoutubeAssetTypeId { get; set; } = string.Empty;
    /// <summary>YoutubeAssetAssociationId (The ID of a line item.)</summary>
    [Required] public string YoutubeAssetAssociationId { get; set; } = string.Empty;
    /// <summary>The ID of an ad group.</summary>
    public string? LinkedEntityAdGroupId { get; set; }
}
