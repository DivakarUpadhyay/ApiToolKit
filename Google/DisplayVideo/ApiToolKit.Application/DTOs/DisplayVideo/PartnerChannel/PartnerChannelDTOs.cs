using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.DisplayVideo.PartnerChannel;

/// <summary>Lists channels for a partner or advertiser.</summary>
public class ListChannelsRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>PartnerId (The ID of the partner that owns the channels.)</summary>
    [Required] public string PartnerId { get; set; } = string.Empty;
    /// <summary>The ID of the advertiser that owns the channels.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>A token identifying a page of results the server should return.</summary>
    public string? PageToken { get; set; }
    /// <summary>Requested page size.</summary>
    public int? PageSize { get; set; }
    /// <summary>Allows filtering by channel fields.</summary>
    public string? Filter { get; set; }
    /// <summary>Field by which to sort the list.</summary>
    public string? OrderBy { get; set; }
}

/// <summary>Gets a channel for a partner or advertiser.</summary>
public class GetChannelRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>PartnerId (The ID of the partner that owns the fetched channel.)</summary>
    [Required] public string PartnerId { get; set; } = string.Empty;
    /// <summary>ChannelId (The ID of the partner that owns the fetched channel.)</summary>
    [Required] public string ChannelId { get; set; } = string.Empty;
    /// <summary>The ID of the advertiser that owns the fetched channel.</summary>
    public string? AdvertiserId { get; set; }
}

/// <summary>Creates a new channel.</summary>
public class CreateChannelRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>PartnerId (The ID of the partner that owns the created channel.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string PartnerId { get; set; } = string.Empty;
    /// <summary>The ID of the advertiser that owns the created channel.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>Channel JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}

/// <summary>Updates a channel.</summary>
public class UpdateChannelRequest
{
    /// <summary>Key you chose when connecting the Google account (the state value of the auth URL)</summary>
    [Required] public string AccountKey { get; set; } = string.Empty;
    /// <summary>PartnerId (The ID of the partner that owns the created channel.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string PartnerId { get; set; } = string.Empty;
    /// <summary>ChannelId (Output only.)</summary>
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore] public string ChannelId { get; set; } = string.Empty;
    /// <summary>The ID of the advertiser that owns the created channel.</summary>
    public string? AdvertiserId { get; set; }
    /// <summary>Required.</summary>
    public string? UpdateMask { get; set; }
    /// <summary>Channel JSON object as documented at https://developers.google.com/display-video/api/reference/rest/v4</summary>
    public object? Body { get; set; }
}
