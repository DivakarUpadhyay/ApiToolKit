using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiToolKit.Application.DTOs.YouTube.Caption;

public class ListCaptionsRequest
{
    [Required] public string VideoId { get; set; } = string.Empty;
}

public class CaptionResponse
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("video_id")]
    public string VideoId { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("language")]
    public string Language { get; set; } = string.Empty;

    [JsonProperty("track_kind")]
    public string TrackKind { get; set; } = string.Empty;

    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;

    [JsonProperty("is_draft")]
    public bool IsDraft { get; set; }

    [JsonProperty("is_auto_synced")]
    public bool IsAutoSynced { get; set; }

    [JsonProperty("last_updated")]
    public DateTime LastUpdated { get; set; }
}

public class CreateCaptionRequest
{
    [Required] public string VideoId { get; set; } = string.Empty;
    [Required] public string Language { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
    public bool IsDraft { get; set; } = false;
}

public class UpdateCaptionRequest
{
    [Required] public string CaptionId { get; set; } = string.Empty;
    public bool? IsDraft { get; set; }
}

public class DeleteCaptionRequest
{
    [Required] public string CaptionId { get; set; } = string.Empty;
}
