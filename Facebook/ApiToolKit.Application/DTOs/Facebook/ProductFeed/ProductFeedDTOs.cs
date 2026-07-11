using Newtonsoft.Json;
namespace ApiToolKit.Application.DTOs.Facebook.ProductFeed;

public record FeedSchedule([property:JsonProperty("interval")] string? Interval, [property:JsonProperty("url")] string? Url, [property:JsonProperty("hour")] int? Hour);
public record ProductFeed([property:JsonProperty("id")] string? Id, [property:JsonProperty("name")] string? Name, [property:JsonProperty("file_name")] string? FileName, [property:JsonProperty("format")] string? Format, [property:JsonProperty("country")] string? Country, [property:JsonProperty("language")] string? Language, [property:JsonProperty("default_currency")] string? DefaultCurrency, [property:JsonProperty("encoding")] string? Encoding, [property:JsonProperty("schedule")] FeedSchedule? Schedule);
public record ProductFeedsResponse([property:JsonProperty("data")] List<ProductFeed>? Data);
public record CreateProductFeedRequest(string Name, string? ScheduleUrl = null, string? Interval = "DAILY", string? Format = "TSV", string? Encoding = "UTF-8", string? Country = null, string? Language = null, string? DefaultCurrency = null);
public record FeedUpload([property:JsonProperty("id")] string? Id, [property:JsonProperty("start_time")] string? StartTime, [property:JsonProperty("end_time")] string? EndTime, [property:JsonProperty("url")] string? Url, [property:JsonProperty("num_persisted_items")] int? NumPersistedItems, [property:JsonProperty("num_detected_items")] int? NumDetectedItems, [property:JsonProperty("status")] string? Status, [property:JsonProperty("error_count")] int? ErrorCount, [property:JsonProperty("warning_count")] int? WarningCount);
public record FeedUploadsResponse([property:JsonProperty("data")] List<FeedUpload>? Data);
public record FeedError([property:JsonProperty("id")] string? Id, [property:JsonProperty("description")] string? Description, [property:JsonProperty("severity")] string? Severity, [property:JsonProperty("summary")] string? Summary, [property:JsonProperty("sample_affected_items_count")] int? SampleAffectedItemsCount);
public record FeedErrorsResponse([property:JsonProperty("data")] List<FeedError>? Data);
