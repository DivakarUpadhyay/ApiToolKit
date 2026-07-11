using Newtonsoft.Json;
namespace ApiToolKit.Application.DTOs.Facebook.Pixel;

public record MetaPixel(
    [property:JsonProperty("id")] string? Id,
    [property:JsonProperty("name")] string? Name,
    [property:JsonProperty("creation_time")] string? CreationTime,
    [property:JsonProperty("last_fired_time")] string? LastFiredTime,
    [property:JsonProperty("is_unavailable")] bool? IsUnavailable);

public record PixelsResponse([property:JsonProperty("data")] List<MetaPixel>? Data);
public record CreatePixelRequest(string Name);
public record UpdatePixelRequest(string Name);

public record PixelStatEntry(
    [property:JsonProperty("start_time")] string? StartTime,
    [property:JsonProperty("end_time")] string? EndTime,
    [property:JsonProperty("event_count")] long? EventCount,
    [property:JsonProperty("event")] string? Event);

public record PixelStatsResponse([property:JsonProperty("data")] List<PixelStatEntry>? Data);

public record PixelAudience(
    [property:JsonProperty("id")] string? Id,
    [property:JsonProperty("name")] string? Name,
    [property:JsonProperty("approximate_count_lower_bound")] long? ApproximateCountLowerBound,
    [property:JsonProperty("description")] string? Description);

public record PixelAudiencesResponse([property:JsonProperty("data")] List<PixelAudience>? Data);
public record SharePixelRequest(string TargetAdAccountId);
