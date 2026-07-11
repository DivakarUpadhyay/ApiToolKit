using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApiToolKit.Application.DTOs.YouTube.LiveStream;

public class ListLiveStreamsRequest
{
    public int PageSize { get; set; } = 50;
    public string? PageToken { get; set; }
}

public class LiveStreamResponse
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("format")]
    public string Format { get; set; } = string.Empty;

    [JsonProperty("ingestion_type")]
    public string IngestionType { get; set; } = string.Empty;

    [JsonProperty("ingestion_address")]
    public string IngestionAddress { get; set; } = string.Empty;

    [JsonProperty("stream_name")]
    public string StreamName { get; set; } = string.Empty;

    [JsonProperty("backup_ingestion_address")]
    public string BackupIngestionAddress { get; set; } = string.Empty;

    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;

    [JsonProperty("health_status")]
    public string HealthStatus { get; set; } = string.Empty;
}

public class CreateLiveStreamRequest
{
    [Required] public string Title { get; set; } = string.Empty;
    public string Format { get; set; } = "1080p";
    public string IngestionType { get; set; } = "rtmp";
}

public class UpdateLiveStreamRequest
{
    [Required] public string StreamId { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string? Description { get; set; }
}
